using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Graphics.API.Vulkan.Core.Textures
{
    internal class ImageLayoutManager
    {
        private IDictionary<Texture, Node> _nodes;
        public ImageLayoutManager()
        {
            _nodes = new Dictionary<Texture, Node>();
        }

        public void TransitionImageLayout(
            CommandBuffer cb,
            Texture texture,
            ReadOnlySpan<ImageSubresourceRange> ranges,
            ImageLayout newLayout)
        {
            if(!_nodes.TryGetValue(texture, out var node))
            {
                node = new Node(texture);
                _nodes[texture] = node;
            }

            Debug.Assert(ReferenceEquals(texture, node.Texture));
            node.TransitionImageLayout(cb, ranges, newLayout);
        }

        public unsafe void Reset(CommandBuffer cb)
        {
            foreach(var node in _nodes.Values)
            {
                var range = node.Texture.GetFullRange().ToVk(node.Texture);
                var ranges = new ReadOnlySpan<ImageSubresourceRange>(&range, 1);
                node.TransitionImageLayoutNonmatching(cb, ranges, node.Texture.DefaultImageLayout);
            }
        }
        private static IEnumerable<(uint Level, uint Layer)> Range(ImageSubresourceRange range)
        {
            foreach (var level in Enumerable.Range((int)range.BaseMipLevel, (int)range.LevelCount))
                foreach (var layer in Enumerable.Range((int)range.BaseArrayLayer, (int)range.LayerCount))
                    yield return ((uint)level, (uint)layer);
        }

        private readonly struct Node
        {
            private readonly ImageLayout[] _imageLayouts;

            public Node(Texture texture)
            {
                if (texture is null)
                    throw new ArgumentNullException(nameof(texture));

                Texture = texture;
                _imageLayouts = new ImageLayout[texture.MipmapLevels * texture.ArrayLayers * texture.Size.Depth];
                Array.Fill(_imageLayouts, texture.DefaultImageLayout);
            }

            public Texture Texture { get; }

            public ref ImageLayout this[uint mipLevel, uint arrayLayer] =>
                ref _imageLayouts[arrayLayer * Texture.MipmapLevels + mipLevel];
            internal void TransitionImageLayout(
                CommandBuffer cb,
                ReadOnlySpan<ImageSubresourceRange> ranges,
                ImageLayout newLayout)
            {
                var oldLayout = this[ranges[0].BaseMipLevel, ranges[0].BaseArrayLayer];

                foreach (var range in ranges)
                    foreach (var (level, layer) in Range(range))
                        if (this[level, layer] != oldLayout)
                        {
                            TransitionImageLayoutNonmatching(cb, ranges, newLayout);
                            return;
                        }

                if (oldLayout == newLayout)
                    return;

                cb.TransiantImageLayout(Texture, ranges, oldLayout, newLayout);

                foreach (var range in ranges)
                    foreach (var (level, layer) in Range(range))
                        this[level, layer] = newLayout;
            }
            internal void TransitionImageLayoutNonmatching(
            CommandBuffer cb,
            ReadOnlySpan<ImageSubresourceRange> ranges,
            ImageLayout newLayout)
            {
                var tempRange = new ImageSubresourceRange()
                {
                    LevelCount = 1,
                    LayerCount = 1,
                };

                foreach (var range in ranges)
                {
                    foreach (var (level, layer) in Range(range))
                    {
                        var oldLayout = this[level, layer];

                        if (oldLayout == newLayout)
                            continue;

                        tempRange.BaseMipLevel = level;
                        tempRange.BaseArrayLayer = layer;

                        unsafe
                        {
                            cb.TransiantImageLayout(Texture, new Span<ImageSubresourceRange>(&range, 1), oldLayout, newLayout);
                        }

                        this[level, layer] = newLayout;
                    }
                }
            }
        }
    }
}
