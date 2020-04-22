
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mir
{
    /**
    D, C++, and C# code must implement the same compression function.
    */
    public abstract class SeriesBase<K, V> : MirWrapper<Native.Handle.RCSeries>, IDictionary<K, V>, IReadOnlyDictionary<K, V>
    {
        private IComparer<K> Comparer { get; }

        public abstract SliceBase<K> KeysBase { get; }

        public abstract SliceBase<V> ValuesBase { get; }

        protected void AssignDictionary(SortedDictionary<K, V> sortedDict)
        {
            var i = 0;
            if (sortedDict == null)
                throw new ArgumentNullException(nameof(sortedDict));
            foreach (var pair in sortedDict)
            {
                KeysBase[i] = pair.Key;
                ValuesBase[i] = pair.Value;
                i++;
            }
        }

        public unsafe SeriesBase(in Native.Handle.RCSeries slice, IComparer<K> comparer = null)
            : base(slice)
        {
            Comparer = comparer ?? Comparer<K>.Default;
        }

        // TODO: move to Handle.Slice for optimisation purpose
        private int TransitionIndex(in K key)
        {
            var first = 0;
            var count = Count;
            while (count != 0)
            {
                var step = count / 2;
                var it = first + step;
                if (Comparer.Compare(KeysBase[it], key) < 0)
                {
                    first = it + 1;
                    count -= step + 1;
                }
                else
                {
                    count = step;
                }
            }
            return first;
        }

        public bool IsReadOnly => true;
    
        public int Count => (int)Handle.ValuesView.Length;
    
        public long LongCount => (long)Handle.ValuesView.Length;

        public bool ContainsKey(K key)
        {
            var ti = TransitionIndex(key);
            if (ti >= Count)
                return false;
            return Comparer.Compare(KeysBase[ti], key) == 0;
        }

        public bool TryGetValue(K key, out V value)
        {
            var ti = TransitionIndex(key);
            if (ti < Count && Comparer.Compare(KeysBase[ti], key) == 0)
            {
                value = ValuesBase[ti];
                return true;
            }
            value = default(V);
            return false;
        }

        public V this[K key]
        {
            get {
                var ti = TransitionIndex(key);
                if (ti < Count && Comparer.Compare(KeysBase[ti], key) == 0)
                    return ValuesBase[ti];
                throw new KeyNotFoundException($"key: {key}");
            }

            set {
                var ti = TransitionIndex(key);
                if (ti < Count && Comparer.Compare(KeysBase[ti], key) == 0)
                    ValuesBase[ti] = value;
                throw new KeyNotFoundException($"key: {key}");
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<K, V>(KeysBase[i], ValuesBase[i]);
            }
        }

        bool ICollection<KeyValuePair<K, V>>.Contains(KeyValuePair<K, V> pair)
        {
            if (TryGetValue(pair.Key, out var value))
                return EqualityComparer<V>.Default.Equals(value, pair.Value);
            return false;
        }

        void ICollection<KeyValuePair<K, V>>.CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            var keys = Handle.KeysView;
            var values = Handle.ValuesView;
            var Len = Count;
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if ((ulong)array.Length < (ulong)arrayIndex + (ulong)Len)
            {
                throw new ArgumentException("The number of elements in the Series is greater than the available space from index to the end of the destination array.", nameof(array));
            }

            for (int i = 0; i < (int)Len; i++)
            {
                array[arrayIndex + i] = new KeyValuePair<K, V>(KeysBase[i], ValuesBase[i]);
            }
        }

        ICollection<K> IDictionary<K, V>.Keys => KeysBase;

        ICollection<V> IDictionary<K, V>.Values => ValuesBase;

        IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => KeysBase;

        IEnumerable<V> IReadOnlyDictionary<K, V>.Values => ValuesBase;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void IDictionary<K, V>.Add(K key, V value)
        {
            throw new NotSupportedException();
        }

        bool IDictionary<K, V>.Remove(K key)
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<K, V>>.Clear()
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<K, V>>.Add(KeyValuePair<K, V> pair)
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<K, V>>.Remove(KeyValuePair<K, V> pair)
        {
            throw new NotSupportedException();
        }
    }
}
