using System;
using System.Runtime.InteropServices;

// Marshalls RCSeries (mir_series based on mir_rci iterators) using a SlimRCPtr (mir_slim_rcptr) wrapper as external pointer handle.
// Should be used striclty with in/out qualifiers.
namespace Mir.Marshalling
{
    public sealed class MirSeriesMarshaller<K, V> : ICustomMarshaler
        where K : unmanaged
        where V : unmanaged
    {
        private static readonly ICustomMarshaler Instance = new MirSeriesMarshaller<K, V>();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            var handle = default(Native.Handle.RCSeries);
            if (pNativeData != IntPtr.Zero)
                handle = *(Native.Handle.RCSeries*)pNativeData;
            return new Series<K, V>(handle);
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            switch(ManagedObj)
            {
                case null:
                    return IntPtr.Zero;
                case Series<K, V> value:
                    var impl = value.Handle;
                    impl.IncreaseCounter();
                    return Native.Handle.SlimRCPtr.Create<Native.Handle.RCSeries>(impl).Ptr;
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

    public sealed class MirSeriesMarshaller<K, V, I> : ICustomMarshaler
        where K : unmanaged
        where V : MirWrapper<I>
        where I : unmanaged
    {
        private static readonly ICustomMarshaler Instance = new MirSeriesMarshaller<K, V, I>();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            var handle = default(Native.Handle.RCSeries);
            if (pNativeData != IntPtr.Zero)
                handle = *(Native.Handle.RCSeries*)pNativeData;
            return new Series<K, V, I>(handle);
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            switch(ManagedObj)
            {
                case null:
                    return IntPtr.Zero;
                case Series<K, V, I> value:
                    var impl = value.Handle;
                    impl.IncreaseCounter();
                    return Native.Handle.SlimRCPtr.Create<Native.Handle.RCSeries>(impl).Ptr;
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
