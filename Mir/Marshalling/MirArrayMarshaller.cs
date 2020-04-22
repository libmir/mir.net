using System;
using System.Runtime.InteropServices;

namespace Mir.Marshalling
{
    // Native Mir zero length arrays are always null.
    // Empty or null C# array is converted to null Mir array.
    // Null native Mir array is converted to null C# MirArray.
    public sealed class MirArrayMarshaller<T> : ICustomMarshaler
        where T : unmanaged
    {
        private static readonly ICustomMarshaler Instance = new MirArrayMarshaller<T>();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return new MirArray<T>(new Native.Handle.RCArray(pNativeData));
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            switch(ManagedObj)
            {
                case null:
                    return IntPtr.Zero;
                case MirArray<T> array:
                    var impl = array.Handle;
                    impl.IncreaseCounter();
                    return impl.Payload;
                default:
                    throw new ArgumentException($"Cannot marshal {ManagedObj.GetType().FullName}, expected MirArray<byte>.");
            }
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            new Native.Handle.RCArray(pNativeData).DecreaseCounter();
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

    public sealed class MirArrayMarshaller<T, I> : ICustomMarshaler
        where T : MirWrapper<I>
        where I : unmanaged
    {
        private static readonly ICustomMarshaler Instance = new MirArrayMarshaller<T, I>();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return new MirArray<T, I>(new Native.Handle.RCArray(pNativeData));
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            switch(ManagedObj)
            {
                case null:
                    return IntPtr.Zero;
                case MirArray<T, I> array:
                    var impl = array.Handle;
                    impl.IncreaseCounter();
                    return impl.Payload;
                default:
                    throw new ArgumentException($"Cannot marshal {ManagedObj.GetType().FullName}, expected MirArray<byte>.");
            }
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            new Native.Handle.RCArray(pNativeData).DecreaseCounter();
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
