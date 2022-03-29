using static System.Runtime.InteropServices.UnmanagedType;
using System.Runtime.InteropServices;

namespace Mir.Numeric
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SmileRoots
    {
        public double Left;
        public double Right;

        public SmileRoots(double left)
        {
            Left = left;
            Right = left;
        }

        public SmileRoots(double left, double right)
        {
            Left = left;
            Right = right;
        }

        public double[] ToArray()
        {
            if (Left == Right)
                return new double[] { Left };
            else
                return new double[] { Left, Right };
        }
    }
}
