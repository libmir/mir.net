namespace Mir
{
    public abstract class MirPtr<T> : MirWrapper<Native.Handle.RCPtr>
        where T : MirPtr<T>
    {
        protected MirPtr() : base(default(Native.Handle.RCPtr)){}
        protected MirPtr(Native.Handle.RCPtr handle) : base(handle){}

        public static T UnsafeMoveFrom(ref Native.Handle.RCPtr handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<T, Mir.Native.Handle.RCPtr>(ref handle);
        }
    }
}
