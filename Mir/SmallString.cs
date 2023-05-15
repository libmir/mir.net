using System;
using System.Runtime.InteropServices;

namespace Mir
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallString2 : IComparable<SmallString2>, IEquatable<SmallString2>
    {
        private const int MaxLength = 2;
        private unsafe fixed byte payload_[MaxLength];

        public unsafe SmallString2(string value)
        {
            fixed (byte* n = payload_)
                SmallStringMethods.FromString(n, MaxLength, value);
        }

        public static implicit operator SmallString2(string s)
        {
            return new SmallString2(s);
        }

        public static implicit operator string(in SmallString2 s)
        {
            return s.ToString();
        }

        public static unsafe implicit operator bool(in SmallString2 value)
        {
            fixed (byte* n = value.payload_)
                return !SmallStringMethods.IsNull(n, MaxLength);
        }

        public static unsafe bool TryConvert(string value, out SmallString2 s)
        {
            fixed (byte* n = s.payload_)
                return SmallStringMethods.TryFromString(n, MaxLength, value);
        }

        /* Implements the same order as in D */
        public unsafe int CompareTo(SmallString2 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Compare(n, m, MaxLength);
        }

        public unsafe bool Equals(SmallString2 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Equals(n, m, MaxLength);
        }

        public static bool operator ==(in SmallString2 lhs, in SmallString2 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in SmallString2 lhs, in SmallString2 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public unsafe bool EqualsIgnoreCase(SmallString2 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.EqualsIgnoreCase(n, m, MaxLength);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() == GetType())
                return Equals((SmallString2) obj);
            if (obj.GetType() == "".GetType())
                return (string) this == (string) obj;
            return false;
        }

        public override unsafe string ToString()
        {
            fixed (byte* n = payload_)
                return SmallStringMethods.ToString(n, MaxLength);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallString3 : IComparable<SmallString3>, IEquatable<SmallString3>
    {
        private const int MaxLength = 3;
        private unsafe fixed byte payload_[MaxLength];

        public unsafe SmallString3(string value)
        {
            fixed (byte* n = payload_)
                SmallStringMethods.FromString(n, MaxLength, value);
        }

        public static implicit operator SmallString3(string s)
        {
            return new SmallString3(s);
        }

        public static implicit operator string(in SmallString3 s)
        {
            return s.ToString();
        }

        public static unsafe implicit operator bool(in SmallString3 value)
        {
            fixed (byte* n = value.payload_)
                return !SmallStringMethods.IsNull(n, MaxLength);
        }

        public static unsafe bool TryConvert(string value, out SmallString3 s)
        {
            fixed (byte* n = s.payload_)
                return SmallStringMethods.TryFromString(n, MaxLength, value);
        }

        /* Implements the same order as in D */
        public unsafe int CompareTo(SmallString3 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Compare(n, m, MaxLength);
        }

        public unsafe bool Equals(SmallString3 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Equals(n, m, MaxLength);
        }

        public static bool operator ==(in SmallString3 lhs, in SmallString3 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in SmallString3 lhs, in SmallString3 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public unsafe bool EqualsIgnoreCase(SmallString3 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.EqualsIgnoreCase(n, m, MaxLength);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() == GetType())
                return Equals((SmallString3) obj);
            if (obj.GetType() == "".GetType())
                return (string) this == (string) obj;
            return false;
        }

        public override unsafe string ToString()
        {
            fixed (byte* n = payload_)
                return SmallStringMethods.ToString(n, MaxLength);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallString4 : IComparable<SmallString4>, IEquatable<SmallString4>
    {
        private const int MaxLength = 4;
        private unsafe fixed byte payload_[MaxLength];

        public unsafe SmallString4(string value)
        {
            fixed (byte* n = payload_)
                SmallStringMethods.FromString(n, MaxLength, value);
        }

        public static implicit operator SmallString4(string s)
        {
            return new SmallString4(s);
        }

        public static implicit operator string(in SmallString4 s)
        {
            return s.ToString();
        }

        public static unsafe implicit operator bool(in SmallString4 value)
        {
            fixed (byte* n = value.payload_)
                return !SmallStringMethods.IsNull(n, MaxLength);
        }

        public static unsafe bool TryConvert(string value, out SmallString4 s)
        {
            fixed (byte* n = s.payload_)
                return SmallStringMethods.TryFromString(n, MaxLength, value);
        }

        /* Implements the same order as in D */
        public unsafe int CompareTo(SmallString4 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Compare(n, m, MaxLength);
        }

        public unsafe bool Equals(SmallString4 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Equals(n, m, MaxLength);
        }

        public static bool operator ==(in SmallString4 lhs, in SmallString4 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in SmallString4 lhs, in SmallString4 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public unsafe bool EqualsIgnoreCase(SmallString4 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.EqualsIgnoreCase(n, m, MaxLength);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() == GetType())
                return Equals((SmallString4) obj);
            if (obj.GetType() == "".GetType())
                return (string) this == (string) obj;
            return false;
        }

        public override unsafe string ToString()
        {
            fixed (byte* n = payload_)
                return SmallStringMethods.ToString(n, MaxLength);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallString31 : IComparable<SmallString31>, IEquatable<SmallString31>
    {
        private const int MaxLength = 31;
        private unsafe fixed byte payload_[MaxLength];

        public unsafe SmallString31(string value)
        {
            fixed (byte* n = payload_)
                SmallStringMethods.FromString(n, MaxLength, value);
        }

        public static implicit operator SmallString31(string s)
        {
            return new SmallString31(s);
        }

        public static implicit operator string(in SmallString31 s)
        {
            return s.ToString();
        }

        public static unsafe implicit operator bool(in SmallString31 value)
        {
            fixed (byte* n = value.payload_)
                return !SmallStringMethods.IsNull(n, MaxLength);
        }

        public static unsafe bool TryConvert(string value, out SmallString31 s)
        {
            fixed (byte* n = s.payload_)
                return SmallStringMethods.TryFromString(n, MaxLength, value);
        }

        /* Implements the same order as in D */
        public unsafe int CompareTo(SmallString31 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Compare(n, m, MaxLength);
        }

        public unsafe bool Equals(SmallString31 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Equals(n, m, MaxLength);
        }

        public static bool operator ==(in SmallString31 lhs, in SmallString31 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in SmallString31 lhs, in SmallString31 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public unsafe bool EqualsIgnoreCase(SmallString31 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.EqualsIgnoreCase(n, m, MaxLength);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() == GetType())
                return Equals((SmallString31) obj);
            if (obj.GetType() == "".GetType())
                return (string) this == (string) obj;
            return false;
        }

        public override unsafe string ToString()
        {
            fixed (byte* n = payload_)
                return SmallStringMethods.ToString(n, MaxLength);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallString32 : IComparable<SmallString32>, IEquatable<SmallString32>
    {
        private const int MaxLength = 32;
        private unsafe fixed byte payload_[MaxLength];

        public unsafe SmallString32(string value)
        {
            fixed (byte* n = payload_)
                SmallStringMethods.FromString(n, MaxLength, value);
        }

        public static implicit operator SmallString32(string s)
        {
            return new SmallString32(s);
        }

        public static implicit operator string(in SmallString32 s)
        {
            return s.ToString();
        }

        public static unsafe implicit operator bool(in SmallString32 value)
        {
            fixed (byte* n = value.payload_)
                return !SmallStringMethods.IsNull(n, MaxLength);
        }

        public static unsafe bool TryConvert(string value, out SmallString32 s)
        {
            fixed (byte* n = s.payload_)
                return SmallStringMethods.TryFromString(n, MaxLength, value);
        }

        /* Implements the same order as in D */
        public unsafe int CompareTo(SmallString32 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Compare(n, m, MaxLength);
        }

        public unsafe bool Equals(SmallString32 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Equals(n, m, MaxLength);
        }

        public static bool operator ==(in SmallString32 lhs, in SmallString32 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in SmallString32 lhs, in SmallString32 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public unsafe bool EqualsIgnoreCase(SmallString32 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.EqualsIgnoreCase(n, m, MaxLength);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() == GetType())
                return Equals((SmallString32) obj);
            if (obj.GetType() == "".GetType())
                return (string) this == (string) obj;
            return false;
        }

        public override unsafe string ToString()
        {
            fixed (byte* n = payload_)
                return SmallStringMethods.ToString(n, MaxLength);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallString64 : IComparable<SmallString64>, IEquatable<SmallString64>
    {
        private const int MaxLength = 64;
        private unsafe fixed byte payload_[MaxLength];

        public unsafe SmallString64(string value)
        {
            fixed (byte* n = payload_)
                SmallStringMethods.FromString(n, MaxLength, value);
        }

        public static implicit operator SmallString64(string s)
        {
            return new SmallString64(s);
        }

        public static implicit operator string(in SmallString64 s)
        {
            return s.ToString();
        }

        public static unsafe implicit operator bool(in SmallString64 value)
        {
            fixed (byte* n = value.payload_)
                return !SmallStringMethods.IsNull(n, MaxLength);
        }

        public static unsafe bool TryConvert(string value, out SmallString64 s)
        {
            fixed (byte* n = s.payload_)
                return SmallStringMethods.TryFromString(n, MaxLength, value);
        }

        /* Implements the same order as in D */
        public unsafe int CompareTo(SmallString64 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Compare(n, m, MaxLength);
        }

        public unsafe bool EqualsIgnoreCase(SmallString64 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.EqualsIgnoreCase(n, m, MaxLength);
        }

        public unsafe bool Equals(SmallString64 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Equals(n, m, MaxLength);
        }

        public static bool operator ==(in SmallString64 lhs, in SmallString64 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in SmallString64 lhs, in SmallString64 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() == GetType())
                return Equals((SmallString64) obj);
            if (obj.GetType() == "".GetType())
                return (string) this == (string) obj;
            return false;
        }

        public override unsafe string ToString()
        {
            fixed (byte* n = payload_)
                return SmallStringMethods.ToString(n, MaxLength);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallString128 : IComparable<SmallString128>, IEquatable<SmallString128>
    {
        private const int MaxLength = 128;
        private unsafe fixed byte payload_[MaxLength];

        public unsafe SmallString128(string value)
        {
            fixed (byte* n = payload_)
                SmallStringMethods.FromString(n, MaxLength, value);
        }

        public static implicit operator SmallString128(string s)
        {
            return new SmallString128(s);
        }

        public static implicit operator string(in SmallString128 s)
        {
            return s.ToString();
        }

        public static unsafe implicit operator bool(in SmallString128 value)
        {
            fixed (byte* n = value.payload_)
                return !SmallStringMethods.IsNull(n, MaxLength);
        }

        public static unsafe bool TryConvert(string value, out SmallString128 s)
        {
            fixed (byte* n = s.payload_)
                return SmallStringMethods.TryFromString(n, MaxLength, value);
        }

        /* Implements the same order as in D */
        public unsafe int CompareTo(SmallString128 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Compare(n, m, MaxLength);
        }

        public unsafe bool EqualsIgnoreCase(SmallString128 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.EqualsIgnoreCase(n, m, MaxLength);
        }

        public unsafe bool Equals(SmallString128 rhs)
        {
            byte* m = rhs.payload_;
            fixed (byte* n = payload_)
                return SmallStringMethods.Equals(n, m, MaxLength);
        }

        public static bool operator ==(in SmallString128 lhs, in SmallString128 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(in SmallString128 lhs, in SmallString128 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() == GetType())
                return Equals((SmallString128) obj);
            if (obj.GetType() == "".GetType())
                return (string) this == (string) obj;
            return false;
        }

        public override unsafe string ToString()
        {
            fixed (byte* n = payload_)
                return SmallStringMethods.ToString(n, MaxLength);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    public static class SmallStringMethods
    {
        public static unsafe void FromString(byte* n, int MaxLength, string value)
        {
            if (value.Length > MaxLength)
                throw new ArgumentException($"SmallString{MaxLength} '{value}': Too long (max {MaxLength} characters)");
            fixed (char* c = value) //Throws if can't fit value into n
            {
                var count = System.Text.Encoding.UTF8.GetBytes(c, value.Length, n, MaxLength);
                for (var idx = count; idx < MaxLength; idx++)
                {
                    n[idx] = 0;
                }
            }
        }

        public static unsafe bool TryFromString(byte* n, int MaxLength, string value)
        {
            if (value.Length > MaxLength)
                return false;
            fixed (char* c = value) //Throws if can't fit value into n
            {
                var count = System.Text.Encoding.UTF8.GetBytes(c, value.Length, n, MaxLength);
                for (var idx = count; idx < MaxLength; idx++)
                {
                    n[idx] = 0;
                }
            }
            return true;
        }

        public static unsafe bool IsNull(byte* n, int MaxLength)
        {
            return n[0] == 0;
        }

        public static unsafe int Compare(byte* n, byte* m, int MaxLength)
        {
            for (var i = 0; i < MaxLength; i++)
            {
                if (n[i] < m[i])
                    return -1;
                if (n[i] > m[i])
                    return 1;
            }
            return 0;
        }

        public static unsafe bool Equals(byte* r, byte* l, int MaxLength)
        {
            var len = MaxLength / 8;
            var n = (ulong*)r;
            var m = (ulong*)l;
            for (var i = 0; i < len; i++)
            {
                if (n[i] != m[i])
                    return false;
            }

            // Check for SmallString2, 3, 31, etc.
            if (MaxLength % 8 != 0)
            {
                for (var i = (len * 8); i < MaxLength; i++)
                {
                    if (n[i] != m[i])
                        return false;
                }
            }

            return true;
        }

        public static unsafe bool EqualsIgnoreCase(byte* r, byte* l, int MaxLength)
        {
            for (var i = 0; i < MaxLength; i++)
            {
                if (r[i] != l[i] && ASCIIToUpper(r[i]) != ASCIIToUpper(l[i]))
                    return false;
                if (r[i] == 0)
                    return true;
            }
            return true;
        }

        private static byte ASCIIToUpper(byte a)
        {
            if (a >= 'a' && a <= 'z')
                return (byte)(a + ('A' - 'a'));
            return a;
        }

        public static unsafe int StrLen(byte* bytes, int MaxLength)
        {
            var i = 0;
            while(i < MaxLength && bytes[i] != 0)
                ++i;
            return i;
        }

        public static unsafe string ToString(byte* n, int MaxLength)
        {
            return System.Text.Encoding.UTF8.GetString(n, StrLen(n, MaxLength));
        }
    }
}
