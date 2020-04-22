namespace Mir.Native.Handle
{
    public interface ISlice : IUniversalSlice
    {
        Slice SliceView { get; }
    }
}
