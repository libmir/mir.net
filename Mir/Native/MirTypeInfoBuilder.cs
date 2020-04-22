using System;
using System.Runtime.InteropServices;

namespace Mir.Native
{
    public static class MirTypeInfoBuilder<T>
        where T : unmanaged
    {
        internal static readonly IntPtr Info;
        // Should be global static data
        static unsafe MirTypeInfoBuilder()
        {
            Info = Marshal.AllocHGlobal(sizeof(mir_type_info));
            *(mir_type_info*)Info = new mir_type_info(MirContextManager<T>.IsMirReferenceCounted ? MirContextManager<T>.DecreaseCounterDelegate : default(MirDecreaseCounterDelegate), sizeof(T));
        }
    }
}
