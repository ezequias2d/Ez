// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Graphics.API.Vulkan.Core.Cached.Framebuffers;
using Ez.Graphics.API.Vulkan.Core.Cached.RenderPasses;
using Ez.Numerics;
using System;
using System.Linq;

namespace Ez.Graphics.API.Vulkan.Core.Cached
{
    internal struct PipelineState : IEquatable<PipelineState>
    {
        public uint ViewportCount;
        public VertexLayoutState[] VertexLayoutStates;
        public InputAssemblyState InputAssemblyState;
        public RasterizationState RasterizationState;
        public MultisampleState MultisampleState;
        public ColorBlendState ColorBlendState;
        public DepthStencilState DepthStencilState;
        public RenderPass RenderPass;
        public Framebuffer Framebuffer;
        public WeakPipeline Pipeline;

        public void Reset()
        {
            ViewportCount = 1;
            InputAssemblyState = InputAssemblyState.Default;
            RasterizationState = RasterizationState.Default;
            MultisampleState = MultisampleState.Default;
            ColorBlendState = ColorBlendState.Default;
            DepthStencilState = DepthStencilState.Default;
        }

        public override int GetHashCode() =>
            HashHelper<PipelineState>.Combine(
                ViewportCount,
                HashHelper<VertexLayoutState>.Combine((ReadOnlySpan<VertexLayoutState>)VertexLayoutStates),
                InputAssemblyState,
                RasterizationState,
                MultisampleState,
                ColorBlendState,
                DepthStencilState,
                Framebuffer,
                Pipeline);

        public bool Equals(PipelineState other)
        {
            var a = ViewportCount == other.ViewportCount &&
            InputAssemblyState == other.InputAssemblyState &&
            RasterizationState == other.RasterizationState &&
            MultisampleState == other.MultisampleState &&
            ColorBlendState == other.ColorBlendState &&
            DepthStencilState == other.DepthStencilState &&
            Framebuffer == other.Framebuffer &&
            Pipeline == other.Pipeline;
            var b = VertexLayoutStates.ToHashSet().SetEquals(other.VertexLayoutStates);

            return a & b;
        }

        public override bool Equals(object obj) =>
            obj is PipelineState gs && Equals(gs);

        public static PipelineState Default => new()
        {
            InputAssemblyState = InputAssemblyState.Default,
            RasterizationState = RasterizationState.Default,
            MultisampleState = MultisampleState.Default,
            DepthStencilState = DepthStencilState.Default,
            ColorBlendState = ColorBlendState.Default,
            Framebuffer = null,
            Pipeline = null,
            RenderPass = null,
            ViewportCount = 1,
            VertexLayoutStates = Array.Empty<VertexLayoutState>(),
        };
    }
}
