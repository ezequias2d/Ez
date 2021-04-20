// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Materials;
using Ez.Memory;
using Ez.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data.Meshes
{
    /// <summary>
    /// A mesh represents a geometry or model with a single material.
    /// </summary>
    public sealed class Mesh : IEquatable<Mesh>
    {
        #region Fields
        private readonly int _hashcode;

        private readonly Vector3[] _vertices;
        private readonly int _verticesCount;

        private readonly Vector2[] _uvs;
        private readonly int _uvsCount;

        private readonly TriangleFace[] _faces;
        private readonly int _facesCount;

        private readonly Bone[] _bones;
        private readonly int _bonesCount;

        private readonly ColorSingle[] _colors;
        private readonly int _colorsCount;

        private readonly Vector3[] _normals;
        private readonly int _normalsCount;

        private readonly Vector3[] _tangents;
        private readonly int _tangetsCount;

        private readonly Vector3[] _bitangents;
        private readonly int _bitangentsCount;
        #endregion

        /// <summary>
        /// Initialize a new instance of <see cref="Mesh"/> class.
        /// </summary>
        /// <param name="name">The name of the mesh.</param>
        /// <param name="material">The material index in a <see cref="Scene"/> instance.</param>
        /// <param name="vertices">The vertices data.</param>
        /// <param name="uvs">The UVs data.</param>
        /// <param name="normals">The normals data.</param>
        /// <param name="faces">The triangles faces data.</param>
        /// <param name="bones">The bones of mesh.</param>
        /// <param name="tangents">The tangents data.</param>
        /// <param name="bitangents">The bitangents data.</param>
        /// <param name="colors">The colors data.</param>
        public Mesh(
            string name,
            SceneIndex<Material> material,
            ReadOnlySpan<Vector3> vertices,
            ReadOnlySpan<Vector2> uvs,
            ReadOnlySpan<Vector3> normals,
            ReadOnlySpan<TriangleFace> faces = default,
            ReadOnlySpan<Bone> bones = default,
            ReadOnlySpan<Vector3> tangents = default,
            ReadOnlySpan<Vector3> bitangents = default,
            ReadOnlySpan<ColorSingle> colors = default)
        {
            if (vertices.Length % 3 != 0 && uvs.Length % 2 != 0 && normals.Length % 3 != 0)
                throw new ArgumentException("Elements are missing from the matrices, recognized by not having a number of elements to complete the structure, that is, at least one of the length of the vertices, uvs and normals is not a multiple of 3, 2 and 3 respectively.");
            if (!((vertices.Length / 3 == uvs.Length / 2) && (uvs.Length / 2 == normals.Length / 3)))
                throw new ArgumentException("The length of the vertex, uv and normal matrices are not compatible with each other.");

            // set defaults values.
            {
                _vertices = Array.Empty<Vector3>();
                _uvs = Array.Empty<Vector2>();
                _normals = Array.Empty<Vector3>();
                _faces = Array.Empty<TriangleFace>();
                _tangents = Array.Empty<Vector3>();
                _bitangents = Array.Empty<Vector3>();
                _colors = Array.Empty<ColorSingle>();
                _bones = Array.Empty<Bone>();
                _hashcode =_verticesCount = _uvsCount = _normalsCount = _facesCount = _tangetsCount = _bitangentsCount = _colorsCount = _bonesCount = 0;
            }

            Name = name;
            Material = material;

            GDHelper.Set(ref _vertices, ref _verticesCount, vertices);
            GDHelper.Set(ref _uvs, ref _uvsCount, uvs);
            GDHelper.Set(ref _faces, ref _facesCount, faces);
            GDHelper.SetManaged(ref _bones, ref _bonesCount, bones);
            GDHelper.Set(ref _colors, ref _colorsCount, colors);
            GDHelper.Set(ref _normals, ref _normalsCount, normals);
            GDHelper.Set(ref _tangents, ref _tangetsCount, tangents);
            GDHelper.Set(ref _bitangents, ref _bitangentsCount, bitangents);

            _hashcode = HashHelper<Mesh>.Combine(
                HashHelper<Vector3>.Combine(Vertices),
                HashHelper<Vector2>.Combine(UVs),
                HashHelper<TriangleFace>.Combine(Faces),
                HashHelper<Bone>.Combine(Bones),
                HashHelper<ColorSingle>.Combine(Colors),
                HashHelper<Vector3>.Combine(Normals),
                HashHelper<Vector3>.Combine(Tangents),
                HashHelper<Vector3>.Combine(Bitangents),
                Material);
        }

        /// <summary>
        /// Name of mesh.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Positions of each vertex.
        /// </summary>
        public ReadOnlySpan<Vector3> Vertices 
            => new ReadOnlySpan<Vector3>(_vertices, 0, _verticesCount);

        /// <summary>
        /// Texture coordinates of each vertex.
        /// </summary>
        public ReadOnlySpan<Vector2> UVs
            => new ReadOnlySpan<Vector2>(_uvs, 0, _uvsCount);

        /// <summary>
        /// Faces of each vertex.
        /// </summary>
        public ReadOnlySpan<TriangleFace> Faces 
            => new ReadOnlySpan<TriangleFace>(_faces, 0, _facesCount);

        /// <summary>
        /// The bones of this mesh.
        /// </summary>
        public ReadOnlySpan<Bone> Bones 
            => new ReadOnlySpan<Bone>(_bones, 0, _bonesCount);

        /// <summary>
        /// Vertex color sets.
        /// </summary>
        public ReadOnlySpan<ColorSingle> Colors 
            => new ReadOnlySpan<ColorSingle>(_colors, 0, _colorsCount);

        /// <summary>
        /// Normals of each vertex.
        /// </summary>
        public ReadOnlySpan<Vector3> Normals
            => new ReadOnlySpan<Vector3>(_normals, 0, _normalsCount);

        /// <summary>
        /// Vertex tangents.
        /// 
        /// The tangent of a vertex points in the direction of the positive X texture axis. 
        /// The array contains normalized vectors, null if not present.
        /// The array, when it exists, is the same size as the vertices.
        /// </summary>
        public ReadOnlySpan<Vector3> Tangents => new ReadOnlySpan<Vector3>(_tangents, 0, _tangetsCount);

        /// <summary>
        /// Vertex bitangents.
        /// </summary>
        public ReadOnlySpan<Vector3> Bitangents => new ReadOnlySpan<Vector3>(_bitangents, 0, _bitangentsCount);

        /// <summary>
        /// The material used by this mesh.
        /// </summary>
        public SceneIndex<Material> Material { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => _hashcode;

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Mesh"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Mesh"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="Mesh"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Mesh other) =>
            _hashcode == other._hashcode &&
            Material.Equals(other.Material) &&
                Vertices.SequenceEqual(other.Vertices) &&
                UVs.SequenceEqual(other.UVs) &&
                Normals.SequenceEqual(other.Normals) &&
                Faces.SequenceEqual(other.Faces) &&
                Tangents.SequenceEqual(other.Tangents) &&
                Bitangents.SequenceEqual(other.Bitangents) &&
                Colors.SequenceEqual(other.Colors) &&
                Bones.SequenceEqual(other.Bones);

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Mesh mesh && Equals(mesh);
    }
}
