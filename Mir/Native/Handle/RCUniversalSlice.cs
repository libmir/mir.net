using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RCUniversalSlice : IUniversalSlice
    {
        internal UIntPtr Length { get; }
        internal IntPtr Stride { get; }
        internal RCIterator Iterator { get; }

        public static RCUniversalSlice Create<T>(int length)
            where T : unmanaged
        {
            return new RCUniversalSlice(RCArray.Create<T>(length));
        }

        public RCUniversalSlice(RCArray array)
            : this(array.Length, (IntPtr)1, new RCIterator(array)) {}

        public RCUniversalSlice(UIntPtr length, IntPtr stride, RCIterator iterator)
        {
            Length = length;
            Iterator = iterator;
            Stride = stride;
        }

        public RCUniversalSlice(RCSlice slice)
            : this(slice.Length, (IntPtr)1, slice.Iterator) {}

        public UniversalSlice UniversalSliceView => new UniversalSlice(Length, Stride, Iterator.Iterator);

        public void IncreaseCounter() => Iterator.IncreaseCounter();
        
        public void DecreaseCounter() => Iterator.DecreaseCounter();
    }
}
