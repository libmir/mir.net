using System;
using System.Collections.Generic;
using System.Linq;

namespace Mir
{
    public class MirByteArrayComparer :  IComparer<MirArray<byte>>
    {
        public int Compare(MirArray<byte> a, MirArray<byte> b)
        {
            var length = a.Count < b.Count ? a.Count : b.Count;
            for (var i = 0; i < length; i++)
            {
                var d = (int)a[i] - (int)b[i];
                if (d != 0)
                    return d;
            }
            return a.Count - b.Count;
        }

        public static readonly IComparer<MirArray<byte>> Instance = new MirByteArrayComparer();
    }

    public sealed class StringSeries<V> : SeriesBase<MirArray<byte>, V>, IDictionary<string, V>, IReadOnlyDictionary<string, V>
        where V : unmanaged
    {
        public Slice<MirArray<byte>, Native.Handle.RCArray> Keys { get; }

        public Slice<V> Values { get; }

        public override SliceBase<MirArray<byte>> KeysBase => Keys;

        public override SliceBase<V> ValuesBase => Values;

        private StringSeries() : this(MirByteArrayComparer.Instance) {}

        private StringSeries(IComparer<MirArray<byte>> comparer) : this(default(Native.Handle.RCSeries), comparer) {}

        private StringSeries(in Native.Handle.RCSeries impl)
            : this(impl, MirByteArrayComparer.Instance) {}

        private StringSeries(in Native.Handle.RCSeries impl, IComparer<MirArray<byte>> comparer)
            : base(impl, comparer)
        {
            Keys = new Slice<MirArray<byte>, Native.Handle.RCArray>(Handle.KeysRCView);
            Values = new Slice<V>(Handle.ValuesRCView);
        }

        public unsafe StringSeries(int length, IComparer<MirArray<byte>> comparer)
            : this(Native.Handle.RCSeries.Create<Native.Handle.RCArray, V>(length), comparer)
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public StringSeries(IDictionary<string, V> dict)
            : this(dict, MirByteArrayComparer.Instance)
        {
        }

        public StringSeries(IDictionary<string, V> dict, IComparer<MirArray<byte>> comparer)
            : this(dict.Count, comparer)
        {
            var collection = new SortedDictionary<MirArray<byte>, V>(comparer);
            foreach (var kv in dict)
                collection.Add(kv.Key.ToMirArray(), kv.Value);
            AssignDictionary(collection);
        }

        public StringSeries(SortedDictionary<MirArray<byte>, V> collection)
            : this(collection, MirByteArrayComparer.Instance)
        {
        }

        public StringSeries(SortedDictionary<MirArray<byte>, V> collection, IComparer<MirArray<byte>> comparer)
            : this(collection.Count, comparer)
        {
            AssignDictionary(collection);
        }

        public bool ContainsKey(string key)
        {
            return ContainsKey(key.ToMirArray());
        }

        public bool TryGetValue(string key, out V value)
        {
            return TryGetValue(key.ToMirArray(), out value);
        }

        public V this[string key]
        {
            get {
                return this[key.ToMirArray()];
            }

            set {
                this[key.ToMirArray()] = value;
            }
        }

        ICollection<string> IDictionary<string, V>.Keys => Keys.Select(ar => ar.ToString()).ToList();
        
        IEnumerable<string> IReadOnlyDictionary<string, V>.Keys => Keys.Select(ar => ar.ToString());

        ICollection<V> IDictionary<string, V>.Values => Values;

        IEnumerable<V> IReadOnlyDictionary<string, V>.Values => Values;

        IEnumerator<KeyValuePair<string, V>> IEnumerable<KeyValuePair<string, V>>.GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<string, V>(KeysBase[i].ToString(), ValuesBase[i]);
            }
        }

        bool ICollection<KeyValuePair<string, V>>.Contains(KeyValuePair<string, V> pair)
        {
            return (this as ICollection<KeyValuePair<MirArray<byte>, V>>).Contains(new KeyValuePair<MirArray<byte>, V>(pair.Key.ToMirArray(), pair.Value));
        }

        void ICollection<KeyValuePair<string, V>>.CopyTo(KeyValuePair<string, V>[] array, int arrayIndex)
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
                array[arrayIndex + i] = new KeyValuePair<string, V>(Keys[i].ToString(), ValuesBase[i]);
            }
        }

        void IDictionary<string, V>.Add(string key, V value)
        {
            throw new NotSupportedException();
        }

        bool IDictionary<string, V>.Remove(string key)
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<string, V>>.Clear()
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<string, V>>.Add(KeyValuePair<string, V> pair)
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<string, V>>.Remove(KeyValuePair<string, V> pair)
        {
            throw new NotSupportedException();
        }
    }
}