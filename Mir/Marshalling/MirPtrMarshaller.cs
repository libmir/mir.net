using System;
using System.Runtime.InteropServices;

namespace Mir.Marshalling
{
    // Marshalls RCPtr (mir_rcptr) using a SlimRCPtr (mir_slim_rcptr) wrapper as external pointer handle.
    // Should be used striclty with in/out qualifiers.
    public sealed class MirPtrMarshaller<T> : ICustomMarshaler
        where T : MirPtr<T>
    {
        private static readonly ICustomMarshaler Instance = new MirPtrMarshaller<T>();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            var handle = default(Native.Handle.RCPtr);
            if (pNativeData != IntPtr.Zero)
                handle = *(Native.Handle.RCPtr*)pNativeData;
            return Native.MirWrapperManager<T, Native.Handle.RCPtr>.New(handle);
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            switch(ManagedObj)
            {
                case null:
                    return IntPtr.Zero;
                case T value:
                    var impl = value.Handle;
                    impl.IncreaseCounter();
                    return Native.Handle.SlimRCPtr.Create<Native.Handle.RCPtr>(impl).Ptr;
                default:
                    throw new ArgumentException($"Cannot marshal {ManagedObj.GetType().FullName}, expected MirArray<byte>.");
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
