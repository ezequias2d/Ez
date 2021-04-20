// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Numerics;

namespace Ez.Graphics.Data.Animations
{
    /// <summary>
    /// A time-value pair specifying a certain 3D vector for a given time.
    /// </summary>
    public struct Vector3Key : IAnimationKey, IEquatable<Vector3Key>
    {
        /// <summary>
        /// The time of this key.
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// The value of this key.
        /// </summary>
        public Vector3 Value { get; set; }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Vector3Key"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Vector3Key"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="Vector3Key"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Vector3Key other) =>
            Time == other.Time &&
            Value == other.Value;

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Vector3Key key && Equals(key);

        /// <summary>
        /// Returns the string representation of the current <see cref="Vector3Key"/> instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString() => $"(Time: {Time}, Value: {Value})";

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => HashHelper<Vector3Key>.Combine(Time, Value);
    }
}
