using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Slice : ISlice
    {
        public UIntPtr Length { get; }
        internal IntPtr Iterator { get; }

        public Slice(UIntPtr length, IntPtr iterator)
        {
            Length = length;
            Iterator = iterator;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ref T Get<T>(int index)
            where T : unmanaged
        {
            if ((uint)index >= (uint)Length)
                throw new IndexOutOfRangeException();
            unsafe
            {
                return ref ((T*)Iterator)[index];
            }
        }

        internal void Set<T, I>(int index, T value)
            where I : unmanaged
            where T : MirWrapper<I>
        {
            Assign(ref Get<I>(index), value);
        }

        internal T Get<T, I>(int index)
            where I : unmanaged
            where T : MirWrapper<I>
        {
            return Native.MirWrapperManager<T, I>.New(Get<I>(index));
        }

        internal static void Assign<T, I>(ref I lhs, T rhs)
            where I : unmanaged
            where T : MirWrapper<I>
        {
            MirContextManager<I>.IncreaseCounter(rhs.Handle);
            MirContextManager<I>.DecreaseCounter(lhs);
            lhs = rhs.Handle;
        }

        public Slice SliceView => this;
        public UniversalSlice UniversalSliceView => new UniversalSlice(SliceView);

        public void IncreaseCounter(){}

        public void DecreaseCounter(){}
    }
}
