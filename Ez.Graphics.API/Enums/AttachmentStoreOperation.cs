// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specify how contents of an attachment are treated at the end of a subpass.
    /// </summary>
    public enum AttachmentStoreOperation
    {
        /// <summary>
        /// Specifies the contents generated during the render pass and within the
        /// render area are written to memory.
        /// </summary>
        Store,
        /// <summary>
        /// Specifies the contents within the render area are not needed after rendering, 
        /// and may be discarded; the contents of the attachment will be undefined inside
        /// the render area. 
        /// </summary>
        DontCare,
    }
}
