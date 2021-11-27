using System.Collections.Generic;
using System.Drawing;
using Nayaemir.Core;
using Nayaemir.Core.Rendering;
using Nayaemir.Core.Resources.Types;
using Silk.NET.OpenGL;

namespace Nayaemir;

internal class GameEngine : Engine
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
        _vbo = ResourceFactory.CreateBufferObject(
            BufferTargetARB.ArrayBuffer, _vertices, BufferUsageARB.StaticDraw
        );
        _ebo = ResourceFactory.CreateBufferObject(
            BufferTargetARB.ElementArrayBuffer, _indices, BufferUsageARB.StaticDraw
        );

        _vao = ResourceFactory.CreateVertexArrayObject(
            _vbo, _ebo, new Dictionary<uint, VertexAttributeType>
            {
                { 0, VertexAttributeType.vec3 },
                { 1, VertexAttributeType.vec4 }
            }
        );

        _shader = ResourceFactory.CreateShader("Resources/shader.vert", "Resources/shader.frag");
    }

    protected override void Render(Renderer3D renderer)
    {
        renderer.Clear(Color.CornflowerBlue);
        renderer.UseShader(_shader);

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