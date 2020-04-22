namespace Mir.Native.Handle
{
    internal interface IMatrix : IMirRC
    {
        Matrix MatrixView { get; }
    }
}
