// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Specify how contents of an attachment are treated at the beginning
    /// of a subpass.
    /// </summary>
    public enum AttachmentLoadOperation
    {
        /// <summary>
        /// Specifies that the previous contents of the texture within the 
        /// render area will be preserved. 
        /// </summary>
        Load,
        /// <summary>
        /// Specifies that the contents within the render area will be cleared 
        /// to a uniform value, which is specified when a render pass instance
        /// is begun.
        /// </summary>
        Clear,
        /// <summary>
        /// Specifies that the previous contents within the area need not be 
        /// preserved; the contents of the attachment will be undefined inside 
        /// the render area.
        /// </summary>
        DontCare
    }
}
