using System;
using System.Runtime.InteropServices;

namespace Mir.Interpolate
{
    public enum SplineType
    {
        C2,
        Cardinal,
        Monotone,
        DoubleQuadratic,
        Akima,
    }

    public enum SplineBoundaryType
    {
        Periodic = -1,
        NotAKnot,
        FirstDerivative,
        SecondDerivative,
        Parabolic,
        Monotone,
        Akima,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SplineBoundaryCondition
    {
        public SplineBoundaryType Type;
        public double Value;
    }
}
