using System;
using System.Reflection;
using System.Linq.Expressions;

namespace Mir.Native
{
    internal struct ReflectionFieldInfo
    {
        internal readonly string Name;
        private readonly Delegate Getter;
        private readonly Delegate Setter;

        internal I GetField<T, I>(T record)
            where T : unmanaged
            where I : unmanaged
        {
            var getter = (GetterFun<T, I>)Getter;
            var ret = getter(record);
            return ret;
        }
        
        internal void SetField<T, I>(ref T record, I field)
            where T : unmanaged
            where I : unmanaged
        {
            MirContextManager<I>.IncreaseCounter(field);
            var tf = GetField<T, I>(record);
            MirContextManager<I>.DecreaseCounter(tf);
            var setter = (SetterFun<T, I>)Setter;
            setter(ref record, field);
        }

        public delegate I GetterFun<T, I>(in T record)
            where I : unmanaged;

        public delegate void SetterFun<T, I>(ref T record, in I field)
            where I : unmanaged;

        public static GetterFun<T, I> GetFieldImpl<T, I>(string fieldName)
            where I : unmanaged
        {
            var record = Expression.Parameter(typeof(T).MakeByRefType(), "record");
            return Expression.Lambda<GetterFun<T, I>>
                (Expression.Field(record, typeof(T), fieldName), record)
                .Compile();
        }

        public static SetterFun<T, I> SetFieldImpl<T, I>(string fieldName)
            where I : unmanaged
        {
            var record = Expression.Parameter(typeof(T).MakeByRefType(), "record");
            var field = Expression.Parameter(typeof(I).MakeByRefType(), "value");
            var fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo is null)
                throw new Exception($"{typeof(T)} doesn't contain field '{fieldName}'");
            return Expression.Lambda<SetterFun<T, I>>
                (Expression.Assign(Expression.Field(record, fieldInfo), field), record, field)
                .Compile();
        }

        internal ReflectionFieldInfo(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            object[] args = {Name};
            Getter = (Delegate)typeof(ReflectionFieldInfo).GetMethod("GetFieldImpl").MakeGenericMethod(fieldInfo.DeclaringType, fieldInfo.FieldType).Invoke(null, args);
            Setter = (Delegate)typeof(ReflectionFieldInfo).GetMethod("SetFieldImpl").MakeGenericMethod(fieldInfo.DeclaringType, fieldInfo.FieldType).Invoke(null, args);
        }
    }
}
