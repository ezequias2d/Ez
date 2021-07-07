// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes comparison functions that can be chosen for stencil or depth tests.
    /// </summary>
    public enum CompareOperation
    {
        /// <summary>
        /// The test always passes.
        /// </summary>
        Always,
        /// <summary>
        /// The test never passes.
        /// </summary>
        Never,
        /// <summary>
        /// Passes if the fragment's value is less than the stored value.
        /// </summary>
        Less,
        /// <summary>
        /// Passess if the fragmnet's value is equal to the stored value.
        /// </summary>
        Equal,
        /// <summary>
        /// Passess if the fragment's value is less thant or equal to the stored value.
        /// </summary>
        LessEqual,
        /// <summary>
        /// Passes if the fragment's value is greater than the stored value.
        /// </summary>
        Greater,
        /// <summary>
        /// Passes if the fragment's value is not equal to the stored value.
        /// </summary>
        NotEqual,
        /// <summary>
        /// Passes if the fragment's value is greater or equal to the stored value.
        /// </summary>
        GreaterEqual
    }
}
