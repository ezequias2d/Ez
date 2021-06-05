// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections;
using Ez.Collections.Pools;
using Ez.Graphics.Data.Animations;
using Ez.Graphics.Data.Cameras;
using Ez.Graphics.Data.Materials;
using Ez.Graphics.Data.Meshes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a graphic scene.
    /// </summary>
    public class Scene : IResettable
    {
        private readonly List<Light> _lights;
        private readonly List<Camera> _cameras;
        private readonly List<Animation> _animations;
        private readonly List<Instance> _instances;

        /// <summary>
        /// Initializes a new instance of <see cref="Scene"/> class.
        /// </summary>
        /// <param name="lights">The lights to add.</param>
        /// <param name="cameras">The cameras to add.</param>
        /// <param name="animations">The animations to add.</param>
        /// <param name="instances">The instances to add.</param>
        public Scene(
            IEnumerable<Light> lights = null, 
            IEnumerable<Camera> cameras = null, 
            IEnumerable<Animation> animations = null,
            IEnumerable<Instance> instances = null)
        {
            (_lights, _cameras, _animations, _instances) = 
                    (new List<Light>(lights ?? Enumerable.Empty<Light>()), 
                        new List<Camera>(cameras ?? Enumerable.Empty<Camera>()), 
                        new List<Animation>(animations ?? Enumerable.Empty<Animation>()),
                        new List<Instance>(instances ?? Enumerable.Empty<Instance>()));
        }


        /// <summary>
        /// Gets the list of lights in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<Light> Lights => _lights;

        /// <summary>
        /// Gets the list of cameras in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<Camera> Cameras => _cameras;

        /// <summary>
        /// Gets the list of animations in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<Animation> Animations => _animations;

        /// <summary>
        /// Gets the list of instance in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<Instance> Instances => _instances;

        /// <summary>
        /// Gets or sets the index of main camera.
        /// </summary>
        public int MainCameraIndex { get; set; }

        /// <summary>
        /// Gets the main camera.
        /// </summary>
        public Camera MainCamera
        {
            get
            {
                if (MainCameraIndex >= 0 && MainCameraIndex < Cameras.Count)
                    return Cameras[MainCameraIndex];
                throw new IndexOutOfRangeException($"The {nameof(MainCameraIndex)} is invalid.");
            }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _lights.Clear();
            _cameras.Clear();
            _animations.Clear();
            _instances.Clear();
        }

        /// <inheritdoc/>
        public void Set()
        {
            _lights.Clear();
            _cameras.Clear();
            _animations.Clear();
            _instances.Clear();
            MainCameraIndex = 0;
        }
    }
}
