using System.Numerics;
using Nayaemir.Core.AttributeLayouts;
using Nayaemir.Core.Resources.Graphics.Types;

namespace Nayaemir.Core.Rendering._2D;

public class Texture2D : Rectangle2D
{
    private float _texCoordStartX;
    public float TexCoordStartX
    {
        get => _texCoordStartX;
        set => OnPropertyChanged(ref _texCoordStartX, value);
    }

    private float _texCoordStartY;
    public float TexCoordStartY
    {
        get => _texCoordStartY;
        set => OnPropertyChanged(ref _texCoordStartY, value);
    }

    private float _texCoordEndX;
    public float TexCoordEndX
    {
        get => _texCoordEndX;
        set => OnPropertyChanged(ref _texCoordEndX, value);
    }

    private float _texCoordEndY;
    public float TexCoordEndY
    {
        get => _texCoordEndY;
        set => OnPropertyChanged(ref _texCoordEndY, value);
    }

    private readonly TextureObject _texture;

    public Texture2D(TextureObject texture, bool useFullTexture = true)
    {
        _texture = texture;

        if (useFullTexture)
        {
            _texCoordStartX = 0.0f;
            _texCoordStartY = 0.0f;
            _texCoordEndX = texture.Width;
            _texCoordEndY = texture.Height;
        }
    }

    public override VertexColorTexCoordLayout[] GetRenderObjectData() => new[]
    {
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X + Width, Y, 0),
            Color = Color.ToNormalizedVector(),
            TexCoord = new Vector3(_texCoordEndX / _texture.Width, _texCoordStartY / _texture.Height, 1.0f)
        },
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X + Width, -(Y + Height), 0),
            Color = Color.ToNormalizedVector(),
            TexCoord = new Vector3(_texCoordEndX / _texture.Width, _texCoordEndX / _texture.Height, 1.0f)
        },
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X, Y, 0),
            Color = Color.ToNormalizedVector(),
            TexCoord = new Vector3(_texCoordStartX / _texture.Width, _texCoordStartY / _texture.Height, 1.0f)
        },
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X, -(Y + Height), 0),
            Color = Color.ToNormalizedVector(),
            TexCoord = new Vector3(_texCoordStartX / _texture.Width, _texCoordEndX / _texture.Height, 1.0f)
        }
    };
}