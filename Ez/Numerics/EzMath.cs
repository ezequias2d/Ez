using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Numerics
{    
    public static class EzMath
    {
        public static readonly float PI = (float)Math.PI;
        public static readonly float Deg2Rad = (float)(Math.PI / 180d);
        public static readonly float Rad2Deg = (float)(180d / Math.PI);
        public static readonly float Epsilon = float.Epsilon;
        public static readonly double InvLogE2 = 1.0 / Math.Log(2);

        public static double Log2(double d) => Math.Log(d) * InvLogE2;
        public static float ToRadians(float degrees) => degrees * Deg2Rad;

        public static Vector3 ToRadians(this Vector3 degrees) => degrees * Deg2Rad;

        public static float ToDegrees(float radians) => radians * Rad2Deg;

        public static Vector3 ToDegrees(this Vector3 radians) => radians * Rad2Deg;

        public static bool Approximately(float a, float b)
        {
            if (a == 0 || b == 0)
                return Math.Abs(a - b) <= Epsilon;
            return Math.Abs(a - b) / Math.Abs(a) <= Epsilon && 
                Math.Abs(a - b) / Math.Abs(b) <= Epsilon;
        }

        public static float fFloor(float value) => value - (value % 1);
        public static float fCeiling(float value) 
        {
            float m = value % 1;
            return value - m + (m / (m + Epsilon));
        }

        public static int iFloor(float value)
        {
            int i = (int)value;
            if (i > value)
                i--;
            return i;
        }

        /// <summary>
        /// Fast ceiling implementation, the cost of this are some errors in the ceiling that 
        /// will represent 4.33% of all floating point space, when compared to <see cref="Math.Ceiling(double)"/>.
        /// 
        /// Returns the smallest integral value that is greater than or equal to the specified
        /// double-precision floating-point number.
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>
        ///     The smallest integral value that is greater than or equal to value. If value is equal
        ///     to System.Double.NaN, System.Double.NegativeInfinity, or System.Double.PositiveInfinity,
        ///     the result will be unpredictable.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceiling(float value)
        {
            //FloatToInt(0.999999940395f)
            //return (int)(value + CreatePositiveFloat((126, (0x7F_FFFF << (ExtractExpoentIEEE754(value) + 1)) & 0x7F_FFFF)));
            return (int)(value + 8388608) - 8388608;
        }

        public static int iCeiling(float value)
        {
            int i = (int)value;
            if (i < value)
                i++;
            return i;
        }

        public static int ExtractExpoentIEEE754(float value)
        {
            unsafe
            {
                return (((*(int*)&value) & int.MaxValue) >> 23) - 127;
            }
        }

        public static float CreatePositiveFloat((byte exponent, int mantissa) value)
        {
            value.mantissa = (value.exponent << 23) | (value.mantissa);
            unsafe
            {
                return *(float*)&value.mantissa;
            }
        }

        public static float IntToFloat(int value)
        {
            unsafe
            {
                return *(float*)&value;
            }
        }

        public static int FloatToInt(float value)
        {
            unsafe
            {
                return *(int*)&value;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Round(float value)
        {
            //return (int)(value + 0.4999999701976776123046875f);
            
            return (int)(value + IntToFloat(FloatToInt(0.49479162693f) | (FloatToInt(value) & int.MinValue)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int TestRound(float value, float constant)
        {
            return (int)(value + IntToFloat(FloatToInt(constant) | (FloatToInt(value) & int.MinValue)));
        }

        public static float Ceiling2(float value)
        {
            float m = value % 1;
            return value - m + (m == 0 ? 0f : 1f);
        }

        public static float Ceiling3(float value)
        {
            return (float)Math.Ceiling(value);
        }

        public static Vector3 ToEulerAngles(this Quaternion q)
        {
            Vector3 euler;

            // if the input quaternion is normalized, this is exactly one. Otherwise, this acts as a correction factor for the quaternion's not-normalizedness
            float unit = (q.X * q.X) + (q.Y * q.Y) + (q.Z * q.Z) + (q.W * q.W);

            // this will have a magnitude of 0.5 or greater if and only if this is a singularity case
            float test = q.X * q.W - q.Y * q.Z;

            if (test > 0.4995f * unit) // singularity at north pole
            {
                euler.X = (float)(Math.PI) / 2;
                euler.Y = 2f * (float)Math.Atan2(q.Y, q.X);
                euler.Z = 0;
            }
            else if (test < -0.4995f * unit) // singularity at south pole
            {
                euler.X = -(float)(Math.PI) / 2;
                euler.Y = -2f * (float)Math.Atan2(q.Y, q.X);
                euler.Z = 0;
            }
            else // no singularity - this is the majority of cases
            {
                euler.X = (float)Math.Asin(2f * (q.W * q.X - q.Y * q.Z));
                euler.Y = (float)Math.Atan2(2f * q.W * q.Y + 2f * q.Z * q.X, 1 - 2f * (q.X * q.X + q.Y * q.Y));
                euler.Z = (float)Math.Atan2(2f * q.W * q.Z + 2f * q.X * q.Y, 1 - 2f * (q.Z * q.Z + q.X * q.X));
            }

            return euler * Rad2Deg;
        }

        public static Quaternion ToQuaternion(this Vector3 eulerAngles)
        {
            Vector3 eulerAnglesRadians = eulerAngles * Deg2Rad;
            return Quaternion.CreateFromYawPitchRoll(eulerAnglesRadians.Y, eulerAnglesRadians.X, eulerAnglesRadians.Z);
        }
    }
}
