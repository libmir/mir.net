using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Mir
{
    public static class MirExtensionMethods
    {
        public static T UnsafeMoveFrom<T, I>(ref I handle)
            where T : MirWrapper<I>
            where I : unmanaged, Native.IMirRC
        {
            object[] parameters = new object[]{handle};
            var ret = Native.MirWrapperManager<T, I>.New(handle);
            handle.DecreaseCounter();
            handle = default(I);
            return ret;
        }

        public static MirArray<T> ToMirArray<T>(this IReadOnlyCollection<T> collection)
            where T : unmanaged
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            return new MirArray<T>(collection);
        }

        public static MirArray<T, Native.Handle.RCPtr> ToMirArrayOfPointers<T>(this IReadOnlyCollection<T> collection)
            where T : MirPtr<T>
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            return new MirArray<T, Native.Handle.RCPtr>(collection);
        }

        public static MirArray<T, Native.Handle.SlimRCPtr> ToMirArrayOfSlimPointers<T>(this IReadOnlyCollection<T> collection)
            where T : MirSlimPtr<T>
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            return new MirArray<T, Native.Handle.SlimRCPtr>(collection);
        }

        public static Slice<T> ToSlice<T>(this IReadOnlyCollection<T> collection)
            where T : unmanaged
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            return new Slice<T>(collection);
        }

        public static MirArray<byte> ToMirArray(this string str)
        {
            if (str is null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length == 0)
                return new MirArray<byte>();
            return new MirArray<byte>(Encoding.UTF8.GetBytes(str));
        }

        public static Slice<byte> ToSlice(this string str)
        {
            if (str is null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length == 0)
                return new Slice<byte>();
            return new Slice<byte>(Encoding.UTF8.GetBytes(str));
        }

        public static StringSeries<V> ToSeries<V>(this IDictionary<string, V> dict)
            where V : unmanaged
        {
            return new StringSeries<V>(dict);
        }

        public static Series<K, V> ToSeries<K, V>(this SortedDictionary<K, V> dict)
            where K : unmanaged
            where V : unmanaged
        {
            return new Series<K, V>(dict);
        }

        public static Series<K, V> ToSeries<K, V>(this IDictionary<K, V> dict)
            where K : unmanaged
            where V : unmanaged
        {
            return ToSeries(new SortedDictionary<K, V>(dict));
        }

        public static Series<K, V> ToSeries<K, V>(this IEnumerable<KeyValuePair<K, V>> dict)
            where K : unmanaged
            where V : unmanaged
        {
            return new Series<K, V>(dict);
        }

        public static Series<K, V, I> ToSeries<K, V, I>(this SortedDictionary<K, V> dict)
            where K : unmanaged
            where I : unmanaged
            where V : MirWrapper<I>
        {
            return new Series<K, V, I>(dict);
        }

        public static Series<K, V, I> ToSeries<K, V, I>(this IDictionary<K, V> dict)
            where K : unmanaged
            where I : unmanaged
            where V : MirWrapper<I>
        {
            return ToSeries<K, V, I>(new SortedDictionary<K, V>(dict));
        }

        public static Series<K, V, I> ToSeries<K, V, I>(this IEnumerable<KeyValuePair<K, V>> dict)
            where K : unmanaged
            where I : unmanaged
            where V : MirWrapper<I>
        {
            return new Series<K, V, I>(dict);
        }

        public static Series<K, V> ToSeries<K, V>(this IReadOnlyCollection<K> keys, IReadOnlyCollection<V> values)
            where K : unmanaged
            where V : unmanaged
        {
            if (keys is null)
                throw new ArgumentNullException(nameof(keys));
            if (values is null)
                throw new ArgumentNullException(nameof(values));
            if (keys.Count != values.Count)
                throw new ArgumentException("keys.Count should be equal to values.Count");
            return ToSeries<K, V>(keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v));
        }

        public static Series<K, V, I> ToSeries<K, V, I>(this IReadOnlyCollection<K> keys, IReadOnlyCollection<V> values)
            where K : unmanaged
            where I : unmanaged
            where V : MirWrapper<I>
        {
            if (keys is null)
                throw new ArgumentNullException(nameof(keys));
            if (values is null)
                throw new ArgumentNullException(nameof(values));
            if (keys.Count != values.Count)
                throw new ArgumentException("keys.Count should be equal to values.Count");
            return ToSeries<K, V, I>(keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v));
        }

        public static void Assign<T, L, R>(this ArraySliceBase<T, L> lhs, ArraySliceBase<T, R> rhs)
            where L : unmanaged, Native.Handle.IUniversalSlice
            where R : unmanaged, Native.Handle.IUniversalSlice
        {
            if (lhs.Count != rhs.Count)
                throw new ArgumentException("'lhs' and 'rhs' must have the same length");
            for(int i = 0; i < lhs.Count; i++)
                lhs[i] = rhs[i];
        }

        public static void Assign<T>(this Matrix<T> lhs, Matrix<T> rhs)
            where T : unmanaged
        {
            if (lhs.Count0 != rhs.Count0 || lhs.Count1 != rhs.Count1)
                throw new ArgumentException("'lhs' and 'rhs' must have the same shape");
            for(int i = 0; i < lhs.Count0; i++)
            for(int j = 0; j < lhs.Count1; j++)
                lhs.At(i, j) = rhs.At(i, j);
        }

        public static void Assign<T, I>(this Matrix<T, I> lhs, Matrix<T, I> rhs)
            where T : MirWrapper<I>
            where I : unmanaged
        {
            if (lhs.Count0 != rhs.Count0 || lhs.Count1 != rhs.Count1)
                throw new ArgumentException("'lhs' and 'rhs' must have the same shape");
            for(int i = 0; i < lhs.Count0; i++)
            for(int j = 0; j < lhs.Count1; j++)
                lhs.Set(i, j, rhs.Get(i, j));
        }

        public static void AddAssign(this Matrix<double> lhs, Matrix<double> rhs)
        {
            if (lhs.Count0 != rhs.Count0 || lhs.Count1 != rhs.Count1)
                throw new ArgumentException("'lhs' and 'rhs' must have the same shape");
            for(int i = 0; i < lhs.Count0; i++)
            for(int j = 0; j < lhs.Count1; j++)
                lhs.At(i, j) += rhs.At(i, j);
        }

        public static void AddAssign(this Matrix<Complex> lhs, Matrix<Complex> rhs)
        {
            if (lhs.Count0 != rhs.Count0 || lhs.Count1 != rhs.Count1)
                throw new ArgumentException("'lhs' and 'rhs' must have the same shape");
            for(int i = 0; i < lhs.Count0; i++)
            for(int j = 0; j < lhs.Count1; j++)
                lhs.At(i, j) += rhs.At(i, j);
        }

        public static UniversalSlice<T> Diagonal<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var length = (UIntPtr) Math.Min(matrix.Count0, matrix.Count1);
            var stride = (IntPtr) (matrix.Count1 + 1);
            return new UniversalSlice<T>(new Native.Handle.RCUniversalSlice(length, stride, matrix.Handle.Iterator));
        }

        public static Matrix<T> Transpose<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var ret = new Matrix<T>(matrix.Count1, matrix.Count0);
            for(int i = 0; i < matrix.Count0; i++)
            for(int j = 0; j < matrix.Count1; j++)
                ret.At(j, i) = matrix.At(i, j);
            return ret;
        }

        public static Slice<T> Flattened<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            return new Slice<T>(new Native.Handle.RCSlice((UIntPtr)(matrix.Count0 * matrix.Count1), matrix.Handle.Iterator));
        }

        public static Slice<T, I> Flattened<T, I>(this Matrix<T, I> matrix)
            where T : MirWrapper<I>
            where I : unmanaged
        {
            return new Slice<T, I>(new Native.Handle.RCSlice((UIntPtr)(matrix.Count0 * matrix.Count1), matrix.Handle.Iterator));
        }

        public static Matrix<T> Sliced<T>(this Slice<T> slice, int rows, int cols)
            where T : unmanaged
        {
            if (slice.Count != rows * cols)
                throw new ArgumentException("Wrong shape");
            return new Matrix<T>(new Native.Handle.RCMatrix((UIntPtr)rows, (UIntPtr)cols, slice.Handle.Iterator));
        }

        public static Matrix<T, I> Sliced<T, I>(this Slice<T, I> slice, int rows, int cols)
            where T : MirWrapper<I>
            where I : unmanaged
        {
            if (slice.Count != rows * cols)
                throw new ArgumentException("Wrong shape");
            return new Matrix<T, I>(new Native.Handle.RCMatrix((UIntPtr)rows, (UIntPtr)cols, slice.Handle.Iterator));
        }

        public static Matrix<T> asRowMatrix<T>(this Slice<T> slice)
            where T : unmanaged
        {
            return Sliced(slice, 1, slice.Count);
        }

        public static Matrix<T> asColumnMatrix<T>(this Slice<T> slice)
            where T : unmanaged
        {
            return Sliced(slice, slice.Count, 1);
        }

        public static Matrix<T> ToDiagonalMatrix<T>(this Slice<T> slice)
            where T : unmanaged
        {
            return ToDiagonalMatrix(slice, slice.Count, slice.Count);
        }

        public static Matrix<T> ToDiagonalMatrix<T>(this Slice<T> slice, int rows, int cols)
            where T : unmanaged
        {
            if (slice.Count != Math.Min(rows, cols))
                throw new ArgumentException("Wrong shape");
            var ret = new Matrix<T>(rows, cols);
            ret.Diagonal().Assign(slice);
            return ret;
        }

        public static Matrix<R> Map<T, R>(this Matrix<T> matrix, Func<T, R> func)
            where T : unmanaged
            where R : unmanaged
        {
            var ret = new Matrix<R>(matrix.Count0, matrix.Count1);
            for(int i = 0; i < matrix.Count0; i++)
            for(int j = 0; j < matrix.Count1; j++)
                ret.At(i, j) = func(matrix.At(i, j));
            return ret;
        }

        public static Matrix<R> Map<T1, T2, R>(this Matrix<T1> matrix1, Matrix<T2> matrix2, Func<T1, T2, R> func)
            where T1 : unmanaged
            where T2 : unmanaged
            where R : unmanaged
        {
            if (matrix1.Count0 != matrix2.Count0 || matrix1.Count1 != matrix2.Count1)
                throw new ArgumentException("'matrix1' and 'matrix2' must have the same shape");
            var ret = new Matrix<R>(matrix1.Count0, matrix1.Count1);
            for(int i = 0; i < matrix1.Count0; i++)
            for(int j = 0; j < matrix1.Count1; j++)
                ret.At(i, j) = func(matrix1.At(i, j), matrix2.At(i, j));
            return ret;
        }

        public static void Each<T>(this Matrix<T> matrix, Action<T> func)
            where T : unmanaged
        {
            for(int i = 0; i < matrix.Count0; i++)
            for(int j = 0; j < matrix.Count1; j++)
                func(matrix.At(i, j));
        }

        public static void Each<T1, T2>(this Matrix<T1> matrix1, Matrix<T2> matrix2, Action<T1, T2> func)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            if (matrix1.Count0 != matrix2.Count0 || matrix1.Count1 != matrix2.Count1)
                throw new ArgumentException("'matrix1' and 'matrix2' must have the same shape");
            for(int i = 0; i < matrix1.Count0; i++)
            for(int j = 0; j < matrix1.Count1; j++)
                func(matrix1.At(i, j), matrix2.At(i, j));
        }

        public static void Each<T, I>(this ArraySliceBase<T, I> slice, Action<T> func)
            where I : unmanaged
        {
            for(int i = 0; i < slice.Count; i++)
                func(slice[i]);
        }

        public static void Each<T1, T2, I1, I2>(this ArraySliceBase<T1, I1> slice1, ArraySliceBase<T2, I2> slice2, Action<T1, T2> func)
            where I1 : unmanaged
            where I2 : unmanaged
        {
            if (slice1.Count != slice2.Count)
                throw new ArgumentException("'slice1' and 'slice2' must have the same shape");
            for(int i = 0; i < slice1.Count; i++)
                func(slice1[i], slice2[i]);
        }

        public static Slice<Slice<T>, Native.Handle.RCSlice> Rows<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            return new Slice<Slice<T>, Native.Handle.RCSlice>(matrix);
        }

        public static Slice<UniversalSlice<T>, Native.Handle.RCUniversalSlice> Columns<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var ret = new Slice<UniversalSlice<T>, Native.Handle.RCUniversalSlice>(matrix.Count0);
            for (int i = 0; i < ret.Count; i++)
                ret[i] = matrix.Column(i);
            return ret;
        }

        public static unsafe UniversalSlice<T> Reversed<T>(this UniversalSlice<T> slice)
            where T : unmanaged
        {
            if (slice.Count <= 1)
                return slice;
            return new UniversalSlice<T>(new Native.Handle.RCUniversalSlice(
                slice.Length,
                (IntPtr)(-(int)slice.Handle.Stride),
                slice.Handle.Iterator + (slice.Count - 1) * (int)slice.Handle.Stride * sizeof(T)));
        }
    }
}
