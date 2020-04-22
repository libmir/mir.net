using System;
using System.Runtime.InteropServices;

namespace Mir.Marshalling
{
    public sealed class MirSlimPtrMarshaller<T> : ICustomMarshaler
        where T : MirSlimPtr<T>, new()
    {
        private static readonly ICustomMarshaler Instance = new MirSlimPtrMarshaller<T>();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == default(IntPtr))
                return null;
            return Native.MirWrapperManager<T, Native.Handle.SlimRCPtr>.New(new Native.Handle.SlimRCPtr(pNativeData));
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            switch(ManagedObj)
            {
                case null:
                    return IntPtr.Zero;
                case MirSlimPtr<T> ptr:
                    var impl = ptr.Handle;
                    impl.IncreaseCounter();
                    return impl.Ptr;
                default:
                    throw new ArgumentException($"Cannot marshal {ManagedObj.GetType().FullName}, expected MirSlimPtr<T>.");
            }
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            new Native.Handle.SlimRCPtr(pNativeData).DecreaseCounter();
        }

        public void CleanUpManagedData(object ManagedObj)
        {
            // Noop
        }

        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }
    }
}
