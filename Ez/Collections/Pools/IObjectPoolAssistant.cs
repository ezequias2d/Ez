// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections.Pools
{
    public interface IObjectPoolAssistant<T, TSpecs>
    {
        /// <summary>
        /// Check that the object meets the specification.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="specs">Specification.</param>
        /// <returns></returns>
        bool MeetsExpectation(in T item, in TSpecs specs, int currentTolerance);

        /// <summary>
        /// Create a T object.
        /// </summary>
        /// <returns>A new T object.</returns>
        T Create(in TSpecs specs);

        void RegisterReturn(in T item);
        void RegisterGet(in T item);

        /// <summary>
        /// Return false, if is to clear the memory.
        /// </summary>
        /// <returns>False to clear.</returns>
        bool IsClear();
    }
}
