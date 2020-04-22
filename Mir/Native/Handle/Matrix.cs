using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    // Contiguous kind
    // Row major format
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix : IMatrix
    {
        public UIntPtr Length { get; }
        public UIntPtr RowLength { get; }
        internal IntPtr Iterator { get; }

        public Matrix(UIntPtr rows, UIntPtr cols, IntPtr iterator)
        {
            Length = rows;
            RowLength = cols;
            Iterator = iterator;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ref T Get<T>(int row, int col)
            where T : unmanaged
        {
            if ((uint)row >= (uint)Length || (uint)col >= (uint)RowLength)
                throw new IndexOutOfRangeException();
            int index = row * (int)RowLength + col;
            unsafe
            {
                return ref ((T*)Iterator)[index];
            }
        }

        internal void Set<T, I>(int row, int col, T value)
            where I : unmanaged
            where T : MirWrapper<I>
        {
            Slice.Assign(ref Get<I>(row, col), value);
        }

        internal T Get<T, I>(int row, int col)
            where I : unmanaged
            where T : MirWrapper<I>
        {
            return Native.MirWrapperManager<T, I>.New(Get<I>(row, col));
        }

        public Matrix MatrixView => this;

        public void IncreaseCounter(){}

        public void DecreaseCounter(){}
    }
}
