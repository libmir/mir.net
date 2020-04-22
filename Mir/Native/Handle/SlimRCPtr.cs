using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SlimRCPtr : IMirRC
    {
        public IntPtr Ptr { get; }
        private mir_rc_context_ptr Context => mir_rc_context_ptr.FromFollowingPtr(Ptr);

        public SlimRCPtr(IntPtr ptr)
        {
            Ptr = ptr;
        }

        internal SlimRCPtr(mir_rc_context_ptr context)
        {
            Ptr = context.ToFollowingPtr();
        }

        internal unsafe ref T Get<T>()
            where T : unmanaged
        {
            return ref *((T*)Ptr);
        }

        internal static unsafe SlimRCPtr Create<T>()
            where T : unmanaged
        {
            var context = new mir_rc_context_ptr(MirTypeInfoBuilder<T>.Info, new UIntPtr(1));
            var ret = new SlimRCPtr(context);
            return ret;
        }

        internal static unsafe SlimRCPtr Create<T>(T defaultValue)
            where T : unmanaged
        {
            var ret = Create<T>();
            ret.Get<T>() = defaultValue;
            return ret;
        }

        public void IncreaseCounter() => Context.IncreaseCounter();

        public void DecreaseCounter() => Context.DecreaseCounter();
    }
}
