using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RCArray : ISlice
    {
        internal IntPtr Payload { get; }

        public RCArray(IntPtr payload)
        {
            Payload = payload;
        }

        internal unsafe RCArray(mir_rc_context_ptr context)
            : this(context.ToFollowingPtr()) {}

        public static unsafe RCArray Create<T>(int length)
            where T : unmanaged
        {
            var context = new mir_rc_context_ptr(MirTypeInfoBuilder<T>.Info, new UIntPtr((uint)length));
            return new RCArray(context);
        }

        internal mir_rc_context_ptr Context => mir_rc_context_ptr.FromFollowingPtr(Payload);

        public Slice SliceView => new Slice(Context.Length, Payload);
        public UniversalSlice UniversalSliceView => new UniversalSlice(SliceView);

        public UIntPtr Length => Context.Length;

        public void IncreaseCounter() => Context.IncreaseCounter();

        public void DecreaseCounter() => Context.DecreaseCounter();
    }
}
