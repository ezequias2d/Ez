// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.Data.Materials;
using Ez.Graphics.Data.Meshes;
using Ez.Graphics.Data.Serializer.Raws;
using Ez.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Ez.Graphics.Data.Serializer
{
    /// <summary>
    /// A static class that provides serializer and deserializer for <see cref="Mesh"/>.
    /// </summary>
    public static class MeshStreamExtensions
    {
        /// <summary>
        /// Derializes an <see cref="Mesh"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="scene">The scene with the mesh material.</param>
        /// <returns>A new instance of <see cref="Mesh"/> with data read from the <paramref name="stream"/>.</returns>
        public static Mesh ReadMesh(this Stream stream, Scene scene)
        {
            var name = stream.ReadString();

            MeshRaw raw = stream.ReadStructure<MeshRaw>();

            var vertices = stream.ReadSpan<Vector3>(raw.VerticesCount);
            var uvs = stream.ReadSpan<Vector2>(raw.UVsCount);
            var faces = stream.ReadSpan<TriangleFace>(raw.FacesCount);
            var colors = stream.ReadSpan<ColorSingle>(raw.ColorsCount);
            var normals = stream.ReadSpan<Vector3>(raw.NormalsCount);
            var tangents = stream.ReadSpan<Vector3>(raw.TangentsCount);
            var bitangents = stream.ReadSpan<Vector3>(raw.BitangentsCount);
            var material = new SceneIndex<Material>(raw.MaterialIndex, scene);

            Bone[] bones = new Bone[raw.BonesCount];
            for (uint i = 0; i < bones.Length; i++)
                bones[i] = ReadBone(stream);

            return new Mesh(name, material, vertices, uvs, normals, faces, bones, tangents, bitangents, colors);
        }

        /// <summary>
        /// Derializes an <see cref="Bone"/> from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new instance of <see cref="Bone"/> with data read from the <paramref name="stream"/>.</returns>
        public static Bone ReadBone(this Stream stream)
        {
            var name = stream.ReadString();

            BoneRaw raw = stream.ReadStructure<BoneRaw>();

            var offsetMatrix = raw.OffsetMatrix;
            var weights = stream.ReadSpan<VertexWeight>(raw.WeightsCount);

            return new Bone(name, offsetMatrix, weights);
        }

        /// <summary>
        /// Serialize <see cref="Mesh"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="mesh">The mesh to write.</param>
        public static void WriteMesh(this Stream stream, in Mesh mesh)
        {
            stream.WriteString(mesh.Name);

            MeshRaw raw = default;

            raw.VerticesCount = (uint)mesh.Vertices.Length;
            raw.UVsCount = (uint)mesh.UVs.Length;
            raw.FacesCount = (uint)mesh.Faces.Length;
            raw.BonesCount = (uint)mesh.Bones.Length;
            raw.ColorsCount = (uint)mesh.Colors.Length;
            raw.NormalsCount = (uint)mesh.Normals.Length;
            raw.TangentsCount = (uint)mesh.Tangents.Length;
            raw.BitangentsCount = (uint)mesh.Bitangents.Length;
            raw.MaterialIndex = (int)mesh.Material;

            stream.WriteStructure(raw);

            stream.WriteSpan(mesh.Vertices);
            stream.WriteSpan(mesh.UVs);
            stream.WriteSpan(mesh.Faces);
            stream.WriteSpan(mesh.Colors);
            stream.WriteSpan(mesh.Normals);
            stream.WriteSpan(mesh.Tangents);
            stream.WriteSpan(mesh.Bitangents);

            foreach (var bone in mesh.Bones)
                stream.WriteBone(bone);
        }

        /// <summary>
        /// Serialize <see cref="Bone"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write it.</param>
        /// <param name="bone">The bone to write.</param>
        public static void WriteBone(this Stream stream, in Bone bone)
        {
            stream.WriteString(bone.Name);

            BoneRaw raw = default;
            raw.OffsetMatrix = bone.OffsetMatrix;
            raw.WeightsCount = (uint)bone.Weights.Length;

            stream.WriteStructure(raw);
            stream.WriteSpan(bone.Weights);
        }
    }
}
