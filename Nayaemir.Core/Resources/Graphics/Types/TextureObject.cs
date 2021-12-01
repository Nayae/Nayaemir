using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Nayaemir.Core.Resources.Graphics.Types;

public class TextureObject : GraphicsResource
{
    private readonly Image<Rgba32> _image;

    private static uint _currentId;
    private readonly uint _id;

    public TextureObject(string imagePath)
    {
        _image = Image.Load<Rgba32>(imagePath);

        _id = Api.GenTexture();
        BufferImageData();
    }

    public void Bind()
    {
        if (_currentId != _id)
        {
            Api.BindTexture(TextureTarget.Texture2D, _id);
            _currentId = _id;
        }
    }

    private void BufferImageData()
    {
        Bind();

        if (_image.TryGetSinglePixelSpan(out var pixels))
        {
            Api.TexImage2D(
                TextureTarget.Texture2D,
                0,
                InternalFormat.Rgba,
                (uint)_image.Width,
                (uint)_image.Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pixels.GetPinnableReference()
            );
        }

        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        Api.GenerateMipmap(TextureTarget.Texture2D);
    }
}