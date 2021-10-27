// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Silk.NET.Vulkan;

using VkSemaphore = Silk.NET.Vulkan.Semaphore;
namespace Ez.Graphics.API.Vulkan.Core
{
    public class Semaphore : DeviceResource, ISemaphore
    {
        public Semaphore(Device device) : base(device)
        {
            var sci = new SemaphoreCreateInfo
            {
                SType = StructureType.SemaphoreCreateInfo,
            };
            unsafe
            {
                var result = Device.Vk.CreateSemaphore(Device, sci, null, out var semaphore);
                result.CheckResult();
                VkSemaphore = semaphore;
            }
        }

        private VkSemaphore VkSemaphore { get; }

        public void Reset()
        {
            var info = new SemaphoreSignalInfo
            {
                SType = StructureType.SemaphoreSignalInfo,
                Semaphore = this,
                Value = 0,
            };

            Device.Vk.SignalSemaphore(Device, info);
        }

        public void Set()
        {
            var info = new SemaphoreSignalInfo
            {
                SType = StructureType.SemaphoreSignalInfo,
                Semaphore = this,
                Value = 1,
            };

            Device.Vk.SignalSemaphore(Device, info);
        }

        protected override void ManagedDispose()
        {

        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroySemaphore(Device, VkSemaphore, null);
        }

        public static implicit operator VkSemaphore(Semaphore semaphore)
        {
            semaphore.CheckDispose();
            return semaphore.VkSemaphore;
        }
    }
}
