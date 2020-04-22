using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mir.Native
{
    internal static class MirWrapperManager<T, I>
        where T : MirWrapper<I>
        where I : unmanaged
    {
        internal delegate T Constructor(in I record);

        internal static readonly Constructor New = HandleNew();

        private static Constructor HandleNew()
        {
            var param = Expression.Parameter(typeof(I).MakeByRefType());
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            // First try to find a constructor taking `I` by `ref` or `in`, then fall back to a constructor taking `I` by value
            var constr = typeof(T).GetConstructor(bindingFlags, null, new[] {typeof(I).MakeByRefType()}, null) ??
                         typeof(T).GetConstructor(bindingFlags, null, new[] {typeof(I)}, null);
            if(constr is null)
                throw new Exception($"Missing constructor: `{typeof(T).Name}(in {typeof(I).Name} impl);` or `{typeof(T).Name}({typeof(I).Name} impl);`");
            var newexpr = Expression.New(constr, param);
            return Expression.Lambda<Constructor>(newexpr, param).Compile();
        }
    }
}
