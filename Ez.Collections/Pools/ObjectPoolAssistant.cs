// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace Ez.Collections.Pools
{
    /// <summary>
    /// An default implementation of <see cref="IObjectPoolAssistant{T}"/> that uses
    /// delegates for <see cref="Create(object[])"/> and <see cref="Evaluate(in T, object[])"/>.
    /// </summary>
    /// <typeparam name="T">The type that assistant evaluates and creates.</typeparam>    
    public sealed class ObjectPoolAssistant<T> : IObjectPoolAssistant<T>
    {
        /// <summary>
        /// A delegate that wraps a create function based on a TSpecs value.
        /// </summary>
        /// <param name="args">Arguments for the object taken from the pool.</param>
        /// <returns>A new T item.</returns>
        public delegate T CreateFunction(params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public delegate bool EvaluateFunction(in T item, params object[] args);

        private readonly CreateFunction _create;
        private readonly EvaluateFunction _evaluate;
        private readonly uint _clearCount;
        private uint _count;

        /// <summary>
        /// Initializes a new instance of <see cref="ObjectPoolAssistant{T}"/> thats wraps a create function and evaluate function.
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

        /// <inheritdoc/>
        public T Create(params object[] args) => _create(args);

        /// <inheritdoc/>
        public bool Evaluate(in T item, params object[] args) =>
            _evaluate.Invoke(item, args);

        /// <inheritdoc/>
        public bool IsClear()
        {
            return _count >= _clearCount;
        }

        /// <inheritdoc/>
        public void RegisterReturn(in T item)
        {
            _count++;
        }

        /// <inheritdoc/>
        public void RegisterGet(in T item)
        {
            _count--;
        }
    }
}
