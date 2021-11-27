using Nayaemir.Core.Resources.Graphics.Registries;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics;

public class GraphicsResourceFactory : ResourceFactory<GraphicsResourceType, GraphicsResource>
{
    protected override Dictionary<GraphicsResourceType, IResourceRegistry> CreateRegistries() => new()
    {
        { GraphicsResourceType.BufferObject, new BufferObjectRegistry() },
        { GraphicsResourceType.VertexArrayObject, new VertexArrayObjectRegistry() },
        { GraphicsResourceType.Shader, new ShaderObjectRegistry() }
    };

    public BufferObject CreateBufferObject<T>(BufferTargetARB target, T[] data, BufferUsageARB usage)
        where T : unmanaged
    {
        var buffer = new BufferObject(target, usage);
        Register(GraphicsResourceType.BufferObject, buffer);

        buffer.SetData(data);

        return buffer;
    }

    public VertexArrayObject CreateVertexArrayObject(
        BufferObject vbo, BufferObject ebo, Dictionary<uint, VertexAttributeType> attributes
    )
    {
        var buffer = new VertexArrayObject(vbo, ebo, attributes);
        Register(GraphicsResourceType.VertexArrayObject, buffer);

        return buffer;
    }

    public VertexArrayObject CreateVertexArrayObject(
        BufferObject vbo, Dictionary<uint, VertexAttributeType> attributes
    )
    {
        var buffer = new VertexArrayObject(vbo, attributes);
        Register(GraphicsResourceType.VertexArrayObject, buffer);

        return buffer;
    }

    public ShaderObject CreateShader(string vertexPath, string fragmentPath)
    {
        var shader = new ShaderObject(vertexPath, fragmentPath);
        Register(GraphicsResourceType.Shader, shader);

        return shader;
    }
}