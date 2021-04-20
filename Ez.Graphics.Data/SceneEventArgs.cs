// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Provides data for a <see cref="EventHandler{T}"/> event in <see cref="Scene"/>.
    /// </summary>
    public sealed class SceneEventArgs : EventArgs
    {
        /// <summary>
        /// The scene that the event was invoked.
        /// </summary>
        public Scene Scene { get; }

        /// <summary>
        /// The old index of the <see cref="Value"/>.
        /// </summary>
        public int OldIndex { get; }

        /// <summary>
        /// The index of the <see cref="Value"/>.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The action of the scene event.
        /// </summary>
        public SceneAction Action { get; }

        /// <summary>
        /// The influenced object.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="SceneEventArgs"/> class.
        /// </summary>
        /// <param name="scene">The scene that the event was invoked.</param>
        /// <param name="index">The index of the <paramref name="value"/>.</param>
        /// <param name="oldIndex">The old index of the <paramref name="value"/>.</param>
        /// <param name="action">The action of the scene event.</param>
        /// <param name="value">The influenced object.</param>
        public SceneEventArgs(Scene scene, int index, int oldIndex, SceneAction action, object value)
        {
            (Scene, Index, Action, Value) = (scene, index, action, value);
        }
    }
}
