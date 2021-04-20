// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.IO;
using Ez.Graphics.Data.Animations;
using System.IO;
using Ez.Graphics.Data.Serializer.Raws;

namespace Ez.Graphics.Data.Serializer
{
    /// <summary>
    /// A static class that provides serializer and deserializer for <see cref="Animation"/> and <see cref="AnimationNode"/>.
    /// </summary>
    public static class AnimationStreamExtensions
    {
        /// <summary>
        /// Serialize <see cref="Animation"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="animation">The animation to write.</param>
        public static void WriteAnimation(this Stream stream, in Animation animation)
        {
            stream.WriteString(animation.Name);

            AnimationRaw raw = default;
            raw.Duration = animation.Duration;
            raw.TicksPerSecond = animation.TicksPerSecond;
            raw.ChannelsCount = (uint)animation.Channels.Length;

            stream.WriteStructure(raw);

            foreach (AnimationNode node in animation.Channels)
            {
                stream.WriteAnimationNode(node);
            }
        }

        /// <summary>
        /// Serialize <see cref="AnimationNode"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="node">The animation node to write.</param>
        public static void WriteAnimationNode(this Stream stream, in AnimationNode node)
        {
            stream.WriteString(node.Name);

            AnimationNodeRaw raw = default;
            raw.PostState = node.PostState;
            raw.PreState = node.PreState;
            raw.PositionKeysCount = (uint)node.PositionKeys.Length;
            raw.RotationKeysCount = (uint)node.RotationKeys.Length;
            raw.ScalingKeysCount = (uint)node.ScalingKeys.Length;

            stream.WriteStructure(raw);

            stream.WriteSpan(node.PositionKeys);
            stream.WriteSpan(node.RotationKeys);
            stream.WriteSpan(node.ScalingKeys);
        }

        /// <summary>
        /// Derializes an <see cref="Animation"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="Animation"/> with data read from the <paramref name="stream"/>.</returns>
        public static Animation ReadAnimation(this Stream stream)
        {
            var name = stream.ReadString();
            AnimationRaw raw = stream.ReadStructure<AnimationRaw>();

            var channels = new AnimationNode[raw.ChannelsCount];
            for (int i = 0; i < raw.ChannelsCount; i++)
                channels[i] = ReadAnimationNode(stream);

            return new Animation(name, raw.Duration, raw.TicksPerSecond, channels);
        }

        /// <summary>
        /// Derializes an <see cref="AnimationNode"/> node from a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="AnimationNode"/> with data read from the <paramref name="stream"/>.</returns>
        public static AnimationNode ReadAnimationNode(this Stream stream)
        {
            var name = stream.ReadString();
            AnimationNodeRaw raw = stream.ReadStructure<AnimationNodeRaw>();
            return new AnimationNode(
                name,
                stream.ReadSpan<Vector3Key>(raw.PositionKeysCount),
                stream.ReadSpan<QuaternionKey>(raw.RotationKeysCount),
                stream.ReadSpan<Vector3Key>(raw.ScalingKeysCount));
        }
    }
}
