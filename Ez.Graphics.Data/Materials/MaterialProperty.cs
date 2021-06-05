// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ez.Graphics.Data.Materials
{
    /// <summary>
    /// Represents a property of a <see cref="Material"/>.
    /// </summary>
    public abstract class MaterialProperty : IEquatable<MaterialProperty>, IClone<MaterialProperty>
    {
        internal MaterialProperty(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of property.
        /// </summary>
        public abstract MaterialPropertyType ValueType { get; }
        /// <summary>
        /// If <see cref="ValueType"/> is <see cref="MaterialPropertyType.Texture"/>, 
        /// this contains the type of texture; otherwise, <see cref="TextureType.None"/>.
        /// </summary>
        public abstract TextureType TextureType { get; set; }
        /// <summary>
        /// Gets a clone of this <see cref="MaterialProperty"/> instance.
        /// </summary>
        public abstract MaterialProperty Clone { get; }

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is MaterialProperty property)
                return Equals(property);
            return false;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="MaterialProperty"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="MaterialProperty"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="MaterialProperty"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(MaterialProperty other) => GetHashCode() == other.GetHashCode() && string.Equals(Name, other.Name, StringComparison.Ordinal);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override abstract int GetHashCode();
    }

    /// <summary>
    /// Represents a property of a <see cref="Material"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MaterialProperty<T> : MaterialProperty, IEquatable<MaterialProperty<T>>
    {
        private static readonly MaterialPropertyType PropertyType;

        static MaterialProperty()
        {
            Type t = typeof(T);
            if (t == typeof(TextureData))
                PropertyType = MaterialPropertyType.Texture;
            else if (t == typeof(ColorSingle))
                PropertyType = MaterialPropertyType.Color;
            else if (t == typeof(float))
                PropertyType = MaterialPropertyType.Single;
            else if (t == typeof(double))
                PropertyType = MaterialPropertyType.Double;
            else if (t == typeof(int))
                PropertyType = MaterialPropertyType.Integer;
            else
                PropertyType = MaterialPropertyType.Undefined;
        }

        private TextureType _textureType;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialProperty{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        public MaterialProperty(string name, T value) : base(name)
        {
            Value = value;
            Name = name;
            _textureType = TextureType.None;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialProperty{T}"/> class.
        /// </summary>
        /// <param name="materialProperty"></param>
        public MaterialProperty(MaterialProperty<T> materialProperty)
            : this(materialProperty.Name, materialProperty.Value)
        { }

        /// <summary>
        /// The value of property.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// The type of property.
        /// </summary>
        public override MaterialPropertyType ValueType => PropertyType;

        /// <summary>
        /// Gets a clone of this <see cref="MaterialProperty"/> instance.
        /// </summary>
        public override MaterialProperty Clone => new MaterialProperty<T>(this);

        /// <summary>
        /// If <see cref="ValueType"/> is <see cref="MaterialPropertyType.Texture"/>, 
        /// this contains the type of texture; otherwise, <see cref="TextureType.None"/>.
        /// </summary>
        public override TextureType TextureType 
        {
            get => _textureType;
            set
            {
                if (PropertyType == MaterialPropertyType.Texture)
                    _textureType = value;
                else
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() =>
            HashHelper<MaterialProperty>.Combine(Name, ValueType, Value);

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="MaterialProperty{T}"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="MaterialProperty{T}"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="MaterialProperty{T}"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(MaterialProperty<T> other) => 
            Equals((MaterialProperty)other) && Value.Equals(other.Value);
    }
}
