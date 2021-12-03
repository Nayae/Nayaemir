using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Nayaemir.Core.Resources.Graphics.Types;

public class TextureObject : ApiResource
{
    internal uint Width { get; }
    internal uint Height { get; }

    private static uint _currentId;
    private readonly uint _id;

    public TextureObject(string imagePath)
    {
        var image = Image.Load<Rgba32>(imagePath);

        Width = (uint)image.Width;
        Height = (uint)image.Height;

        _id = Api.GenTexture();

        if (image.TryGetSinglePixelSpan(out var pixels))
        {
            BufferImageData(pixels);
        }
    }

    public TextureObject(Span<Rgba32> pixels, uint width, uint height)
    {
        _id = Api.GenTexture();

        Width = width;
        Height = height;

        BufferImageData(pixels);
    }

    public void Bind()
    {
        if (_currentId != _id)
        {
            Api.BindTexture(TextureTarget.Texture2D, _id);
            _currentId = _id;
        }
    }

    private void BufferImageData(Span<Rgba32> pixels)
    {
        Bind();

        Api.TexImage2D(
            TextureTarget.Texture2D,
            0,
            InternalFormat.Rgba,
            Width,
            Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            pixels.GetPinnableReference()
        );

        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
            (int)TextureWrapMode.ClampToBorder);
        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
            (int)TextureWrapMode.ClampToBorder);
        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        Api.GenerateMipmap(TextureTarget.Texture2D);
    }
}