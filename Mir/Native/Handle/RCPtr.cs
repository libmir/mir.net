using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RCPtr : IMirRC
    {
        public IntPtr Ptr;
        private mir_rc_context_ptr Context;

        internal RCPtr(mir_rc_context_ptr context)
        {
            Ptr = context.ToFollowingPtr();
            Context = context;
        }

        internal RCPtr(IntPtr ptr, mir_rc_context_ptr context)
        {
            Ptr = ptr;
            Context = context;
        }

        internal unsafe ref T Get<T>()
            where T : unmanaged
        {
            return ref *((T*)Ptr);
        }

        internal static unsafe RCPtr Create<T>()
            where T : unmanaged
        {
            var context = new mir_rc_context_ptr(MirTypeInfoBuilder<T>.Info, new UIntPtr(1));
            return new RCPtr(context);
        }

        internal static unsafe RCPtr Create<T>(T defaultValue)
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
