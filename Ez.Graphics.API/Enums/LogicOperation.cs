// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describe logical operations.
    /// </summary>
    public enum LogicOperation
    {
        /// <summary>
        /// 0
        /// </summary>
        Clear,
        /// <summary>
        /// s ∧ d
        /// </summary>
        And,
        /// <summary>
        /// s ∧ ¬ d
        /// </summary>
        AndReverse,
        /// <summary>
        /// s
        /// </summary>
        Copy,
        /// <summary>
        /// ¬ s ∧ d
        /// </summary>
        AndInverted,
        /// <summary>
        /// d
        /// </summary>
        NoOp,
        /// <summary>
        /// s ⊕ d
        /// </summary>
        Xor,
        /// <summary>
        /// s ∨ d
        /// </summary>
        Or,
        /// <summary>
        /// ¬ (s ∨ d)
        /// </summary>
        Nor,
        /// <summary>
        /// ¬ (s ⊕ d)
        /// </summary>
        Equivalent,
        /// <summary>
        /// ¬ d
        /// </summary>
        Invert,
        /// <summary>
        /// s ∨ ¬ d
        /// </summary>
        OrReverse,
        /// <summary>
        /// ¬ s
        /// </summary>
        CopyInverted,
        /// <summary>
        /// ¬ s ∨ d
        /// </summary>
        OrInverted,
        /// <summary>
        /// ¬ (s ∧ d)
        /// </summary>
        Nand,
        /// <summary>
        /// all 1s
        /// </summary>
        Set,
    }
}
