// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Graphics.API.Resources
{
    /// <summary>
    /// Provides an interface to a <see cref="IDevice"/> resource.
    /// </summary>
    public interface IResource : IDisposable, IEquatable<IResource>
    {
        /// <summary>
        /// Gets or sets a string that identifies this instance.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the <see cref="IDevice"/> that owns this <see cref="IResource"/>.
        /// </summary>
        IDevice Device { get; }

        /// <summary>
        /// Gets a value that indicating whether this <see cref="IResource"/> has been disposed.
        /// </summary>
        bool IsDispose { get; }
    }
}
