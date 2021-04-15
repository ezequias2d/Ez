// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ez.Numerics
{    
    /// <summary>
    /// Provides constants and static methods to complement <see cref="Math"/>.
    /// </summary>
    public static class EzMath
    {
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        public static readonly float PI = (float)Math.PI;

        /// <summary>
        /// Degrees-to-radians conversion constant.
        /// <see cref="Rad2Deg"/>
        /// </summary>
        public static readonly float Deg2Rad = (float)(Math.PI / 180d);

        /// <summary>
        /// Radians-to-degrees conversion constant.
        /// <see cref="Deg2Rad"/>
        /// </summary>
        public static readonly float Rad2Deg = (float)(180d / Math.PI);

        /// <summary>
        /// Natural logarithm of 2 inverted constant(1 / log 2).
        /// </summary>
        public static readonly double InvLogE2 = 1.0 / Math.Log(2);

        /// <summary>
        /// Calculates the log on base 2.
        /// </summary>
        /// <param name="d">The number whose logarithm is to be found.</param>
        /// <returns>
        /// The natural logarithm of <paramref name="d"/>, if <paramref name="d"/> is positive.<br/>
        /// <see cref="double.NegativeInfinity"/>, if <paramref name="d"/> is zero.<br/>
        /// <see cref="double.NaN"/>, if <paramref name="d"/> is negative or equal to <see cref="double.NaN"/>.<br/>
        /// <see cref="double.PositiveInfinity"/>, if <paramref name="d"/> is <see cref="double.PositiveInfinity"/>.</returns>
        public static double Log2(double d) => Math.Log(d) * InvLogE2;

        /// <summary>
        /// Compares two floating point values and returns true if they are similar.
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">The second value to compare.</param>
        /// <returns><see langword="true"/>, if they are within a small value epsilon; otherwise, <see langword="false"/>.</returns>
        public static bool Approximately(float a, float b)
        {
            if (a == 0 || b == 0)
                return Math.Abs(a - b) <= float.Epsilon;
            return Math.Abs(a - b) / Math.Abs(a) <= float.Epsilon && 
                Math.Abs(a - b) / Math.Abs(b) <= float.Epsilon;
        }

        /// <summary>
        /// Returns the largest integral value less than or equal to the specified number.
        /// </summary>
        /// <param name="d">A single-precision floating-point number.</param>
        /// <returns>The largest integral value less than or equal to <paramref name="d"/>. 
        /// If <paramref name="d"/> is equal to <see cref="float.NaN"/>, <see cref="float.NegativeInfinity"/>, 
        /// or <see cref="float.PositiveInfinity"/>, that value is returned.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float d) => (float)Math.Floor(d);

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the specified single-precision floating-point number.
        /// </summary>
        /// <param name="a">A single-precision floating-point number.</param>
        /// <returns>The smallest integral value that is greater than or equal to <paramref name="a"/>. 
        /// If <paramref name="a"/> is equal to <see cref="float.NaN"/>, <see cref="float.NegativeInfinity"/>, 
        /// or <see cref="float.PositiveInfinity"/>, that value is returned. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceiling(float a) => (float)Math.Ceiling(a);

        /// <summary>
        /// Rounds a value to the nearest integer or to the specified number of fractional digits.
        /// </summary>
        /// <param name="a">A single-precision floating-point number to be rounded.</param>
        /// <returns>The integer nearest <paramref name="a"/>. If the fractional component of a is halfway between two integers,
        /// one of which is even and the other odd, then the even number is returned.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float a) => (float)Math.Round(a);

        /// <summary>
        /// Returns the euler angle representation of a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="q">The quaternion to represent in the form of euler angle.</param>
        /// <returns>An euler angle vector that represents the <paramref name="q"/> quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        /// <summary>
        /// Returns the quaternion representation of a euler angle <see cref="Vector3"/>.
        /// </summary>
        /// <param name="eulerAngles">The euler angle to represents in the form of quaternion.</param>
        /// <returns>An quaternion that represents the <paramref name="eulerAngles"/> vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToQuaternion(this Vector3 eulerAngles)
        {
            Vector3 eulerAnglesRadians = eulerAngles * Deg2Rad;
            return Quaternion.CreateFromYawPitchRoll(eulerAnglesRadians.Y, eulerAnglesRadians.X, eulerAnglesRadians.Z);
        }
    }
}
