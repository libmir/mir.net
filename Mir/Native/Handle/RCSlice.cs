using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RCSlice : ISlice
    {
        internal UIntPtr Length { get; }
        internal RCIterator Iterator { get; }

        public static RCSlice Create<T>(int length)
            where T : unmanaged
        {
            return new RCSlice(RCArray.Create<T>(length));
        }

        public RCSlice(RCArray array)
            : this(array.Length, new RCIterator(array)) {}

        public RCSlice(UIntPtr length, RCIterator iterator)
        {
            Length = length;
            Iterator = iterator;
        }

        public Slice SliceView => new Slice(Length, Iterator.Iterator);
        public UniversalSlice UniversalSliceView => new UniversalSlice(SliceView);

        public void IncreaseCounter() => Iterator.IncreaseCounter();
        
        public void DecreaseCounter() => Iterator.DecreaseCounter();
    }
}
