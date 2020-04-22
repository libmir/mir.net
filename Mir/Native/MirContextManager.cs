using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mir.Native
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void MirDecreaseCounterDelegate(void* obj);

    internal static class MirContextManager<T>
        where T : unmanaged
    {
        internal static readonly bool IsMirReferenceCounted = MirContextManager.IsMirReferenceCounted(typeof(T));

        internal delegate void ContextAction(in T record);

        internal static readonly ContextAction IncreaseCounter = HandleCounter("IncreaseCounter");

        internal static readonly ContextAction DecreaseCounter = HandleCounter("DecreaseCounter");

        internal static readonly unsafe MirDecreaseCounterDelegate DecreaseCounterDelegate = delegate (void* obj)
        {
            DecreaseCounter(*(T*)obj);
        };

        private static readonly ContextAction DefaultContextAction = (in T record) => {};

        private static ContextAction HandleCounter(string action)
        {
            if (!IsMirReferenceCounted)
                return DefaultContextAction;
            var record = Expression.Parameter(typeof(T).MakeByRefType());
            var list = MirContextManager.InvokeRecursiveImpl(action, typeof(T), record);
            var block = Expression.Block(list);
            return Expression.Lambda<ContextAction>(block, record).Compile();
        }
    }

    internal static class MirContextManager
    {
        private static FieldInfo[] GetAllFieldsBasic(Type type)
        {
            if (type.IsEnum
             || type.IsPrimitive
             || typeof(IMirRC).IsAssignableFrom(type))
                return new FieldInfo[]{};
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        internal static ReflectionFieldInfo[] GetAllFields(Type type)
        {
            return GetAllFieldsBasic(type)
                .Select(fieldInfo => new ReflectionFieldInfo(fieldInfo))
                .ToArray();
        }

        internal static bool IsMirReferenceCounted(Type type)
        {
            if (type.IsEnum || type.IsPrimitive)
                return false;
            if (typeof(IMirRC).IsAssignableFrom(type))
                return true;
            foreach(var field in GetAllFieldsBasic(type))
            {
                if (IsMirReferenceCounted(field.FieldType))
                    return true;
            }
            return false;
        }

        internal static List<Expression> InvokeRecursiveImpl(
            string leafAction,
            Type type,
            Expression record
        )
        {
            var ret = new List<Expression>();
            if (!type.IsEnum && !type.IsPrimitive)
            {
                if (typeof(IMirRC).IsAssignableFrom(type))
                    ret.Add(Expression.Call(record, type.GetMethod(leafAction)));
                else
                    foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                        ret.AddRange(InvokeRecursiveImpl(leafAction, field.FieldType, Expression.Field(record, field)));
            }
            return ret;
        }
    }
}
