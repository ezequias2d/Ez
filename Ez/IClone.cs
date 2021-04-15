﻿// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace Ez
{
    /// <summary>
    /// Supports cloning, which creates a new instance of a class with the specific type.
    /// </summary>
    /// <typeparam name="T">The type of the clone.</typeparam>
    public interface IClone<T>
    {
        /// <summary>
        /// Creates a new T instance that is a copy of the current instance. 
        /// </summary>
        T Clone { get; }
    }
}
