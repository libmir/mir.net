namespace Mir
{
    public abstract class MirSlimPtr<T> : MirWrapper<Native.Handle.SlimRCPtr>
        where T : MirSlimPtr<T>
    {
        protected MirSlimPtr() : base(default(Native.Handle.SlimRCPtr)){}
        protected MirSlimPtr(Native.Handle.SlimRCPtr handle) : base(handle){}

        public static T UnsafeMoveFrom(ref Native.Handle.SlimRCPtr handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<T, Mir.Native.Handle.SlimRCPtr>(ref handle);
        }
    }
}
