using System.Runtime.InteropServices;
using Handle = Mir.Native.Handle;

namespace Mir.Test
{
    // LayoutKind.Sequential is required
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        private int x;
        private int y;
    }

    public class Example1 : MirWrapper<Example1.Impl>
    {
        // Each MirWrapper based class with public/internal Impl struct should define such constructor.
        public Example1(Impl impl) : base (impl) {}

        public struct Impl
        {
            // Native C# type
            public double factor;
            // POD non RC struct
            public Point referenceDate;
            // RC handles
            public Handle.RCArray Points;
            public Handle.RCSlice Numbers;
        }

        // Zero cost access
        public double factor => Handle.factor;

        // Low cost access (zero GC usage).
        public MirArray<Point> Points {
            get { return new MirArray<Point>(Handle.Points); }
            set { SetRCField(value, nameof(Handle.Points)); } 
        }
    }
}
