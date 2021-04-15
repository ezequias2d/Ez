// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Collections.Pools
{
    /// <summary>
    /// An interface to implement a assistant used by an <see cref="ObjectPool{T, TSpecs}"/> to evaluate, create, register an object and decide when to clean objects from the pool.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSpecs"></typeparam>
    public interface IObjectPoolAssistant<T, TSpecs>
    {
        /// <summary>
        /// Evaluates whether a <paramref name="item"/> has <paramref name="specs"/> specifications.
        /// </summary>
        /// <param name="item">The item to evaluates.</param>
        /// <param name="specs">The specifications used to evaluate.</param>
        /// <param name="currentTolerance">Current tolerance, to avoid false negatives put 0.</param>
        /// <returns><see langword="false"/>, if the item does not meet the specifications or is a false 
        /// negative based on internal logic using the current tolerance as the main factor, otherwise <see langword="true"/>.</returns>
        bool Evaluate(in T item, in TSpecs specs, int currentTolerance);

        /// <summary>
        /// Creates a new T item that <paramref name="specs"/> describes.
        /// </summary>
        /// <param name="specs">The specifications for creating.</param>
        /// <returns>A new T item.</returns>
        T Create(in TSpecs specs);

        /// <summary>
        /// Registers an <paramref name="item"/> returning to the <see cref="ObjectPool{T, TSpecs}"/>.
        /// </summary>
        /// <param name="item">An item to register.</param>
        void RegisterReturn(in T item);

        /// <summary>
        /// Registers an <paramref name="item"/> leaving the <see cref="ObjectPool{T, TSpecs}"/>.
        /// </summary>
        /// <param name="item">An item to register.</param>
        void RegisterGet(in T item);

        /// <summary>
        /// Checks if the pool is clean enough to remain uncleaned.
        /// </summary>
        /// <returns><see langword="true"/> if it is clean enough, otherwise <see langword="false"/>.</returns>
        bool IsClear();
    }
}
