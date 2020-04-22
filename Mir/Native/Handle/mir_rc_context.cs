using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void MirDeallocateDelegate(mir_rc_context* obj);

    [StructLayout(LayoutKind.Sequential)]
    internal ref struct mir_rc_context
    {
        internal IntPtr deallocator;
        internal IntPtr typeInfo;
        internal UIntPtr counter;
        internal UIntPtr length;

        internal MirDeallocateDelegate Deallocator
        {
            get => Marshal.GetDelegateForFunctionPointer<MirDeallocateDelegate>(deallocator);
            set => deallocator = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal unsafe ref mir_type_info TypeInfo => ref *(mir_type_info*)typeInfo;

        public UIntPtr Counter => counter;

        public UIntPtr Length => length;
    }
}
