// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using Ez.IO;
using Ez.Numerics;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a transformation with hierarchy support.
    /// </summary>
    public sealed class Transform
    {
        private Vector3 _position;
        private Vector3 _scale;
        private Vector3 _eulerAngles;
        private Quaternion _rotation;
        private Matrix4x4 _transform;
        private bool _transformChange;

        /// <summary>
        /// Gets the position of this transform.
        /// </summary>
        public Vector3 Position 
        {
            get => _position;
            set
            {
                if(_position != value)
                {
                    _position = value;
                    _transformChange = true;
                }
            }
        }

        /// <summary>
        /// Gets the scale of this transform.
        /// </summary>
        public Vector3 Scale 
        {
            get => _scale;
            set
            {
                if(_scale != value)
                {
                    _scale = value;
                    _transformChange = true;
                }
            }
        }

        /// <summary>
        /// Gets the rotation in euler angles format of this transform.
        /// </summary>
        public Vector3 EulerAngles 
        {
            get => _eulerAngles;
            set
            {
                if(_eulerAngles != value)
                {
                    _eulerAngles = value;
                    _rotation = EzMath.ToQuaternion(value);
                    _transformChange = true;
                }
            }
        }
        /// <summary>
        /// Gets the rotation of this transform.
        /// </summary>
        public Quaternion Rotation 
        {
            get => _rotation;
            set
            {
                if(_rotation != value)
                {
                    _rotation = value;
                    _eulerAngles = EzMath.ToEulerAngles(value);
                    _transformChange = true;
                }
            }
        }

        /// <summary>
        /// Gets the local transform matrix of this transform.
        /// </summary>
        public Matrix4x4 TransformMatrix
        {
            get
            {
                if (_transformChange)
                {
                    _transform = Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateTranslation(Position);
                    _transformChange = false;
                }
                return _transform;
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether the transform is static. 
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Gets the global transform matrix of this transform.
        /// </summary>
        public Matrix4x4 GlobalTransformMatrix
        {
            get
            {
                if(Father != null)
                    return TransformMatrix * Father.GlobalTransformMatrix;
                return TransformMatrix;
            }
        }

        /// <summary>
        /// The father transform.
        /// </summary>
        public Transform Father { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Transform"/> class.
        /// </summary>
        /// <param name="transformMatrix">The local transform matrix.</param>
        /// <param name="father">The father transform.</param>
        public Transform(Matrix4x4 transformMatrix, Transform father)
        {
            _transform = transformMatrix;
            Matrix4x4.Decompose(transformMatrix, out _scale, out _rotation, out _position);
            _eulerAngles = _rotation.ToEulerAngles();
            IsStatic = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Transform"/> class.
        /// </summary>
        /// <param name="position">The local position of transform.</param>
        /// <param name="scale">The local scale of transform.</param>
        /// <param name="eulerAngles">The local euler angles of transform.</param>
        /// <param name="father">The father transform.</param>
        public Transform(Vector3 position, Vector3 scale, Vector3 eulerAngles, Transform father)
        {
            _scale = scale;
            _position = position;
            _eulerAngles = eulerAngles;
            _rotation = _eulerAngles.ToQuaternion();
            Father = father;

            _transformChange = true;
            IsStatic = false;
        }
    }
}
