// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Runtime.Serialization;

namespace Ez.Graphics.API
{
    public class GraphicsApiException : GraphicsException
    {
        public GraphicsApiException()
        {
        }

        public GraphicsApiException(string message) : base(message)
        {
        }

        public GraphicsApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GraphicsApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
