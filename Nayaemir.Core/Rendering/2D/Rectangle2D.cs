using System.Drawing;
using System.Numerics;
using Nayaemir.Core.AttributeLayouts;

namespace Nayaemir.Core.Rendering._2D;

public class Rectangle2D : RenderObject2D
{
    private float _x;
    public float X
    {
        get => _x;
        set => OnPropertyChanged(ref _x, value);
    }

    private float _y;
    public float Y
    {
        get => _y;
        set => OnPropertyChanged(ref _y, value);
    }

    private float _width;
    public float Width
    {
        get => _width;
        set => OnPropertyChanged(ref _width, value);
    }

    private float _height;
    public float Height
    {
        get => _height;
        set => OnPropertyChanged(ref _height, value);
    }

    private Color _color = Color.White;
    public Color Color
    {
        get => _color;
        set => OnPropertyChanged(ref _color, value);
    }

    public override VertexColorTexCoordLayout[] GetRenderObjectData() => new[]
    {
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X + Width, Y, 0),
            Color = Color.ToNormalizedVector()
        },
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X + Width, -(Y + Height), 0),
            Color = Color.ToNormalizedVector()
        },
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X, Y, 0),
            Color = Color.ToNormalizedVector()
        },
        new VertexColorTexCoordLayout
        {
            Vertex = new Vector3(X, -(Y + Height), 0),
            Color = Color.ToNormalizedVector()
        }
    };
}