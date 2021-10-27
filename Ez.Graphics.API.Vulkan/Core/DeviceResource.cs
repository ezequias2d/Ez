// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Resources;
using Silk.NET.Vulkan;
using System;
using System.Text;

namespace Ez.Graphics.API.Vulkan.Core
{
    public abstract class DeviceResource : Disposable, IResource
    {
        private string _name;

        internal protected DeviceResource(Device device)
        {
            Device = device;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    SetName(value);
                }
            }
        }

        public Device Device { get; }

        private protected ulong ObjectHandle { get; set; }

        private protected ObjectType ObjectType { get; set; }

        IDevice IResource.Device => Device;

        public virtual bool Equals(IResource other) =>
            other is DeviceResource dr && ObjectHandle == dr.ObjectHandle && ObjectType == dr.ObjectType;

        private protected virtual void SetName(string name) =>
            SetObjectName(ObjectType, ObjectHandle, name);

        private unsafe protected void SetObjectName(ObjectType objectType, ulong objectHandle, string name)
        {
            if (Device.DebugUtils != null && objectType != ObjectType.Unknown)
            {
                var byteCount = Encoding.UTF8.GetByteCount(name);
                var objectName = stackalloc byte[byteCount + 1];

                // null terminated string
                objectName[byteCount] = 0;

                Encoding.UTF8.GetBytes(name, new Span<byte>(objectName, byteCount));


                var result = Device.DebugUtils.SetDebugUtilsObjectName(Device, new DebugUtilsObjectNameInfoEXT()
                {
                    SType = StructureType.DebugUtilsObjectNameInfoExt,
                    ObjectType = objectType,
                    ObjectHandle = objectHandle,
                    PObjectName = objectName
                });

                result.CheckResult();
            }
        }
    }
}
