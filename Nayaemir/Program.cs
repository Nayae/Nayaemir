using System.Drawing;
using System.Numerics;
using Nayaemir.Core;
using Silk.NET.OpenGL;

namespace Nayaemir;

public class GameEngine : Engine
{
    private static readonly Vector3[] _vertices =
    {
        new(100.0f, 0.0f, 0.0f), // Top Right
        new(100.0f, -100.0f, 0.0f), // Bottom Right
        new(0.0f, -100.0f, 0.0f), // Bottom Left
        new(0.0f, 0.0f, 0.0f) // Top Left
    };

    private static readonly Color[] _colors =
    {
        Color.Red,
        Color.Green,
        Color.Blue,
        Color.Yellow
    };

    private static readonly Vector2[] _texCoords =
    {
        new(1.0f, 1.0f),
        new(1.0f, 0.0f),
        new(0.0f, 0.0f),
        new(0.0f, 1.0f)
    };

    private static readonly uint[] _indices =
    {
        0, 1, 3, // first triangle
        1, 2, 3 // second triangle
    };

    protected override void Initialize()
    {
    }

    protected override void Render(float delta)
    {
        Api.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
    }
}

internal class Program
{
    private static unsafe void Main(string[] args)
    {
        using (var engine = new GameEngine())
        {
            engine.Run();
        }
    }
}