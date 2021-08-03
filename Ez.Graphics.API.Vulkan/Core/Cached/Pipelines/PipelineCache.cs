// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Vulkan;

using VkPipelineCache = Silk.NET.Vulkan.PipelineCache;
namespace Ez.Graphics.API.Vulkan.Core.Cached
{
    internal class PipelineCache : Cache<PipelineState, Pipeline>
    {
        private readonly VkPipelineCache _pipelineCache;
        public PipelineCache(Device device) : base(null)
        {
            Device = device;
            var pipelineCacheCreateInfo = new PipelineCacheCreateInfo
            {
                SType = StructureType.PipelineCacheCreateInfo,
            };
            unsafe
            {
                var result = Device.Vk.CreatePipelineCache(Device, pipelineCacheCreateInfo, null, out _pipelineCache);
                result.CheckResult();
            }
        }

        private Device Device { get; }

        public override CreateCached Create => CreatePipeline;

        private Pipeline CreatePipeline(in PipelineState pipelineState) =>
            new(Device, _pipelineCache, pipelineState);

        protected unsafe override void UnmanagedDispose()
        {
            base.UnmanagedDispose();
            Device.Vk.DestroyPipelineCache(Device, _pipelineCache, null);
        }
    }
}
