// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace Ez.Graphics.API.Vulkan.Core.Cached.RenderPasses
{
    internal class RenderPassCache : Cache<RenderPassBeginInfo, RenderPass>
    {
        public RenderPassCache(Device device) : base(null)
        {
            Device = device;
        }

        private Device Device { get; }

        public override CreateCached Create => CreateRenderPass;

        private RenderPass CreateRenderPass(in RenderPassBeginInfo renderPassDescription) =>
            new(Device, renderPassDescription);
    }
}
