// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Silk.NET.Vulkan;
using VkFence = Silk.NET.Vulkan.Fence;
using VkSemaphore = Silk.NET.Vulkan.Semaphore;
namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Fence : DeviceResource, IFence
    {
        public Fence(Device device) : base(device)
        {
            var fci = new FenceCreateInfo
            {
                SType = StructureType.FenceCreateInfo,
            };

            var sci = new SemaphoreCreateInfo
            {
                SType = StructureType.SemaphoreCreateInfo,
            };

            unsafe
            {
                var result = Device.Vk.CreateFence(Device, fci, null, out var fence);
                result.CheckResult();
                VkFence = fence;

                result = Device.Vk.CreateSemaphore(Device, sci, null, out var semaphore);
                result.CheckResult();
                VkSemaphore = semaphore;
            }

            ObjectHandle = VkFence.Handle;
            ObjectType = ObjectType.Fence;
            this.SetDefaultName();
        }

        public bool Signaled
        {
            get
            {
                this.CheckDispose();
                return Device.Vk.GetFenceStatus(Device, VkFence) == Result.Success;
            }
        }

        private VkFence VkFence { get; }
        private VkSemaphore VkSemaphore { get; }

        public bool Equals(IFence other)
        {
            this.CheckDispose();
            return other is Fence f && VkFence.Handle == f.VkFence.Handle;
        }

        public void Reset()
        {
            this.CheckDispose();
            Device.Vk.ResetFences(Device, 1, VkFence);
        }

        public void Wait(in ulong timeout)
        {
            this.CheckDispose();
            Device.Vk.WaitForFences(Device, 1, VkFence, true, timeout);
        }

        protected override void ManagedDispose()
        {
        }

        protected unsafe override void UnmanagedDispose() =>
            Device.Vk.DestroyFence(Device, VkFence, null);

        public static implicit operator VkFence(Fence d)
        {
            d.CheckDispose();
            return d.VkFence;
        }
    }
}