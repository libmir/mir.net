using System;
namespace Mir
{
    public abstract class MirArrayBase<T> : ArraySliceBase<T, Native.Handle.RCArray>
    {
        public MirArrayBase(Native.Handle.RCArray array) : base(array) {}
        
        public sealed override System.UIntPtr Length => Handle.SliceView.Length;

        public override unsafe string ToString()
        {
            if(typeof(T) != typeof(byte))
                return base.ToString();
            if (Count == 0)
                return "";
            return System.Text.Encoding.UTF8.GetString((byte*)Handle.SliceView.Iterator, (int)Handle.SliceView.Length);
        }
    }
}
