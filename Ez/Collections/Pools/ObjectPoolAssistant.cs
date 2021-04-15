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
    /// An default implementation of <see cref="IObjectPoolAssistant{T, TSpecs}"/> that uses
    /// delegates for <see cref="Create(in TSpecs)"/> and <see cref="Evaluate(in T, in TSpecs, int)"/>.
    /// </summary>
    /// <typeparam name="T">The type that assistant evaluates and creates.</typeparam>
    /// <typeparam name="TSpecs">The type of data used to evaluate an item.</typeparam>
    public sealed class ObjectPoolAssistant<T, TSpecs> : IObjectPoolAssistant<T, TSpecs>
    {
        /// <summary>
        /// A delegate that wraps a create function based on a TSpecs value.
        /// </summary>
        /// <param name="value">A value used to describe how the created item is.</param>
        /// <returns>A new T item.</returns>
        public delegate T CreateFunction(TSpecs value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="specs"></param>
        /// <param name="currentTolerance"></param>
        /// <returns></returns>
        public delegate bool EvaluateFunction(in T item, object specs, int currentTolerance);

        private readonly CreateFunction _create;
        private readonly EvaluateFunction _evaluate;
        private readonly uint _clearCount;
        private uint _count;

        /// <summary>
        /// Initializes a new instance of <see cref="ObjectPoolAssistant{T, TSpecs}"/> thats wraps a create function and evaluate function.
        /// </summary>
        /// <param name="create">The create function of a T item.</param>
        /// <param name="evaluate">The evaluate function of a T item.</param>
        /// <param name="clearCount">The number of elements counted for <see cref="IsClear"/> returns <see langword="false"/>.</param>
        public ObjectPoolAssistant(CreateFunction create, EvaluateFunction evaluate, uint clearCount = 32)
        {
            _create = create ?? throw new ArgumentNullException(nameof(create));
            _evaluate = evaluate;
            _clearCount = clearCount;
            _count = _clearCount;
        }

        /// <summary>
        /// Creates a new T item that <paramref name="specs"/> describes.
        /// </summary>
        /// <param name="specs">The specifications for creating.</param>
        /// <returns>A new T item.</returns>
        public T Create(in TSpecs specs) => _create(specs);

        /// <summary>
        /// Evaluates whether an <paramref name="item"/> has <paramref name="specs"/> specifications.
        /// </summary>
        /// <param name="item">The item to evaluates.</param>
        /// <param name="specs">The specifications used to evaluate.</param>
        /// <param name="currentTolerance">Current tolerance, to avoid false negatives put 0.</param>
        /// <returns><see langword="false"/>, if the item does not meet the specifications or is a false 
        /// negative based on internal logic using the current tolerance as the main factor, otherwise <see langword="true"/>.</returns>
        public bool Evaluate(in T item, in TSpecs specs, int currentTolerance) =>
            _evaluate.Invoke(item, specs, currentTolerance);

        /// <summary>
        /// Checks if the pool is clean enough to remain uncleaned.
        /// </summary>
        /// <returns><see langword="true"/> if it is clean enough, otherwise <see langword="false"/>.</returns>
        public bool IsClear()
        {
            return _count >= _clearCount;
        }

        /// <summary>
        /// Registers an <paramref name="item"/> returning to the <see cref="ObjectPool{T, TSpecs}"/>.
        /// </summary>
        /// <param name="item">An item to register.</param>
        public void RegisterReturn(in T item)
        {
            _count++;
        }

        /// <summary>
        /// Registers an <paramref name="item"/> leaving the <see cref="ObjectPool{T, TSpecs}"/>.
        /// </summary>
        /// <param name="item">An item to register.</param>
        public void RegisterGet(in T item)
        {
            _count--;
        }
    }
}
