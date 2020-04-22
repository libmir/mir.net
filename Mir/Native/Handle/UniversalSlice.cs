using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UniversalSlice : IUniversalSlice
    {
        public UIntPtr Length { get; }
        public IntPtr Stride { get; }
        internal IntPtr Iterator { get; }

        public UniversalSlice(UIntPtr length, IntPtr stride, IntPtr iterator)
        {
            Length = length;
            Stride = stride;
            Iterator = iterator;
        }

        public UniversalSlice(Slice slice)
        {
            Length = slice.Length;
            Stride = (IntPtr)1;
            Iterator = slice.Iterator;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ref T Get<T>(int index)
            where T : unmanaged
        {
            if ((uint)index >= (uint)Length)
                throw new IndexOutOfRangeException();
            unsafe
            {
                return ref ((T*)Iterator)[index * (int)Stride];
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

        public UniversalSlice UniversalSliceView => this;

        public void IncreaseCounter(){}

        public void DecreaseCounter(){}
    }
}
