namespace Mir
{
    public sealed class SliceView<T> : ArraySliceBase<T, Native.Handle.Slice>
        where T : unmanaged
    {
        public SliceView()
            : this(default(Native.Handle.Slice))
        {
        }

        public SliceView(Native.Handle.Slice slice)
            : base(slice)
        {
        }

        public override T this[int index]
        {
            get { return Handle.Get<T>(index); }
            set { Handle.Get<T>(index) = value; }
        }

        public override System.UIntPtr Length => Handle.Length;

        public override unsafe string ToString()
        {
            if(typeof(T) != typeof(byte))
                return base.ToString();
            if (Count == 0)
                return "";
            return System.Text.Encoding.UTF8.GetString((byte*)Handle.Iterator, (int)Handle.Length);
        }
    }
}
