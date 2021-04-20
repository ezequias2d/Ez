// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Meshes;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a <see cref="Scene"/> instance.
    /// </summary>
    public struct Instance
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Instance"/> structure.
        /// </summary>
        /// <param name="scene">The scene of the <see cref="Instance"/>.</param>
        /// <param name="meshIndex">The index of <see cref="Mesh"/> in <paramref name="scene"/>.</param>
        /// <param name="transformIndex">The index of <see cref="Transform"/> in <paramref name="scene"/>.</param>
        public Instance(Scene scene, int meshIndex, int transformIndex)
        {
            MeshIndex = new SceneIndex<Mesh>(meshIndex, scene);
            TransformIndex = new SceneIndex<Transform>(transformIndex, scene);
        }

        /// <summary>
        /// The mesh index of this instance.
        /// </summary>
        public SceneIndex<Mesh> MeshIndex;

        /// <summary>
        /// The transform index of this instance.
        /// </summary>
        public SceneIndex<Transform> TransformIndex;
    }
}
