using System.Drawing;
using System.Numerics;
using Nayaemir.Core;
using Nayaemir.Core.Resources.Component.Types;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir;

public class GameEngine : Engine
{
    private static readonly Vector3[] _vertices =
    {
        new(0.5f, 0.5f, 0.0f),
        new(0.5f, -0.5f, 0.0f),
        new(-0.5f, -0.5f, 0.0f),
        new(-0.5f, 0.5f, 0.0f)
    };

    private static readonly Color[] _colors =
    {
        Color.White,
        Color.White,
        Color.White,
        Color.White
    };

    private static readonly uint[] _indices =
    {
        0, 1, 3, // first triangle
        1, 2, 3 // second triangle
    };

    private static ShaderObject _shader;
    private static Mesh _mesh;

    protected override void Initialize()
    {
        _shader = new ShaderObject("Resources/shader.vert", "Resources/shader.frag");

        _mesh = new Mesh(VertexAttributes.Vertices | VertexAttributes.Colors, 4);
        _mesh.SetVertices(_vertices);
        _mesh.SetColors(_colors);
        _mesh.SetIndices(_indices);
    }

    protected override void Render()
    {
        Api.Clear(ClearBufferMask.ColorBufferBit);

        _shader.Use();
        _mesh.Render();
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        using (var engine = new GameEngine())
        {
            engine.Run();
        }
    }
}