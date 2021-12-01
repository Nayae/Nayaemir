using System.Drawing;
using System.Numerics;
using Nayaemir.Core;
using Nayaemir.Core.Resources.Components.Types;
using Nayaemir.Core.Resources.Graphics.Types;
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

    private Camera _camera;
    private Mesh _mesh;

    private ShaderObject _shader;
    private TextureObject _texture;

    protected override void Initialize()
    {
        _mesh = new Mesh(VertexAttributes.Vertices | VertexAttributes.Colors | VertexAttributes.TexCoords, 4);
        _mesh.SetVertices(_vertices);
        _mesh.SetColors(_colors);
        _mesh.SetTextureCoordinates(_texCoords);
        _mesh.SetIndices(_indices);

        _camera = new Camera();
        _camera.SetOrthographicMode(0.01f, 100.0f);
        _camera.SetView(0, 0, -3.0f);

        _texture = new TextureObject("Resources/container.jpg");

        _shader = new ShaderObject("Resources/shader.vert", "Resources/shader.frag");
        _shader.AttachCamera(_camera);
    }

    protected override void Render(float delta)
    {
        Api.Clear(ClearBufferMask.ColorBufferBit);

        _texture.Bind();
        _shader.Bind();

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