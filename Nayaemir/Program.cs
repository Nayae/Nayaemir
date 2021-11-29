using System.Collections.Generic;
using Nayaemir.Core;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir;

public class GameEngine : Engine
{
    private static readonly float[] _vertices =
    {
        // positions      colors
        0.5f, 0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, // top right
        0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, // bottom right
        -0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, // bottom left
        -0.5f, 0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f // top left 
    };

    private static readonly uint[] _indices =
    {
        0, 1, 3, // first triangle
        1, 2, 3 // second triangle
    };

    private BufferObject _vbo;
    private BufferObject _ebo;
    private VertexArrayObject _vao;

    private ShaderObject _shader;

    protected override void Initialize()
    {
        _vbo = new BufferObject(BufferTargetARB.ArrayBuffer, BufferUsageARB.StaticDraw);
        _vbo.SetData(_vertices);

        _ebo = new BufferObject(BufferTargetARB.ElementArrayBuffer, BufferUsageARB.StaticDraw);
        _ebo.SetData(_indices);

        _vao = new VertexArrayObject(_vbo, _ebo, new Dictionary<uint, VertexAttributeType>
        {
            { 0, VertexAttributeType.vec3 },
            { 1, VertexAttributeType.vec4 }
        });

        _shader = new ShaderObject("Resources/shader.vert", "Resources/shader.frag");
    }

    protected override void Render()
    {
        Api.Clear(ClearBufferMask.ColorBufferBit);

        _shader.Use();
        _vao.Render(PrimitiveType.Triangles);
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