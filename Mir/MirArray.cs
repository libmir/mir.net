
using System.Collections.Generic;

namespace Mir
{
    public sealed class MirArray<T> : MirArrayBase<T>
        where T : unmanaged
    {
        public MirArray()
            : this(default(Native.Handle.RCArray))
        {
        }

        public MirArray(Native.Handle.RCArray array)
            : base(array)
        {
        }

        public MirArray(int length)
            : this(Native.Handle.RCArray.Create<T>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public MirArray(IReadOnlyCollection<T> collection)
            : this(collection.Count)
        {
            AssignCollection(collection);
        }

        public static implicit operator MirArray<T>(T[] array)
        {
            return new MirArray<T>(array);
        }

        public override T this[int index]
        {
            get { return Handle.SliceView.Get<T>(index); }
            set { Handle.SliceView.Get<T>(index) = value; }
        }

        public static MirArray<T> UnsafeMoveFrom(ref Native.Handle.RCArray handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<MirArray<T>, Mir.Native.Handle.RCArray>(ref handle);
        }
    }

    public sealed class MirArray<T, I> : MirArrayBase<T>
        where T : MirWrapper<I>
        where I : unmanaged
    {
        public MirArray(Native.Handle.RCArray array)
            : base(array)
        {
        }

        public MirArray(int length)
            : this(Native.Handle.RCArray.Create<I>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public MirArray(IReadOnlyCollection<T> collection)
            : this(collection.Count)
        {
            AssignCollection(collection);
        }

        public static implicit operator MirArray<T, I>(T[] array)
        {
            return new MirArray<T, I>(array);
        }

        public override T this[int index]
        {
            get { return Handle.SliceView.Get<T, I>(index); }
            set { Handle.SliceView.Set<T, I>(index, value); }
        }

        public static MirArray<T, I> UnsafeMoveFrom(ref Native.Handle.RCArray handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<MirArray<T, I>, Mir.Native.Handle.RCArray>(ref handle);
        }
    }
}
