using Ez.Graphics.API.Vulkan.Core.Textures;
using Ez.Numerics;
using System;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal struct BindingInfo : IEquatable<BindingInfo>
    {
        public BufferUsage BufferUsage;
        public BufferSpan Buffer;
        public BaseTexture Texture;
        public Sampler Sampler;
        public uint Binding;

        public bool IsBufferBinding =>
            Buffer.Buffer != null &&
            BufferUsage != BufferUsage.None &&
            Texture == null && Sampler == null;

        public bool IsTextureOrSamplerBinding =>
            Buffer.Buffer == null &&
            BufferUsage == BufferUsage.None &&
            (Texture != null || Sampler != null);

        public bool IsBinding => IsBufferBinding || IsTextureOrSamplerBinding;

        public bool Equals(BindingInfo other) =>
            BufferUsage == other.BufferUsage &&
            Buffer == other.Buffer &&
            Texture == other.Texture &&
            Sampler == other.Sampler &&
            Binding == other.Binding;

        public override bool Equals(object obj) =>
            obj is BindingInfo bi && Equals(bi);

        public override int GetHashCode() =>
            HashHelper<BindingInfo>.Combine(BufferUsage, Buffer, Texture, Sampler, Binding);

        public override string ToString()
        {
            if (IsBufferBinding)
                return $"(BindingInfo, Binding: {Binding}, BufferUsage: {BufferUsage}, Buffer: {Buffer})";
            else if (IsTextureOrSamplerBinding)
                return $"(BindingInfo, Binding: {Binding}, Texture: {Texture}, Sampler: {Sampler})";
            else
                return $"(BindingInfo undefined)";
        }

        public static bool operator ==(BindingInfo value1, BindingInfo value2) =>
            value1.Equals(value2);

        public static bool operator !=(BindingInfo value1, BindingInfo value2) =>
            !(value1 == value2);
    }
}
