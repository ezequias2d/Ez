using System.Numerics;

namespace VulkanExample
{
    public struct Vertex
    {
        public Vector2 Position;
        public Vector3 Color;

        public Vertex(Vector2 pos, Vector3 color)
        {
            Position = pos;
            Color = color;
        }
    }
}
