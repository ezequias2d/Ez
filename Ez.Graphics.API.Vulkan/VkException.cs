// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Runtime.Serialization;

namespace Ez.Graphics.API.Vulkan
{
    public class VkException : GraphicsException
    {
        public VkException()
        {
        }

        public VkException(string message) : base(message)
        {
        }

        public VkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
