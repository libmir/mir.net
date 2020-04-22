using System.Collections.Generic;

namespace Mir
{
    public sealed class Slice<T> : SliceBase<T>
        where T : unmanaged
    {
        public Slice()
            : this(default(Native.Handle.RCSlice))
        {
        }

        public Slice(Native.Handle.RCSlice slice)
            : base(slice)
        {
        }

        public unsafe Slice(int length)
            : this(Native.Handle.RCSlice.Create<T>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public Slice(IReadOnlyCollection<T> collection)
            : this(collection.Count)
        {
            AssignCollection(collection);
        }

        public static implicit operator Slice<T>(T[] array)
        {
            return new Slice<T>(array);
        }

        public override T this[int index]
        {
            get { return Handle.SliceView.Get<T>(index); }
            set { Handle.SliceView.Get<T>(index) = value; }
        }

        public static Slice<T> UnsafeMoveFrom(ref Native.Handle.RCSlice handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<Slice<T>, Mir.Native.Handle.RCSlice>(ref handle);
        }
    }


    public sealed class Slice<T, I> : SliceBase<T>
        where T : MirWrapper<I>
        where I : unmanaged
    {
        public Slice()
            : this(default(Native.Handle.RCSlice))
        {
        }

        public Slice(Native.Handle.RCSlice array)
            : base(array)
        {
        }

        public Slice(int length)
            : this(Native.Handle.RCSlice.Create<I>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public Slice(IReadOnlyCollection<T> collection)
            : this(collection.Count)
        {
            AssignCollection(collection);
        }

        public static implicit operator Slice<T, I>(T[] array)
        {
            return new Slice<T, I>(array);
        }

        public override T this[int index]
        {
            get { return Handle.SliceView.Get<T, I>(index); }
            set { Handle.SliceView.Set<T, I>(index, value); }
        }

        public static Slice<T, I> UnsafeMoveFrom(ref Native.Handle.RCSlice handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<Slice<T, I>, Mir.Native.Handle.RCSlice>(ref handle);
        }
    }
}
