
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mir
{
    public abstract class ArraySliceBase<T, I> : MirWrapper<I>, IList<T>, IReadOnlyList<T>
        where I : unmanaged
    {
        public abstract UIntPtr Length { get; }

        public abstract T this[int index] { get; set; }

        public ArraySliceBase(I impl) : base(impl) {}

        public bool IsReadOnly => true;
        public bool IsFixedSize => true;

        T IReadOnlyList<T>.this[int index] => this[index];

        public int Count => (int) Length;
        public long LongCount => (long) Length;

        protected void AssignCollection(IEnumerable<T> collection)
        {
            int i = 0;
            foreach (var value in collection)
            {
                this[i] = value;
                i++;
            }
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            return Length != UIntPtr.Zero && IndexOf(item) != -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            var len = Length;
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if ((ulong)array.Length < (ulong)arrayIndex + (ulong)len)
            {
                throw new ArgumentException("The number of elements in the ArraySliceBase is greater than the available space from index to the end of the destination array.", nameof(array));
            }

            for (int i = 0; i < (int)len; i++)
            {
                array[arrayIndex + i] = this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }
    }
}
