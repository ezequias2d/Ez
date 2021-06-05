// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data.Animations
{
    /// <summary>
    /// Describes the animation of a single node.
    /// 
    /// The name specifies the bone/node which is affected by this animation channel. 
    /// The keyframes are given in three separate series of values, one each for position, rotation and scaling. 
    /// The transformation matrix computed from these values replaces the node's original transformation matrix at a specific time. 
    /// This means all keys are absolute and not relative to the bone default pose. 
    /// 
    /// The order in which the transformations are applied is 
    ///     as usual - scaling, rotation, translation.
    ///     
    ///Note:
    ///     All keys are returned in their correct, chronological order. Duplicate keys don't pass the validation step. 
    ///     Most likely there will be no negative time values, but they are not forbidden also ( so implementations need to cope with them! )
    /// </summary>
    public readonly struct AnimationNode : IEquatable<AnimationNode>
    {
        private readonly Vector3Key[] _positionKeys;
        private readonly QuaternionKey[] _rotationKeys;
        private readonly Vector3Key[] _scalingKeys;

        private readonly int _positionKeysCount;
        private readonly int _rotationKeysCount;
        private readonly int _scalingKeysCount;

        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="AnimationNode"/> class.
        /// </summary>
        /// <param name="name">The name of the animation node.</param>
        /// <param name="positionKeys">The position keys data.</param>
        /// <param name="rotationKeys">The rotation keys data.</param>
        /// <param name="scalingKeys">The scaling keys data.</param>
        /// <param name="preState">The behaviour of the animation before the first key is encountered.</param>
        /// <param name="postState">The behaviour of the animation after the last key was processed.</param>
        public AnimationNode(
            string name,
            ReadOnlySpan<Vector3Key> positionKeys,
            ReadOnlySpan<QuaternionKey> rotationKeys,
            ReadOnlySpan<Vector3Key> scalingKeys,
            AnimationBehaviour preState = AnimationBehaviour.Default,
            AnimationBehaviour postState = AnimationBehaviour.Default)
        {
            Name = name;
            PreState = preState;
            PostState = postState;

            _positionKeys = Array.Empty<Vector3Key>();
            _rotationKeys = Array.Empty<QuaternionKey>();
            _scalingKeys = Array.Empty<Vector3Key>();
            _positionKeysCount = _rotationKeysCount = _scalingKeysCount = 0;

            var positions = Sort(positionKeys);
            var rotations = Sort(rotationKeys);
            var scales = Sort(scalingKeys);

            InternalHelper.Set(ref _positionKeys, ref _positionKeysCount, positions);
            InternalHelper.Set(ref _rotationKeys, ref _rotationKeysCount, rotations);
            InternalHelper.Set(ref _scalingKeys, ref _scalingKeysCount, scales);

            _hashcode = 0;
            _hashcode = HashHelper<AnimationNode>.Combine(
                Name,
                PostState,
                PreState,
                HashHelper<Vector3Key>.Combine(PositionKeys),
                HashHelper<QuaternionKey>.Combine(RotationKeys),
                HashHelper<Vector3Key>.Combine(ScalingKeys));
        }

        /// <summary>
        /// Clone constructor.
        /// </summary>
        /// <param name="animationNode">The <see cref="AnimationNode"/> cloned.</param>
        public AnimationNode(AnimationNode animationNode) : this(
            animationNode.Name, 
            animationNode.PositionKeys, 
            animationNode.RotationKeys, 
            animationNode.ScalingKeys,
            animationNode.PreState,
            animationNode.PostState)
        {

        }

        /// <summary>
        /// The name of the node affected by this animation.
        /// The node must exist and it must be unique.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Defines how the animation behaves after the last key was processed.
        /// The default value is <see cref="AnimationBehaviour.Default"/>(the original transformation matrix of the affected node is taken).
        /// </summary>
        public AnimationBehaviour PostState { get; }

        /// <summary>
        /// Defines how the animation behaves before the first key is encountered.
        /// The default value is <see cref="AnimationBehaviour.Default"/>(the original transformation matrix of the affected node is used).
        /// </summary>
        public AnimationBehaviour PreState { get; }

        /// <summary>
        /// The position keys of this animation channel.
        /// Positions are specified as 3D vector.The array is mNumPositionKeys in size.
        /// If there are position keys, there will also be at least one scaling and one rotation key.
        /// </summary>
        public ReadOnlySpan<Vector3Key> PositionKeys 
            => new ReadOnlySpan<Vector3Key>(_positionKeys, 0, _positionKeysCount);

        /// <summary>
        /// The rotation keys of this animation channel.
        /// Rotations are given as quaternions, which are 4D vectors.The array is mNumRotationKeys in size.
        /// If there are rotation keys, there will also be at least one scaling and one position key.
        /// </summary>
        public ReadOnlySpan<QuaternionKey> RotationKeys 
            => new ReadOnlySpan<QuaternionKey>(_rotationKeys, 0, _rotationKeysCount);

        /// <summary>
        /// The scaling keys of this animation channel.
        /// Scalings are specified as 3D vector.The array is mNumScalingKeys in size.
        /// If there are scaling keys, there will also be at least one position and one rotation key.
        /// </summary>
        public ReadOnlySpan<Vector3Key> ScalingKeys 
            => new ReadOnlySpan<Vector3Key>(_scalingKeys, 0, _scalingKeysCount);

        /// <summary>
        /// Gets the hashcode of the animation node data.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => _hashcode;

        internal static ReadOnlySpan<T> Sort<T>(ReadOnlySpan<T> keys) where T : IAnimationKey
        {
            var array = keys.ToArray();
            Array.Sort(array, (a, b) => a.Time.CompareTo(b.Time));
            return array;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="AnimationNode"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="AnimationNode"/>.</param>
        /// <returns><see langword="true"/> if the two animation nodes are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(AnimationNode other) =>
            _hashcode == other._hashcode &&
            PostState == other.PostState &&
            PreState == other.PreState &&
            MemoryExtensions.SequenceEqual(PositionKeys, other.PositionKeys) &&
            MemoryExtensions.SequenceEqual(RotationKeys, other.RotationKeys) &&
            MemoryExtensions.SequenceEqual(ScalingKeys, other.ScalingKeys);

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is AnimationNode an && Equals(an);
    }
}
