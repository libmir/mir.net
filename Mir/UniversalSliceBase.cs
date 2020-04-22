namespace Mir
{
    public abstract class UniversalSliceBase<T> : ArraySliceBase<T, Native.Handle.RCUniversalSlice>
    {
        public UniversalSliceBase(Native.Handle.RCUniversalSlice slice) : base(slice) {}
        
        public sealed override System.UIntPtr Length => Handle.UniversalSliceView.Length;
    }
}
