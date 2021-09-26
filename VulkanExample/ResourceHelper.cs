using Ez.Graphics;
using Ez.Graphics.Data;
using StbiSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace VulkanExample
{
    public static class ResourceHelper
    {
        public static Texture LoadTexture(byte[] data)
        {
            using var stream = new MemoryStream(data);
            return LoadTexture(stream);
        }

        public static Texture LoadTexture(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                StbiImage image = Stbi.LoadFromMemory(memoryStream, 4);
                return new Texture(PixelFormat.R8G8B8A8UNorm, (uint)image.Width, (uint)image.Height, 1, 1, 1, TextureType.Texture2D, image.Data);
            }
        }
    }
}
