using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    // Contiguous kind
    // Row major format
    [StructLayout(LayoutKind.Sequential)]
    public struct RCMatrix : IMatrix
    {
        internal UIntPtr Length { get; }
        internal UIntPtr RowLength { get; }
        internal RCIterator Iterator { get; }

        public static RCMatrix Create<T>(int rows, int cols)
            where T : unmanaged
        {
            return new RCMatrix(new UIntPtr((uint)rows), new UIntPtr((uint)cols), new RCIterator(RCArray.Create<T>(rows * cols)));
        }

        public RCMatrix(UIntPtr rows, UIntPtr cols, RCIterator iterator)
        {
            Length = rows;
            RowLength = cols;
            Iterator = iterator;
        }

        public Matrix MatrixView => new Matrix(Length, RowLength, Iterator.Iterator);

        public void IncreaseCounter() => Iterator.IncreaseCounter();
        
        public void DecreaseCounter() => Iterator.DecreaseCounter();
    }
}
