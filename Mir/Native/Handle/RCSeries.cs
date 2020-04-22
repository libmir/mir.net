using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RCSeries : ISeries
    {
        private RCSlice Data;
        private RCIterator IndexIt;

        public UIntPtr Length => Data.Length;

        public static RCSeries Create<K, V>(int length)
            where K : unmanaged
            where V : unmanaged
        {
            return new RCSeries(RCSlice.Create<K>(length), RCSlice.Create<V>(length));
        }

        public RCSeries(RCArray index, RCArray values)
            : this(new RCSlice(index), new RCSlice(values)) {}

        public RCSeries(RCSlice index, RCSlice values)
        {
            if (index.SliceView.Length != values.SliceView.Length)
                throw new IndexOutOfRangeException();
            Data = values;
            IndexIt = index.Iterator;
        }

        public Series SeriesView => new Series(Data.SliceView, IndexIt.Iterator);
        public RCSlice ValuesRCView => Data;
        public RCSlice KeysRCView => new RCSlice(ValuesView.Length, IndexIt);
        public Slice ValuesView => Data.SliceView;
        public Slice KeysView => new Slice(ValuesView.Length, IndexIt.Iterator);

        public void IncreaseCounter()
        {
            Data.IncreaseCounter();
            IndexIt.IncreaseCounter();
        }
        
        public void DecreaseCounter()
        {
            Data.DecreaseCounter();
            IndexIt.DecreaseCounter();
        }
    }
}
