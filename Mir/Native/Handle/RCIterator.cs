using System;
using System.Runtime.InteropServices;

namespace Mir.Native.Handle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RCIterator : IMirRC
    {
        internal IntPtr Iterator;
        private RCArray Array;

        public RCIterator(RCArray array)
        {
            Iterator = array.Payload;
            Array = array;
        }

        public static RCIterator operator +(RCIterator lhs, int shift)
        {
            RCIterator ret;
            ret.Array = lhs.Array;
            ret.Iterator = lhs.Iterator + shift;
            return ret;
        }

        public void IncreaseCounter() => Array.IncreaseCounter();

        public void DecreaseCounter() => Array.DecreaseCounter();
    }
}
