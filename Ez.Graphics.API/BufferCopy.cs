// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specifying a buffer copy operation.
    /// </summary>
    public struct BufferCopy
    {
        /// <summary>
        /// Specifies the starting offset in bytes from the start of the source buffer.
        /// </summary>
        public long SrcOffset { get; set; }

        /// <summary>
        /// Specifies the starting offset in bytes from the start of the destination buffer.
        /// </summary>
        public long DstOffset { get; set; }

        /// <summary>
        /// Specifies the number of bytes to copy.
        /// </summary>
        public long Size { get; set; }
    }
}
