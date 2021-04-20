// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data.Cameras
{
    /// <summary>
    /// A virtual camera.
    /// </summary>
    public readonly struct Camera : IEquatable<Camera>
    {
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="Camera"/> struct
        /// </summary>
        /// <param name="name"></param>
        /// <param name="up"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="fov"></param>
        /// <param name="orthographicSize"></param>
        /// <param name="nearDistance"></param>
        /// <param name="farDistance"></param>
        /// <param name="aspectRatio"></param>
        /// <param name="cameraMode"></param>
        public Camera(
            string name, 
            Vector3 up, 
            Vector3 position, 
            Quaternion rotation, 
            float fov, 
            float orthographicSize, 
            float nearDistance, 
            float farDistance, 
            float aspectRatio, 
            CameraMode cameraMode)
        {
            Name = name;
            Up = up;
            Position = position;
            Rotation = rotation;
            FieldOfView = fov;
            OrthographicSize = orthographicSize;
            NearDistance = nearDistance;
            FarDistance = farDistance;
            AspectRatio = aspectRatio;
            CameraMode = cameraMode;
            
            _hashcode = 0;
            _hashcode = HashHelper<Camera>.Combine(
                Name,
                Up,
                Position,
                Rotation,
                FieldOfView,
                OrthographicSize,
                NearDistance,
                FarDistance,
                AspectRatio);
        }

        /// <summary>
        /// The name of the camera.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The up vector of camera.
        /// </summary>
        public Vector3 Up { get; }

        /// <summary>
        /// Position of camera in world space.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Rotation of camera in world space.
        /// </summary>
        public Quaternion Rotation { get; }

        /// <summary>
        /// The field of view in the y direction, in radians.
        /// </summary>
        public float FieldOfView { get; }

        /// <summary>
        /// Camera's half-size when in orthographic mode.
        /// </summary>
        public float OrthographicSize { get; }

        /// <summary>
        /// The distance to the near view plane.
        /// </summary>
        public float NearDistance { get; }

        /// <summary>
        /// The distance to the far view plane.
        /// </summary>
        public float FarDistance { get; }

        /// <summary>
        /// The aspect ratio, defined as view space width divided by height.
        /// </summary>
        public float AspectRatio { get; }

        /// <summary>
        /// The camera mode.
        /// </summary>
        public CameraMode CameraMode { get; }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;

        /// <summary>
        /// Creates an othographic perspective matrix from this <see cref="Camera"/>.
        /// </summary>
        /// <returns>The othographic projection matrix.</returns>
        public Matrix4x4 CreateOthographic() =>
            Matrix4x4.CreateOrthographic(OrthographicSize, OrthographicSize * AspectRatio, NearDistance, FarDistance);

        /// <summary>
        /// Creates a perspective projection matrix from this <see cref="Camera"/>.
        /// </summary>
        /// <returns>The perspective projection matrix.</returns>
        public Matrix4x4 CreatePerspective() => Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearDistance, FarDistance);
        
        /// <summary>
        /// Creates a view transform matrix from this <see cref="Camera"/>.
        /// </summary>
        /// <returns>The view transform matrix.</returns>
        public Matrix4x4 CreateView() =>
            Matrix4x4.CreateTranslation(-Position) *
                Matrix4x4.CreateFromQuaternion(Rotation);

        /// <summary>
        /// Creates a projection matrix from this <see cref="Camera"/>
        /// (automatically deciding through <see cref="CameraMode"/>).
        /// </summary>
        /// <returns>The projection matrix.</returns>
        public Matrix4x4 CreateProjection()
        {
            switch (CameraMode)
            {
                case CameraMode.Orthographic:
                    return CreateOthographic();
                case CameraMode.Perspective:
                    return CreatePerspective();
                default:
                    throw new InvalidOperationException("The camera has not mode selected to create a projection matrix.");
            }
        }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Camera"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Camera"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="Camera"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Camera other) =>
            Up == other.Up &&
            Position == other.Position &&
            Rotation == other.Rotation &&
            FieldOfView == other.FieldOfView &&
            OrthographicSize == other.OrthographicSize &&
            NearDistance == other.NearDistance &&
            FarDistance == other.FarDistance &&
            AspectRatio == other.AspectRatio &&
            CameraMode == other.CameraMode;

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Camera camera && Equals(camera);

        /// <summary>
        /// Look the camera to the target.
        /// </summary>
        /// <param name="camera">The camera to look at the <paramref name="target"/>.</param>
        /// <param name="target">The target to be looked at.</param>
        public static Camera LookAt(in Camera camera, in Vector3 target)
        {
            Matrix4x4.Decompose(Matrix4x4.CreateLookAt(camera.Position, target, camera.Up), out _, out Quaternion rot, out _);
            return 
                new Camera(
                    camera.Name, 
                    camera.Up, 
                    camera.Position,
                    rot, 
                    camera.FieldOfView, 
                    camera.OrthographicSize, 
                    camera.NearDistance, 
                    camera.FarDistance, 
                    camera.AspectRatio, 
                    camera.CameraMode);
        }

        /// <summary>
        /// Creates a new translated <see cref="Camera"/>.
        /// </summary>
        /// <param name="camera">The <see cref="Camera"/> to translate.</param>
        /// <param name="translation">The translation to apply.</param>
        /// <returns>A translated <see cref="Camera"/>.</returns>
        public static Camera Translate(in Camera camera, in Vector3 translation) =>
            new Camera(
                    camera.Name,
                    camera.Up,
                    camera.Position + translation,
                    camera.Rotation,
                    camera.FieldOfView,
                    camera.OrthographicSize,
                    camera.NearDistance,
                    camera.FarDistance,
                    camera.AspectRatio,
                    camera.CameraMode);

        /// <summary>
        /// Creates a new camera with specific rotation.
        /// </summary>
        /// <param name="camera">The <see cref="Camera"/> to set.</param>
        /// <param name="rotation">The new rotation of <see cref="Camera"/>.</param>
        /// <returns></returns>
        public static Camera SetRotate(in Camera camera, in Quaternion rotation) =>
            new Camera(
                    camera.Name,
                    camera.Up,
                    camera.Position,
                    rotation,
                    camera.FieldOfView,
                    camera.OrthographicSize,
                    camera.NearDistance,
                    camera.FarDistance,
                    camera.AspectRatio,
                    camera.CameraMode);

        /// <summary>
        /// Creates a new camera with specific postion and rotation.
        /// </summary>
        /// <param name="camera">The camera to set.</param>
        /// <param name="position">The new position of <see cref="Camera"/>.</param>
        /// <param name="rotation">The new rotation of <see cref="Quaternion"/>.</param>
        /// <returns></returns>
        public static Camera SetTransform(in Camera camera, in Vector3 position, in Quaternion rotation) =>
            new Camera(
                    camera.Name,
                    camera.Up,
                    position,
                    rotation,
                    camera.FieldOfView,
                    camera.OrthographicSize,
                    camera.NearDistance,
                    camera.FarDistance,
                    camera.AspectRatio,
                    camera.CameraMode);
    }
}
