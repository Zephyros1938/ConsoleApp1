using OpenTK.Graphics.ES20;
using OGL = OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace ConsoleApp1.Shaders
{

    public class Texture
    {
        public readonly ImageResult Tex;
        public readonly string Path;
        public readonly int Handle;

        public Texture(String location, int Handle)
        {
            this.Handle = Handle;
            this.Path = location;
            StbImage.stbi_set_flip_vertically_on_load(1);
            this.Tex = LoadTexture(location);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Tex.Width, Tex.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Tex.Data);
        }

        public ImageResult LoadTexture(String path)
        {
            return ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha); ;
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}