// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.CreateInfos;
using Ez.Graphics.API.Resources;
using Ez.Memory;
using Microsoft.Extensions.Logging;
using Silk.NET.Vulkan;
using System;
using System.Text;
using Vortice.ShaderCompiler;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class Shader : DeviceResource, IShader
    {
        private readonly uint[] _spirv;

        public Shader(Device device, ShaderCreateInfo createInfo) : base(device)
        {
            Stage = createInfo.Stages;
            VkStage = Stage.ToVk();
            EntryPoint = createInfo.EntryPoint;

            switch (createInfo.Format)
            {
                case ShaderFormat.SpirV:
                    _spirv = new uint[createInfo.Source.Length / sizeof(uint)];
                    MemUtil.Copy<uint, byte>(_spirv, createInfo.Source.Span);
                    break;
                case ShaderFormat.Glsl:
                    _spirv = CompilerGlsl(createInfo);
                    break;
                default:
                    throw new VkException("Not supported shader format.");
            }

            unsafe
            {
                fixed (uint* pSpirv = _spirv)
                {

                    var ci = new ShaderModuleCreateInfo
                    {
                        SType = StructureType.ShaderModuleCreateInfo,
                        PNext = null,
                        CodeSize = (nuint)_spirv.Length * sizeof(uint),
                        PCode = pSpirv,
                    };

                    var result = Device.Vk.CreateShaderModule(Device.Handle, ci, null, out var handle);
                    result.CheckResult("Failed to create shader module!");
                    Handle = handle;
                }
            }
        }

        public ShaderStageFlags VkStage { get; }
        public ShaderModule Handle { get; }
        public string EntryPoint { get; }


        public ShaderStages Stage { get; }

        public override bool Equals(IResource other) =>
            other is Shader s && s.Handle.Handle == Handle.Handle;

        public bool Equals(IShader other) =>
            other is Shader s && s.Handle.Handle == Handle.Handle;

        protected override void ManagedDispose()
        {

        }

        protected unsafe override void UnmanagedDispose()
        {
            Device.Vk.DestroyShaderModule(Device.Handle, Handle, null);
        }

        private static ShaderKind ToVortice(ShaderStages stages) =>
            stages switch
            {
                ShaderStages.Compute => ShaderKind.ComputeShader,
                ShaderStages.Fragment => ShaderKind.FragmentShader,
                ShaderStages.Geometry => ShaderKind.GeometryShader,
                ShaderStages.TessellationControl => ShaderKind.TessControlShader,
                ShaderStages.TessellationEvaluation => ShaderKind.TessEvaluationShader,
                ShaderStages.Vertex => ShaderKind.VertexShader,
                _ => throw new NotSupportedException()
            };

        private unsafe uint[] CompilerGlsl(in ShaderCreateInfo createInfo)
        {
            using var compiler = new Compiler();
            var source = Encoding.ASCII.GetString(createInfo.Source.Span);
            var result = compiler.Compile(source, createInfo.Name + "." + createInfo.Stages, ToVortice(createInfo.Stages), createInfo.EntryPoint);

            if (result.Status != CompilationStatus.Success)
                Device.Logger.LogError($"Errors: {result.ErrorsCount}, Warnings: {result.WarningsCount}\n\n{result.ErrorMessage}");
            else if (result.WarningsCount > 0)
                Device.Logger.LogError($"Warnings: {result.WarningsCount}\n\n{result.ErrorMessage}");

            return (new Span<uint>(result.GetBytes(), (int)result.Length / 4)).ToArray();
        }
    }
}
