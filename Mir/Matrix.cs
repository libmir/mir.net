
using System;
using System.Collections.Generic;

namespace Mir
{
    public sealed class Matrix<T> : ArraySliceBase<Slice<T>, Native.Handle.RCMatrix>
        where T : unmanaged
    {
        public sealed override System.UIntPtr Length => Handle.Length;
        public System.UIntPtr RowLength => Handle.RowLength;

        public int Count0 => (int) Length;
        public long LongCount0 => (long) Length;

        public int Count1 => (int) RowLength;
        public long LongCount1 => (long) RowLength;

        public Matrix()
            : this(default(Native.Handle.RCMatrix))
        {
        }

        public Matrix(Native.Handle.RCMatrix slice)
            : base(slice)
        {
        }

        public unsafe Matrix(int rows, int cols)
            : this(Native.Handle.RCMatrix.Create<T>(rows, cols))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public Matrix(T[,] data) : this(data.GetLength(0), data.GetLength(1))
        {
            for (int i = 0; i < Count0; i++)
                for (int j = 0; j < Count1; j++)
                    At(i, j) = data[i, j];
        }

        public static implicit operator Matrix<T>(T[,] data)
        {
            return new Matrix<T>(data);
        }

        public override unsafe Slice<T> this[int index]
        {
            get {
                if (index >= (int)Length)
                    throw new IndexOutOfRangeException();
                return new Slice<T>(new Mir.Native.Handle.RCSlice(RowLength, Handle.Iterator + (int)RowLength * index * sizeof(T)));
            }
            set { throw new NotSupportedException(); }
        }

        public unsafe UniversalSlice<T> Column(int index)
        {
            if (index >= (int)RowLength)
                throw new IndexOutOfRangeException();
            return new UniversalSlice<T>(new Mir.Native.Handle.RCUniversalSlice(Length, (IntPtr)(void*)RowLength, Handle.Iterator + index * sizeof(T)));
        }

        public ref T At(int i, int j) => ref Handle.MatrixView.Get<T>(i, j);
        public T Get(int i, int j) => At(i, j);
        public void Set(int i, int j, T value) => At(i, j) = value;

        public ref T this[ReadOnlySpan<int> dimensions]
        {
            get {
                if (dimensions.Length != 2)
                    throw new ArgumentException("Matrix this[,] requires exactly two dimensions", nameof(dimensions));
                return ref At(dimensions[0], dimensions[1]);
            }
        }

        public static Matrix<T> UnsafeMoveFrom(ref Native.Handle.RCMatrix handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<Matrix<T>, Mir.Native.Handle.RCMatrix>(ref handle);
        }

        public static explicit operator T[,](Matrix<T> data)
        {
            var res = new T[data.Count0, data.Count1];
            data.ToArray(res);
            return res;
        }

        public static explicit operator object[,](Matrix<T> data)
        {
            var res = new object[data.Count0, data.Count1];
            data.ToArray(res);
            return res;
        }

        public void ToArray(T[,] array, int offset0 = 0, int offset1 = 0)
        {
            for (var i = 0; i < Count0; i++)
            for (var j = 0; j < Count1; j++)
                array[i + offset0, j + offset1] = At(i, j);
        }

        public void ToArray(object[,] array, int offset0 = 0, int offset1 = 0)
        {
            for (var i = 0; i < Count0; i++)
                for (var j = 0; j < Count1; j++)
                    array[i + offset0, j + offset1] = At(i, j);
        }
    }

    public sealed class Matrix<T, I> : ArraySliceBase<Slice<T, I>, Native.Handle.RCMatrix>
        where T : MirWrapper<I>
        where I : unmanaged
    {
        public sealed override System.UIntPtr Length => Handle.Length;
        public System.UIntPtr RowLength => Handle.Length;

        public int Count0 => (int) Length;
        public long LongCount0 => (long) Length;

        public int Count1 => (int) RowLength;
        public long LongCount1 => (long) RowLength;

        public Matrix()
            : this(default(Native.Handle.RCMatrix))
        {
        }

        public Matrix(Native.Handle.RCMatrix array)
            : base(array)
        {
        }

        public Matrix(int rows, int cols)
            : this(Native.Handle.RCMatrix.Create<I>(rows, cols))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public Matrix(T[,] data) : this(data.GetLength(0), data.GetLength(1))
        {
            for (int i = 0; i < Count0; i++)
                for (int j = 0; j < Count1; j++)
                    Set(i, j, data[i, j]);
        }

        public static implicit operator Matrix<T, I>(T[,] data)
        {
            return new Matrix<T, I>(data);
        }

        unsafe public override Slice<T, I> this[int index]
        {
            get {
                if (index >= (int)Length)
                    throw new IndexOutOfRangeException();
                return new Slice<T, I>(new Mir.Native.Handle.RCSlice(RowLength, Handle.Iterator + (int)RowLength * index * sizeof(I)));
            }
            set { throw new NotSupportedException(); }
        }

        public unsafe UniversalSlice<T, I> Column(int index)
        {
            if (index >= (int)RowLength)
                throw new IndexOutOfRangeException();
            return new UniversalSlice<T, I>(new Mir.Native.Handle.RCUniversalSlice(Length, (IntPtr)(void*)RowLength, Handle.Iterator + index * sizeof(I)));
        }

        public T Get(int i, int j) => Handle.MatrixView.Get<T, I>(i, j);
        public void Set(int i, int j, T value) => Handle.MatrixView.Set<T, I>(i, j, value);

        public T this[ReadOnlySpan<int> dimensions]
        {
            get {
                if (dimensions.Length != 2)
                    throw new ArgumentException("Matrix this[,] requires exactly two dimensions", nameof(dimensions));
                return Get(dimensions[0], dimensions[1]);
            }
            set {
                if (dimensions.Length != 2)
                    throw new ArgumentException("Matrix this[,] requires exactly two dimensions", nameof(dimensions));
                Set(dimensions[0], dimensions[1], value);
            }
        }

        public static Matrix<T, I> UnsafeMoveFrom(ref Native.Handle.RCMatrix handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<Matrix<T, I>, Mir.Native.Handle.RCMatrix>(ref handle);
        }
    }
}
