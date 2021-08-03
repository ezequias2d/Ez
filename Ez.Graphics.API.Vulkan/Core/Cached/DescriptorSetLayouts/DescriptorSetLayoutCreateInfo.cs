// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Memory;
using Ez.Numerics;
using System;
using System.Linq;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal readonly struct DescriptorSetLayoutCreateInfo : IEquatable<DescriptorSetLayoutCreateInfo>
    {
        private readonly int _hashCode;

        public DescriptorSetLayoutCreateInfo(PipelineCreateInfo createInfo)
        {
            var copy = createInfo.Bindings.OrderBy((a) => a.GetHashCode()).ToArray();
            Bindings = copy;
            _hashCode = HashHelper<DescriptorSetLayoutCreateInfo>.Combine((ReadOnlySpan<SetLayoutBinding>)copy);
        }

        public readonly ReadOnlyMemory<SetLayoutBinding> Bindings { get; }

        public override int GetHashCode() => _hashCode;

        public override bool Equals(object obj) =>
            obj is DescriptorSetLayoutCreateInfo ci && Equals(ci);

        public bool Equals(DescriptorSetLayoutCreateInfo other) =>
            _hashCode == other._hashCode &&
            MemUtil.Equals(Bindings.Span, other.Bindings.Span);

        public static implicit operator DescriptorSetLayoutCreateInfo(PipelineCreateInfo ci) =>
            new(ci);
    }
}
