// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ez.Graphics.Data.Materials
{
    /// <summary>
    /// A class to store the information of a light.
    /// </summary>
    public sealed class Material : IEquatable<Material>
    {
        private readonly MaterialProperty[] _array;
        private readonly int _count;
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="Material"/> class.
        /// </summary>
        /// <param name="name">The name of the material.</param>
        /// <param name="properties">Material properties.</param>
        public Material(string name, IEnumerable<MaterialProperty> properties)
        {
            Name = name;

            _array = default;
            _count = 0;
            InternalHelper.SetManaged(ref _array, ref _count, properties.ToArray());

            _hashcode = 0;
            _hashcode = HashHelper<Material>.Combine(Name, HashHelper<MaterialProperty>.Combine(Properties));
        }

        /// <summary>
        /// Gets the name of a <see cref="Material"/> instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the properties of a <see cref="Material"/> instance.
        /// </summary>
        public ReadOnlySpan<MaterialProperty> Properties =>
            new ReadOnlySpan<MaterialProperty>(_array, 0, _count);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> with <see cref="MaterialProperty"/> of a <see cref="Material"/> instance.
        /// </summary>
        public IEnumerable<MaterialProperty> Enumerable => 
            MemoryMarshal.ToEnumerable<MaterialProperty>(new Memory<MaterialProperty>(_array, 0, _count));

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Material"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Material"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="Material"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Material other) =>
            _hashcode == other._hashcode &&
            Name == other.Name &&
            Enumerable.Join(
                    other.Enumerable,
                    (property) => property,
                    (property) => property,
                    (property1, property2) => default(object)
                ).Any();

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Material material && Equals(material);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;
    }
}
