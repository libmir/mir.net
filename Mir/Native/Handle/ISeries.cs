namespace Mir.Native.Handle
{
    internal interface ISeries : IMirRC
    {
        Series SeriesView { get; }
        Slice KeysView { get; }
        Slice ValuesView { get; }
    }
}
