// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a index in a <see cref="Scene"/>.
    /// </summary>
    /// <typeparam name="T">The type of value in index.</typeparam>
    public readonly struct SceneIndex<T> : IEquatable<SceneIndex<T>>, IEquatable<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SceneIndex{T}"/> struct.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="scene"></param>
        public SceneIndex(int index, Scene scene)
        {
            Index = index;
            Scene = scene;
        }

        /// <summary>
        /// Gets the value of index in the <see cref="Scene"/>;
        /// </summary>
        public T Value => Scene.Get(this);

        /// <summary>
        /// Gets the <see cref="Scene"/> of <see cref="Index"/>.
        /// </summary>
        public Scene Scene { get; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() =>
            HashHelper<SceneIndex<T>>.Combine(Index, Scene);

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="SceneIndex{T}"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="SceneIndex{T}"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="SceneIndex{T}"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(SceneIndex<T> other) =>
            (Index == other.Index && Scene == other.Scene) || 
            (Value?.Equals(other.Value) ?? false);

        /// <summary>
        /// Returns a value indicating whether the value of this instance and another value are the same.
        /// </summary>
        /// <param name="other">The other T value.</param>
        /// <returns><see langword="true"/> if the <see cref="SceneIndex{T}.Value"/> is equal to <paramref name="other"/> value; otherwise, <see langword="false"/>.</returns>
        public bool Equals(T other) =>
            Value.Equals(other);

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case SceneIndex<T> sceneIndex:
                    return Equals(sceneIndex);
                case T t:
                    return Equals(t);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the index of a <see cref="SceneIndex{T}"/>.
        /// </summary>
        /// <param name="index">The <see cref="SceneIndex{T}"/> to get the index.</param>
        public static explicit operator int(SceneIndex<T> index) => index.Index;

        /// <summary>
        /// Gets the value of a <see cref="SceneIndex{T}"/>.
        /// </summary>
        /// <param name="index">The <see cref="SceneIndex{T}"/> to get the value.</param>
        public static implicit operator T(SceneIndex<T> index) => index.Value;
    }
}
