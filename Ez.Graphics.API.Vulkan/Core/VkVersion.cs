// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.API.Vulkan.Core
{
    public struct VkVersion
    {
        public VkVersion(byte variant, byte major, ushort minor, ushort patch)
        {
            // 3 bits
            if (variant > 7)
                throw new ArgumentOutOfRangeException(nameof(variant));

            // 7 bits
            if (major > 127)
                throw new ArgumentOutOfRangeException(nameof(major));

            // 10 bits
            if (minor > 1023)
                throw new ArgumentOutOfRangeException(nameof(minor));

            // 12 bits
            if (patch > 4095)
                throw new ArgumentOutOfRangeException(nameof(patch));

            Value = ((uint)variant << 29) | ((uint)major << 22) | ((uint)minor << 12) | patch;
        }

        public VkVersion(uint value)
        {
            Value = value;
        }

        public byte Variant
        {
            get => (byte)(Value >> 29);
            set
            {
                if (value > 7)
                    throw new ArgumentOutOfRangeException(nameof(value));

                Value = ((uint)value << 29) | (Value & 0x1FFF_FFFF);
            }
        }

        public byte Major
        {
            get => (byte)((Value >> 22) & 0x7F);
            set
            {
                if (value > 127)
                    throw new ArgumentOutOfRangeException(nameof(value));

                Value = ((uint)value << 22) | (Value & 0xE03F_FFFF);
            }
        }

        public ushort Minor
        {
            get => (ushort)((Value >> 12) & 0x3FF);
            set
            {
                if (value > 1023)
                    throw new ArgumentOutOfRangeException(nameof(value));

                Value = ((uint)value << 12) | (Value & 0xFFC0_0FFF);
            }
        }

        public ushort Patch
        {
            get => (ushort)(Value & 0xFFF);
            set
            {
                if (value > 4095)
                    throw new ArgumentOutOfRangeException(nameof(value));
                Value = value | (Value & 0xFFFF_F000);
            }
        }

        public uint Value { get; set; }

        public static implicit operator uint(VkVersion version) => version.Value;
        public static explicit operator VkVersion(uint value) => new(value);
    }
}
