using System;
using System.Runtime.InteropServices;

namespace Mir.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct mir_type_info
    {
        private readonly IntPtr destructor;
        internal readonly int Size;

        internal MirDecreaseCounterDelegate Destructor => destructor == default(IntPtr) ? null : Marshal.GetDelegateForFunctionPointer<MirDecreaseCounterDelegate>(destructor);

        public mir_type_info(MirDecreaseCounterDelegate destructor,  int size)
        {
            this.destructor = destructor is null ? default(IntPtr) : Marshal.GetFunctionPointerForDelegate(destructor);
            this.Size = size;
        }
    }
}
