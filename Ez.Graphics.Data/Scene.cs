// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Collections;
using Ez.Graphics.Data.Animations;
using Ez.Graphics.Data.Cameras;
using Ez.Graphics.Data.Materials;
using Ez.Graphics.Data.Meshes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a graphic scene.
    /// </summary>
    public class Scene
    {
        private readonly DiscontiguousList<Mesh> _meshes;
        private readonly DiscontiguousList<Material> _materials;
        private readonly DiscontiguousList<Light> _lights;
        private readonly DiscontiguousList<Camera> _cameras;
        private readonly DiscontiguousList<Animation> _animations;
        private readonly DiscontiguousList<TextureData> _textures;
        private readonly DiscontiguousList<Transform> _transforms;
        private readonly DiscontiguousList<Instance> _instances;

        /// <summary>
        /// Initializes a new instance of <see cref="Scene"/> class.
        /// </summary>
        /// <param name="meshes">The meshes to add.</param>
        /// <param name="materials">The materials to add.</param>
        /// <param name="lights">The lights to add.</param>
        /// <param name="cameras">The cameras to add.</param>
        /// <param name="animations">The animations to add.</param>
        /// <param name="textures">The textures to add.</param>
        /// <param name="transforms">The transforms to add.</param>
        /// <param name="instances">The instances to add.</param>
        public Scene(
            IEnumerable<Mesh> meshes = null, 
            IEnumerable<Material> materials = null, 
            IEnumerable<Light> lights = null, 
            IEnumerable<Camera> cameras = null, 
            IEnumerable<Animation> animations = null, 
            IEnumerable<TextureData> textures = null,
            IEnumerable<Transform> transforms = null,
            IEnumerable<Instance> instances = null)
        {
            (_meshes,
                _materials,
                _lights,
                _cameras,
                _animations,
                _textures,
                _transforms,
                _instances) = 
                    (new DiscontiguousList<Mesh>(meshes ?? Enumerable.Empty<Mesh>()),
                        new DiscontiguousList<Material>(materials ?? Enumerable.Empty<Material>()), 
                        new DiscontiguousList<Light>(lights ?? Enumerable.Empty<Light>()), 
                        new DiscontiguousList<Camera>(cameras ?? Enumerable.Empty<Camera>()), 
                        new DiscontiguousList<Animation>(animations ?? Enumerable.Empty<Animation>()), 
                        new DiscontiguousList<TextureData>(textures ?? Enumerable.Empty<TextureData>()),
                        new DiscontiguousList<Transform>(
                            transforms.Select((v) => new Transform(this, v.TransformMatrix, v.FatherIndex)) ??
                                Enumerable.Empty<Transform>()),
                        new DiscontiguousList<Instance>(
                            instances?.Select((v) => new Instance(this, (int)v.MeshIndex, (int)v.TransformIndex)) ?? 
                            Enumerable.Empty<Instance>()));

            SceneChangeAction += (sender, e) =>
            {
                if(e.OldIndex != -1)
                {
                    if (e.Value is Transform)
                    {
                        for (int i = 0; i < _instances.Count; i++)
                            if ((int)_instances[i].TransformIndex == e.OldIndex)
                            {
                                var instance = _instances[i];
                                instance.TransformIndex = new SceneIndex<Transform>(e.Index, this);
                                _instances[i] = instance;
                            }

                        for (int i = 0; i < _transforms.Count; i++)
                            if (_transforms[i].FatherIndex == e.OldIndex)
                            {
                                var transform = _transforms[i];
                                transform.FatherIndex = e.Index;
                                _transforms[i] = transform;
                            }
                    }
                    else if (e.Value is Mesh)
                    {
                        for (int i = 0; i < _instances.Count; i++)
                            if ((int)_instances[i].MeshIndex == e.OldIndex)
                            {
                                var instance = _instances[i];
                                instance.MeshIndex = new SceneIndex<Mesh>(e.Index, this);
                                _instances[i] = instance;
                            }
                    }
                    else if (e.Value is Texture)
                        ChangeSceneIndexInMaterialProperties<Texture>(e);
                    else if (e.Value is TextureReference)
                        ChangeSceneIndexInMaterialProperties<TextureReference>(e);
                }
            };
        }

        private void ChangeSceneIndexInMaterialProperties<T>(SceneEventArgs e)
        {
            for (int i = 0; i < _materials.Count; i++)
            {
                var material = _materials[i];
                for (int j = 0; j < material.Properties.Length; j++)
                    if (material.Properties[j] is MaterialProperty<SceneIndex<T>> property && property.Value.Index == e.OldIndex)
                        property.Value = new SceneIndex<T>(e.Index, this);
            }
        }

        /// <summary>
        /// Gets the list of meshes in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<Mesh> Meshes => _meshes;

        /// <summary>
        /// Gets the list of materials in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<Material> Materials => _materials;

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
        /// Gets the list of textures in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<TextureData> Textures => _textures;

        /// <summary>
        /// Gets the list of transform in <see cref="Scene"/>.
        /// </summary>
        public IReadOnlyList<Transform> Transforms => _transforms;

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

        #region events
        /// <summary>
        /// Event called when an element of the <see cref="Scene"/> is added, updated, removed or moved .
        /// </summary>
        public event EventHandler<SceneEventArgs> SceneChangeAction;
        #endregion

        /// <summary>
        /// Gets the value of a <see cref="SceneIndex{T}"/>.
        /// </summary>
        /// <typeparam name="T">The value type of <paramref name="index"/>.</typeparam>
        /// <param name="index">The index to remove.</param>
        /// <returns>The value of <paramref name="index"/>.</returns>
        public T Get<T>(SceneIndex<T> index)
        {
            int aux = (int)index;
            object value;
            switch (index)
            {
                case SceneIndex<Mesh> _:
                    value = _meshes[aux];
                    break;
                case SceneIndex<Material> _:
                    value = _materials[aux];
                    break;
                case SceneIndex<Light> _:
                    value = _lights[aux];
                    break;
                case SceneIndex<Camera> _:
                    value = _cameras[aux];
                    break;
                case SceneIndex<Animation> _:
                    value = _animations[aux];
                    break;
                case SceneIndex<TextureData> _:
                    value = _textures[aux];
                    break;
                case SceneIndex<Transform> _:
                    value = _transforms[aux];
                    break;
                case SceneIndex<Instance> _:
                    value = _instances[aux];
                    break;
                default:
                    throw new NotImplementedException($"The Get<T> not supported the type of T: {nameof(T)}");
            }
            return (T)value;
        }

        /// <summary>
        /// Adds a value to the <see cref="Scene"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to add.</typeparam>
        /// <param name="value">The value to add.</param>
        /// <returns>The value-added <see cref="SceneIndex{T}"/>.</returns>
        public SceneIndex<T> Add<T>(in T value)
        {
            int index;
            switch (value)
            {
                case Mesh mesh:
                    index = _meshes.Add(mesh);
                    break;
                case Material material:
                    index = _materials.Add(material);
                    break;
                case Light light:
                    index = _lights.Add(light);
                    break;
                case Camera camera:
                    index = _cameras.Add(camera);
                    break;
                case Animation animation:
                    index = _animations.Add(animation);
                    break;
                case TextureData texture:
                    index = _textures.Add(texture);
                    break;
                case Transform transform:
                    index = _transforms.Add(transform);
                    break;
                case Instance instance:
                    index = _instances.Add(instance);
                    break;
                default:
                    throw new NotImplementedException($"The Add<T> not supported the type of T: {nameof(T)}");
            }
            SceneChangeAction?.Invoke(this, new SceneEventArgs(this, index, -1, SceneAction.Add, value));
            return new SceneIndex<T>(index, this);
        }

        /// <summary>
        /// Removes a index from the <see cref="Scene"/>.
        /// </summary>
        /// <typeparam name="T">The type of value in index.</typeparam>
        /// <param name="index">The index to remove.</param>
        public void Remove<T>(SceneIndex<T> index)
        {
            int intIndex = (int)index;
            
            switch (index)
            {
                case SceneIndex<Mesh> _:
                    _meshes.RemoveAt(intIndex);
                    break;
                case SceneIndex<Material> _:
                    _materials.RemoveAt(intIndex);
                    break;
                case SceneIndex<Light> _:
                    _lights.RemoveAt(intIndex);
                    break;
                case SceneIndex<Camera> _:
                    _cameras.RemoveAt(intIndex);
                    break;
                case SceneIndex<Animation> _:
                    _animations.RemoveAt(intIndex);
                    break;
                case SceneIndex<TextureData> _:
                    _textures.RemoveAt(intIndex);
                    break;
                case SceneIndex<Transform> _:
                    _transforms.RemoveAt(intIndex);
                    break;
                case SceneIndex<Instance> _:
                    _instances.RemoveAt(intIndex);
                    break;
                default:
                    throw new NotImplementedException($"The Remove<T> not supported the type of T: {nameof(T)}");
            }

            SceneChangeAction?.Invoke(this, new SceneEventArgs(this, -1, intIndex, SceneAction.Remove, Get(index)));
        }

        /// <summary>
        /// Update the value of a index.
        /// </summary>
        /// <typeparam name="T">The type of value to update.</typeparam>
        /// <param name="index">The index of value to update.</param>
        /// <param name="value">The </param>
        public void Update<T>(SceneIndex<T> index, in T value)
        {
            var aux = (int)index;
            var objValue = (object)value;
            switch (index)
            {
                case SceneIndex<Mesh> _:
                    _meshes[aux] = (Mesh)objValue;
                    break;
                case SceneIndex<Material> _:
                    _materials[aux] = (Material)objValue;
                    break;
                case SceneIndex<Light> _:
                    _lights[aux] = (Light)objValue;
                    break;
                case SceneIndex<Camera> _:
                    _cameras[aux] = (Camera)objValue;
                    break;
                case SceneIndex<Animation> _:
                    _animations[aux] = (Animation)objValue;
                    break;
                case SceneIndex<TextureData> _:
                    _textures[aux] = (TextureData)objValue;
                    break;
                case SceneIndex<Transform> _:
                    _transforms[aux] = (Transform)objValue;
                    break;
                case SceneIndex<Instance> _:
                    _instances[aux] = (Instance)objValue;
                    break;
                default:
                    throw new NotImplementedException($"The Get<T> not supported the type of T: {nameof(T)}");
            }
            SceneChangeAction?.Invoke(this, new SceneEventArgs(this, aux, aux, SceneAction.Update, value));
        }

        /// <summary>
        /// Creates a new instance, a transform for it and adds to the <see cref="Scene"/>.
        /// </summary>
        /// <param name="meshIndex">The index of the mesh.</param>
        /// <param name="fatherInstanceIndex">The father of the new <see cref="Instance"/>.</param>
        /// <returns>A new <see cref="SceneIndex{T}"/> with <paramref name="meshIndex"/> and <paramref name="fatherInstanceIndex"/>.</returns>
        public SceneIndex<Instance> NewInstance(int meshIndex, int fatherInstanceIndex = -1) =>
            Add(new Instance 
                { 
                    MeshIndex = new SceneIndex<Mesh>(meshIndex, this), 
                    TransformIndex = Add(new Transform(this, Matrix4x4.Identity, fatherInstanceIndex)) 
                });

        /// <summary>
        /// Defragments the spaces between the indexes used.
        /// </summary>
        public void Defrag()
        {
            _meshes.Defrag((a, b) => DefragDelegate(a, b, _meshes[b]));
            _materials.Defrag((a, b) => DefragDelegate(a, b, _materials[b]));
            _lights.Defrag((a, b) => DefragDelegate(a, b, _lights[b]));
            _cameras.Defrag((a, b) => DefragDelegate(a, b, _cameras[b]));
            _animations.Defrag((a, b) => DefragDelegate(a, b, _animations[b]));
            _textures.Defrag((a, b) => DefragDelegate(a, b, _textures[b]));
            _transforms.Defrag((a, b) => DefragDelegate(a, b, _transforms[b]));
            _instances.Defrag((a, b) => DefragDelegate(a, b, _instances[b]));
        }

        private void DefragDelegate(int oldIndex, int index, object value) =>
            SceneChangeAction?.Invoke(this, new SceneEventArgs(this, index, oldIndex, SceneAction.Move, value));
    }
}
