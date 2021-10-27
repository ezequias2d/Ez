using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VulkanExample
{
    public struct UniformBufferObject
    {
        public Matrix4x4 Model;
        public Matrix4x4 View;
        public Matrix4x4 Proj;
    }
}
