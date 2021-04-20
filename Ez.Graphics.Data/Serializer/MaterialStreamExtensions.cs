// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Materials;
using Ez.Graphics.Data.Serializer.Raws;
using Ez.IO;
using System.IO;


namespace Ez.Graphics.Data.Serializer
{
    /// <summary>
    /// A static class that provides serializer and deserializer for <see cref="Material"/>, <see cref="MaterialProperty"/> or <see cref="TextureData"/>.
    /// </summary>
    public static class MaterialStreamExtensions
    {
        /// <summary>
        /// Derializes an <see cref="Material"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="Material"/> with data read from the <paramref name="stream"/>.</returns>
        public static Material ReadMaterial(this Stream stream)
        {
            var name = stream.ReadString();
            MaterialRaw raw = stream.ReadStructure<MaterialRaw>();

            var properties = new MaterialProperty[raw.PropertiesCount];
            for(uint i = 0; i < raw.PropertiesCount; i++)
                properties[i] = stream.ReadMaterialProperty();

            return new Material(name, properties);
        }

        /// <summary>
        /// Derializes an <see cref="MaterialProperty"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="MaterialProperty"/> with data read from the <paramref name="stream"/>.</returns>
        public static MaterialProperty ReadMaterialProperty(this Stream stream)
        {
            string name = stream.ReadString();

            MaterialPropertyRaw raw = stream.ReadStructure<MaterialPropertyRaw>();

            switch (raw.ValueType)
            {
                case MaterialPropertyType.Color:
                    return new MaterialProperty<ColorSingle>(name, stream.ReadStructure<ColorSingle>());
                case MaterialPropertyType.Single:
                    return new MaterialProperty<float>(name, stream.ReadStructure<float>());
                case MaterialPropertyType.Texture:
                    return new MaterialProperty<TextureData>(name, stream.ReadTexture());
                case MaterialPropertyType.Undefined:
                    return new MaterialProperty<byte>(name, 0);
                default:
                    throw new EzException("Could not read MaterialProperty correctly.");
            }
        }

        /// <summary>
        /// Derializes an <see cref="TextureData"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="TextureData"/> with data read from the <paramref name="stream"/>.</returns>
        public static TextureData ReadTexture(this Stream stream)
        {
            int mode = stream.ReadByte();
            switch (mode)
            {
                //texture
                case 0:
                    TextureRaw raw = stream.ReadStructure<TextureRaw>();
                    var data = stream.ReadSpan<byte>(raw.Size);
                    return new Texture(raw.PixelFormat, raw.Width, raw.Height, raw.Depth, raw.MipmapLevels, raw.Layers, raw.TextureType, data);
                //texture reference
                case 1:
                    return new TextureReference(stream.ReadString());
                default:
                    throw new InvalidDataException();
            }
        }

        /// <summary>
        /// Serialize <see cref="Material"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="material">The material to write.</param>
        public static void WriteMaterial(this Stream stream, in Material material)
        {
            stream.WriteString(material.Name);
            MaterialRaw raw = default;
            raw.PropertiesCount = (uint)material.Properties.Length;

            foreach (var property in material.Properties)
                stream.WriteMaterialProperty(property);
        }

        /// <summary>
        /// Serialize <see cref="MaterialProperty"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="property">The property to write.</param>
        public static void WriteMaterialProperty(this Stream stream, in MaterialProperty property)
        {
            stream.WriteString(property.Name);

            MaterialPropertyRaw raw = default;
            raw.ValueType = property.ValueType;
            stream.WriteStructure(raw);

            switch (property.ValueType)
            {
                case MaterialPropertyType.Color:
                    stream.WriteMaterialProperty<ColorSingle>(property);
                    break;
                case MaterialPropertyType.Single:
                    stream.WriteMaterialProperty<float>(property);
                    break;
                case MaterialPropertyType.Texture:
                    stream.WriteTexture(((MaterialProperty<TextureData>)property).Value);
                    break;
                case MaterialPropertyType.Undefined:
                    break;
                default:
                    throw new EzException("Could not write MaterialProperty correctly.");
            }
        }

        /// <summary>
        /// Serialize <see cref="TextureData"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="texture">The texture to write.</param>
        public static void WriteTexture(this Stream stream, in TextureData texture)
        {
            switch (texture)
            {
                case Texture t:
                    stream.WriteByte(0);
                    stream.WriteStructure(new TextureRaw
                    {
                        PixelFormat = t.PixelFormat,
                        Width = t.Width,
                        Height = t.Height,
                        Depth = t.Depth,
                        Layers = t.ArrayLayers,
                        MipmapLevels = t.MipmapLevels,
                        TextureType = t.TextureType,
                        Size = (uint)t.Data.Length
                    });
                    stream.WriteSpan(t.Data);
                    break;
                case TextureReference tr:
                    stream.WriteByte(1);
                    stream.WriteString(tr.Reference);
                    break;
            }
        }

        private static void WriteMaterialProperty<T>(this Stream stream, MaterialProperty property) where T : unmanaged =>
            stream.WriteStructure(((MaterialProperty<T>)property).Value);
    }
}
