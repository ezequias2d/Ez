// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Silk.NET.Vulkan;

using SamplerCreateInfo = Ez.Graphics.API.CreateInfos.SamplerCreateInfo;
using VkSampler = Silk.NET.Vulkan.Sampler;
using VkSamplerCreateInfo = Silk.NET.Vulkan.SamplerCreateInfo;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Sampler : DeviceResource, ISampler
    {
        private readonly VkSampler _handle;
        public Sampler(Device device, SamplerCreateInfo sci) : base(device)
        {
            var filter = sci.Filter.ToVk();

            var vksci = new VkSamplerCreateInfo
            {
                SType = StructureType.SamplerCreateInfo,
                MagFilter = filter.Mag,
                MinFilter = filter.Min,
                AddressModeU = sci.EdgeSampleU.ToVk(),
                AddressModeV = sci.EdgeSampleV.ToVk(),
                AddressModeW = sci.EdgeSampleW.ToVk(),
                AnisotropyEnable = sci.Filter.HasFlag(SamplerFilter.Anisotropic),
                MaxAnisotropy = sci.MaximumAnisotropy,
                UnnormalizedCoordinates = false,
                CompareEnable = sci.CompareOperation != null,
                CompareOp = sci.CompareOperation?.ToVk() ?? CompareOp.Always,
                MipmapMode = sci.Filter.HasFlag(SamplerFilter.MipmapLinear) ?
                    SamplerMipmapMode.Linear :
                    SamplerMipmapMode.Nearest,
                MipLodBias = sci.LodBias,
                MinLod = sci.MinimumLod,
                MaxLod = sci.MaximumLod,

                BorderColor = BorderColor.FloatTransparentBlack,
            };

            unsafe
            {
                var result = Device.Vk.CreateSampler(Device, vksci, null, out _handle);
                result.CheckResult();
            }

            EdgeSampleU = sci.EdgeSampleU;
            EdgeSampleV = sci.EdgeSampleV;
            EdgeSampleW = sci.EdgeSampleW;
            Filter = sci.Filter;
            CompareOperation = sci.CompareOperation;
            MaximumAnisotropy = sci.MaximumAnisotropy;
            MinimumLod = sci.MinimumLod;
            MaximumLod = sci.MaximumLod;
            LodBias = sci.LodBias;
        }

        public EdgeSample EdgeSampleU { get; }

        public EdgeSample EdgeSampleV { get; }

        public EdgeSample EdgeSampleW { get; }

        public SamplerFilter Filter { get; }

        public CompareOperation? CompareOperation { get; }

        public uint MaximumAnisotropy { get; }

        public uint MinimumLod { get; }

        public uint MaximumLod { get; }

        public int LodBias { get; }

        protected override void ManagedDispose()
        {
        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroySampler(Device, this, null);
        }

        public static implicit operator VkSampler(Sampler sampler) => sampler._handle;
    }
}
