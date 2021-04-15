// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ez
{
    /// <summary>
    /// The execption that is thrown by Ez namespace.
    /// </summary>
    public class EzException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EzException"/> class.
        /// </summary>
        public EzException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EzException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EzException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EzException"/> class with serialized data.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/>
        /// reference if no inner exception is specified.</param>
        public EzException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EzException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object 
        /// data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information 
        /// about the source or destination.</param>
        protected EzException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
