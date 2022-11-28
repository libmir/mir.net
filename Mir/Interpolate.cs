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
        Makima,
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
        Makima,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SplineBoundaryCondition
    {
        public SplineBoundaryType Type;
        public double Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SplineConfiguration
    {
        public SplineType Kind;
        public SplineBoundaryCondition LeftBoundary;
        public SplineBoundaryCondition RightBoundary;
        public double Param;
    }
}
