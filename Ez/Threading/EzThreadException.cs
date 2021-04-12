// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ez.Threading
{
    public class EzThreadException : Exception
    {
        public EzThreadException()
        {
        }

        public EzThreadException(string message) : base(message)
        {
        }

        public EzThreadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EzThreadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
