
using System.Collections.Generic;

namespace Mir
{
    public sealed class Series<K, V> : SeriesBase<K, V>
        where K : unmanaged
        where V : unmanaged
    {
        public Slice<K> Keys { get; }

        public Slice<V> Values { get; }

        public override SliceBase<K> KeysBase => Keys;

        public override SliceBase<V> ValuesBase => Values;

        public Series() : this((IComparer<K>)null) {}

        public Series(IComparer<K> comparer) : this(default(Native.Handle.RCSeries), comparer) {}

        public Series(in Native.Handle.RCSeries impl) : this(impl, null) {}

        public Series(in Native.Handle.RCSeries impl, IComparer<K> comparer)
            : base(impl, comparer)
        {
            Keys = new Slice<K>(Handle.KeysRCView);
            Values = new Slice<V>(Handle.ValuesRCView);
        }

        public Series(MirArray<K> keys, MirArray<V> values)
            : this(new Native.Handle.RCSeries(keys.Handle, values.Handle)) {}

        public unsafe Series(int length)
            : this(Native.Handle.RCSeries.Create<K, V>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public Series(SortedDictionary<K, V> collection)
            : this(collection.Count)
        {
            AssignDictionary(collection);
        }

        public Series(IDictionary<K, V> collection)
            : this(new SortedDictionary<K, V>(collection))
        {
        }

        public Series(IEnumerable<KeyValuePair<K, V>> collection)
            : this(EnumerableToSortedDictionary(collection))
        {
        }

        private static SortedDictionary<K, V> EnumerableToSortedDictionary(IEnumerable<KeyValuePair<K, V>> collection)
        {
            var dictionary = new SortedDictionary<K, V>();
            foreach (var pair in collection)
                dictionary.Add(pair.Key, pair.Value);
            return dictionary;
        }

        public static Series<K, V> UnsafeMoveFrom(ref Native.Handle.RCSeries handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<Series<K, V>, Mir.Native.Handle.RCSeries>(ref handle);
        }
    }

    public sealed class Series<K, V, I> : SeriesBase<K, V>
        where K : unmanaged
        where V : MirWrapper<I>
        where I : unmanaged
    {
        public Slice<K> Keys { get; }

        public Slice<V, I> Values { get; }

        public override SliceBase<K> KeysBase => Keys;

        public override SliceBase<V> ValuesBase => Values;

        public Series() : this((IComparer<K>)null) {}

        public Series(IComparer<K> comparer) : this(default(Native.Handle.RCSeries), comparer) {}

        public Series(in Native.Handle.RCSeries impl) : this(impl, null) {}

        public Series(in Native.Handle.RCSeries impl, IComparer<K> comparer)
            : base(impl, comparer)
        {
            Keys = new Slice<K>(Handle.KeysRCView);
            Values = new Slice<V, I>(Handle.ValuesRCView);
        }

        public Series(MirArray<K> keys, MirArray<V, I> values)
            : this(new Native.Handle.RCSeries(keys.Handle, values.Handle)) {}

        public unsafe Series(int length)
            : this(Native.Handle.RCSeries.Create<K, I>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public Series(SortedDictionary<K, V> collection)
            : this(collection.Count)
        {
            AssignDictionary(collection);
        }

        public Series(IEnumerable<KeyValuePair<K, V>> collection)
            : this(EnumerableToSortedDictionary(collection))
        {
        }

        private static SortedDictionary<K, V> EnumerableToSortedDictionary(IEnumerable<KeyValuePair<K, V>> collection)
        {
            var dictionary = new SortedDictionary<K, V>();
            foreach (var pair in collection)
                dictionary.Add(pair.Key, pair.Value);
            return dictionary;
        }

        public static Series<K, V, I> UnsafeMoveFrom(ref Native.Handle.RCSeries handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<Series<K, V, I>, Mir.Native.Handle.RCSeries>(ref handle);
        }
    }
}
