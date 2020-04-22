using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Series : ISeries
    {
        private Slice Data;
        private IntPtr IndexIt;

        public Series(Slice data, IntPtr index)
        {
            Data = data;
            IndexIt = index;
        }

        public UIntPtr Length => Data.Length; 
        public Series SeriesView => this;
        public Slice ValuesView => Data;
        public Slice KeysView => new Slice(Length, IndexIt);

        public void IncreaseCounter() {}

        public void DecreaseCounter() {}
    }
}
