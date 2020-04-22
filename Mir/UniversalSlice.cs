using System.Collections.Generic;

namespace Mir
{
    public sealed class UniversalSlice<T> : UniversalSliceBase<T>
        where T : unmanaged
    {
        public UniversalSlice()
            : this(default(Native.Handle.RCUniversalSlice))
        {
        }

        public UniversalSlice(Native.Handle.RCUniversalSlice slice)
            : base(slice)
        {
        }

        public unsafe UniversalSlice(int length)
            : this(Native.Handle.RCUniversalSlice.Create<T>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public UniversalSlice(IReadOnlyCollection<T> collection)
            : this(collection.Count)
        {
            AssignCollection(collection);
        }

        public UniversalSlice(Slice<T> slice)
            : this(new Native.Handle.RCUniversalSlice(slice.Handle))
        {
        }

        public static implicit operator UniversalSlice<T>(Slice<T> slice)
        {
            return new UniversalSlice<T>(slice);
        }

        public override T this[int index]
        {
            get { return Handle.UniversalSliceView.Get<T>(index); }
            set { Handle.UniversalSliceView.Get<T>(index) = value; }
        }

        public static UniversalSlice<T> UnsafeMoveFrom(ref Native.Handle.RCUniversalSlice handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<UniversalSlice<T>, Mir.Native.Handle.RCUniversalSlice>(ref handle);
        }
    }


    public sealed class UniversalSlice<T, I> : UniversalSliceBase<T>
        where T : MirWrapper<I>
        where I : unmanaged
    {
        public UniversalSlice()
            : this(default(Native.Handle.RCUniversalSlice))
        {
        }

        public UniversalSlice(Native.Handle.RCUniversalSlice array)
            : base(array)
        {
        }

        public UniversalSlice(int length)
            : this(Native.Handle.RCUniversalSlice.Create<I>(length))
        {
            // base class calls IncreaseCounter in constructor
            Handle.DecreaseCounter();
        }

        public UniversalSlice(IReadOnlyCollection<T> collection)
            : this(collection.Count)
        {
            AssignCollection(collection);
        }

        public UniversalSlice(Slice<T, I> slice)
            : this(new Native.Handle.RCUniversalSlice(slice.Handle)){}

        public static implicit operator UniversalSlice<T, I>(Slice<T, I> slice)
        {
            return new UniversalSlice<T, I>(slice);
        }

        public override T this[int index]
        {
            get { return Handle.UniversalSliceView.Get<T, I>(index); }
            set { Handle.UniversalSliceView.Set<T, I>(index, value); }
        }

        public static UniversalSlice<T, I> UnsafeMoveFrom(ref Native.Handle.RCUniversalSlice handle)
        {
            return MirExtensionMethods.UnsafeMoveFrom<UniversalSlice<T, I>, Mir.Native.Handle.RCUniversalSlice>(ref handle);
        }
    }
}
