using System;
using System.Runtime.InteropServices;

namespace Mir.Marshalling
{
    // The marshaller uses ref-counted arrays of chars (bytes for C#),
    // which don't have trailing zero comparing with common C string.

    // Native Mir zero length arrays are always null.
    // Zero length or null C# string is converted to null Mir array.
    // Null native Mir array is converted to null C# string.
    public sealed class StringMarshaller : ICustomMarshaler
    {
        private static readonly ICustomMarshaler Instance = new StringMarshaller();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
                return "";
            return new MirArray<byte>(new Native.Handle.RCArray(pNativeData)).ToString();
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            S: switch(ManagedObj)
            {
                case null:
                    return IntPtr.Zero;
                case MirArray<byte> array:
                    var impl = array.Handle;
                    impl.IncreaseCounter();
                    return impl.Payload;
                case string str:
                    ManagedObj = str.ToMirArray();
                    goto S;  
                default:
                    throw new ArgumentException($"Cannot marshal {ManagedObj.GetType().FullName}, expected MirArray<byte> or string.");
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
