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
    public class Instance
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Instance"/> class.
        /// </summary>
        /// <param name="mesh">The mesh of the instance.</param>
        /// <param name="transform">The transform of the instance.</param>
        public Instance(Mesh mesh, Transform transform)
        {
            Mesh = mesh;
            Transform = transform;
        }

        /// <summary>
        /// The mesh index of this instance.
        /// </summary>
        public Mesh Mesh;

        /// <summary>
        /// The transform index of this instance.
        /// </summary>
        public Transform Transform;
    }
}
