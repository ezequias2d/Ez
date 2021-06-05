// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ez.Graphics.Data.Animations
{
    /// <summary>
    /// An animation consists of keyframe data for a number of nodes.
    /// 
    /// For each node affected by the animation a separate series of data is given.
    /// </summary>
    public sealed class Animation : IEquatable<Animation>
    {
        private readonly int _hashcode;
        private readonly AnimationNode[] _channels;
        private readonly int _channelsCount;

        /// <summary>
        /// Initializes a new instance of <see cref="Animation"/> class.
        /// </summary>
        /// <param name="name">The name of the animation.</param>
        /// <param name="duration">The animation duration in ticks.</param>
        /// <param name="ticksPerSecond">The number of ticks per second.</param>
        /// <param name="channels">The channels of animation.</param>
        public Animation(string name, double duration, double ticksPerSecond, ReadOnlySpan<AnimationNode> channels)
        {
            Name = name;
            Duration = duration;
            TicksPerSecond = ticksPerSecond;

            InternalHelper.SetManaged(ref _channels, ref _channelsCount, channels);

            _hashcode = HashHelper<Animation>.Combine(HashHelper<AnimationNode>.Combine(Channels), Duration, Name, TicksPerSecond);
        }

        /// <summary>
        /// The animation node channels.
        /// </summary>
        public ReadOnlySpan<AnimationNode> Channels 
            => new ReadOnlySpan<AnimationNode>(_channels, 0, _channelsCount);

        /// <summary>
        /// Duration of the animation in ticks.
        /// </summary>
        public double Duration { get; }

        /// <summary>
        /// The name of the animation.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Ticks per second.
        /// </summary>
        public double TicksPerSecond { get; }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Animation"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Animation"/>.</param>
        /// <returns><see langword="true"/> if the two animations are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Animation other) =>
            ReferenceEquals(this, other) || 
            (GetHashCode() == other.GetHashCode() &&
                Duration == other.Duration &&
                TicksPerSecond == other.TicksPerSecond &&
                MemoryExtensions.SequenceEqual(Channels, other.Channels));

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            ReferenceEquals(this, obj) || 
            (obj is Animation animation && Equals(animation));

        /// <summary>
        /// Gets the hashcode of the animation data.
        /// </summary>
        public override int GetHashCode() => _hashcode;
    }
}
