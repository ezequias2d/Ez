// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Ez.Collections.Pools
{
    /// <summary>
    /// An interface to implement a assistant used by an <see cref="ObjectPool{T}"/> to evaluate, create, register an object and decide when to clean objects from the pool.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectPoolAssistant<T>
    {
        ///<summary>
        /// Evaluates whether a <paramref name="item"/> has <paramref name="args"/> specifications.
        /// </summary>
        /// <param name="item">The item to evaluates.</param>
        /// <param name="args">Arguments for the object taken from the pool.</param>
        /// <returns><see langword="false"/>, if the item does not meet the specifications or is a false 
        /// negative based on internal logic using the current tolerance as the main factor, otherwise <see langword="true"/>.</returns>
        bool Evaluate(in T item, params object[] args);

        /// <summary>
        /// Creates a new T item.
        /// </summary>
        /// <returns>A new T item.</returns>
        T Create(params object[] args);

        /// <summary>
        /// Registers an <paramref name="item"/> returning to the <see cref="ObjectPool{T}"/>.
        /// </summary>
        /// <param name="item">An item to register.</param>
        void RegisterReturn(in T item);

        /// <summary>
        /// Registers an <paramref name="item"/> leaving the <see cref="ObjectPool{T}"/>.
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
