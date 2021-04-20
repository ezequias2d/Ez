// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data.Animations
{
    /// <summary>
    /// Defines how an animation channel behaves outside the defined time range.
    /// 
    /// <seealso cref="AnimationNode.PreState"/>
    /// <seealso cref="AnimationNode.PostState"/>
    /// </summary>
    public enum AnimationBehaviour : uint
    {
        /// <summary>
        /// The value from the default node transformation is taken.
        /// </summary>
        Default,
        /// <summary>
        /// The nearest key value is used without interpolation.
        /// </summary>
        Constant,
        /// <summary>
        /// The value of the nearest two keys is linearly extrapolated for the current time value.
        /// </summary>
        Linear,
        /// <summary>
        /// The animation is repeated.
        /// If the animation key go from n to m and the current time is t, use the value at (t-n) % (|m-n|).
        /// </summary>
        Repeat
    }
}
