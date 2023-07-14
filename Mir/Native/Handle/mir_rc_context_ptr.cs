using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct mir_rc_context_ptr : IMirRC
    {
        internal readonly IntPtr Payload;

        internal static readonly unsafe MirDeallocateDelegate DeallocateDelegate = delegate (mir_rc_context* context)
        {
            Marshal.FreeHGlobal((IntPtr)context);
        };

        internal unsafe mir_rc_context_ptr(IntPtr typeInfo, UIntPtr length)
        {
            if (length == default(UIntPtr))
            {
                Payload = default(IntPtr);
                return;
            }
            var size = (int)length * (*(mir_type_info*)typeInfo).Size + sizeof(mir_rc_context);
            Payload = Marshal.AllocHGlobal(size);
            Unsafe.InitBlock(ref *(byte*)Payload, default(byte), (uint)size);
            Context.Deallocator = DeallocateDelegate;
            Context.typeInfo = typeInfo;
            Context.counter = (UIntPtr)1;
            Context.length = length;
        }

        public unsafe ref mir_rc_context Context => ref *(mir_rc_context*) (Payload);

        public static int Size => IntPtr.Size;

        public static readonly mir_rc_context_ptr Zero = new mir_rc_context_ptr(IntPtr.Zero);

        public bool IsNull => Payload == IntPtr.Zero;

        public mir_rc_context_ptr(IntPtr payload) => Payload = payload;

        public static unsafe mir_rc_context_ptr FromFollowingPtr(IntPtr payload)
        {
            return new mir_rc_context_ptr(payload == IntPtr.Zero ?
                IntPtr.Zero:
                payload - sizeof(mir_rc_context));
        }

        public unsafe IntPtr ToFollowingPtr()
        {
            return Payload == IntPtr.Zero ?
                IntPtr.Zero:
                Payload + sizeof(mir_rc_context);
        }

        public unsafe void IncreaseCounter()
        {
            if (!IsNull)
            {
                if (Context.counter != default(UIntPtr))
                {
                    if (sizeof(UIntPtr) == 8)
                        Interlocked.Increment(ref *(long*)(Payload + 16));
                    else
                        Interlocked.Increment(ref *(int*)(Payload + 8));
                }
            }
        }

        public unsafe void DecreaseCounter()
        {
            if (!IsNull)
            {
                if (Context.counter != default(UIntPtr))
                {
                    if (sizeof(UIntPtr) == 8)
                    {
                        if (Interlocked.Decrement(ref *(long*)(Payload + 16)) != 0)
                            return;
                    }
                    else
                    {
                        if (Interlocked.Decrement(ref *(int*)(Payload + 8)) != 0)
                            return;
                    }
                    var destructor = Context.TypeInfo.Destructor;
                    var size = Context.TypeInfo.Size;
                    if (!(destructor is null))
                    {
                        var ptr = Payload + sizeof(mir_rc_context);
                        var i = Context.length;
                        do
                        {
                            destructor((void*)ptr);
                            ptr = ptr + size;
                            i = i - 1;
                        } while(i != default(UIntPtr));
                    }
                    Context.Deallocator((mir_rc_context*)Payload);
                }
            }
        }

        public UIntPtr Counter => IsNull ? UIntPtr.Zero : Context.Counter;

        public UIntPtr Length => IsNull ? UIntPtr.Zero : Context.Length;

    }
}
