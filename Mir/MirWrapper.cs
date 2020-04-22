using System;
using System.Collections.Generic;
using System.Linq;

namespace Mir
{
    public class MirWrapper<T>
        where T: unmanaged
    {
        public T UnsafeHandle;
        public ref readonly T Handle => ref UnsafeHandle;

        protected MirWrapper(in T unsafeHandle)
        {
            UnsafeHandle = unsafeHandle;
            Native.MirContextManager<T>.IncreaseCounter(UnsafeHandle);
        }

        protected void SetField<I>(in I field, string name)
            where I : unmanaged
        {
            FieldsInfo[indexOf(name)].SetField<T, I>(ref UnsafeHandle, field);
        }

        protected void SetRCField<I>(MirWrapper<I> field, string name)
            where I : unmanaged
        {
            SetField(field.Handle, name);
        }

        ~MirWrapper()
        {
            Native.MirContextManager<T>.DecreaseCounter(UnsafeHandle);
        }

        private static readonly Native.ReflectionFieldInfo[] FieldsInfo = Native.MirContextManager.GetAllFields(typeof(T));

        private static int indexOf(string fieldName)
        {
            for (var i = 0; i < FieldsInfo.Length; i++)
                if (FieldsInfo[i].Name == fieldName)
                    return i;
            throw new KeyNotFoundException($"Unmanaged type {typeof(T)} doesn't contain Mir reference counted field '{fieldName}. Available fields: {String.Join(",", FieldsInfo.Select(field => field.Name))}.");
        }
    }
}
