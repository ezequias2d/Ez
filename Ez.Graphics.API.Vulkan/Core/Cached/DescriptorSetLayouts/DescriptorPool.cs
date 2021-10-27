// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;
using System.Collections.Generic;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class DescriptorPool : DeviceResource
    {
        private const int MaxSets = 64;

        private readonly DescriptorPoolSize[] _poolSizes;
        private readonly IList<DescriptorPoolNode> _pools;
        private readonly Stack<DescriptorPoolNode> _availables;

        private readonly DescriptorSetLayout _layout;

        public DescriptorPool(Device device, DescriptorSetLayout layout) : base(device)
        {
            _layout = layout;
            _pools = new List<DescriptorPoolNode>();
            _availables = new Stack<DescriptorPoolNode>();

            var dTypeCounts = new Dictionary<DescriptorType, uint>();

            foreach (var binding in _layout.Bindings.Span)
            {
                if (!dTypeCounts.ContainsKey(binding.DescriptorType))
                    dTypeCounts[binding.DescriptorType] = binding.DescriptorCount;
                else
                    dTypeCounts[binding.DescriptorType] += binding.DescriptorCount;
            }

            _poolSizes = new DescriptorPoolSize[dTypeCounts.Count];
            {
                var i = 0;
                foreach (var typeCount in dTypeCounts)
                    _poolSizes[i++] = new()
                    {
                        Type = typeCount.Key,
                        DescriptorCount = typeCount.Value * MaxSets,
                    };
            }
        }

        public DescriptorSet AllocateDescriptorSet()
        {
            DescriptorSet descriptorSet;
            lock (this)
            {
                DescriptorPoolNode pool;

                if (_availables.Count > 0)
                    pool = _availables.Pop();
                else
                    pool = new(Device, _layout, _poolSizes, MaxSets);

                descriptorSet = pool.AllocateDescriptorSet();

                if (pool.Available > 0)
                    _availables.Push(pool);
            }
            return descriptorSet;
        }

        protected override void ManagedDispose()
        {
            foreach (var node in _pools)
                node.Dispose();
        }

        protected override void UnmanagedDispose()
        {
        }
    }
}
