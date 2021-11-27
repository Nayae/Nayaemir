using Nayaemir.Core.Resources.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources;

public class ResourceFactory
{
    private readonly ResourceManager _resourceManager;

    internal ResourceFactory(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }

    public BufferObject CreateBufferObject<T>(BufferTargetARB target, T[] data, BufferUsageARB usage)
        where T : unmanaged
    {
        var buffer = new BufferObject(target, usage);
        _resourceManager.Register(ResourceType.BufferObject, buffer);

        buffer.SetData(data);

        return buffer;
    }

    public VertexArrayObject CreateVertexArrayObject(
        BufferObject vbo, BufferObject ebo, Dictionary<uint, VertexAttributeType> attributes
    )
    {
        var buffer = new VertexArrayObject(vbo, ebo, attributes);
        _resourceManager.Register(ResourceType.VertexArrayObject, buffer);

        return buffer;
    }

    public VertexArrayObject CreateVertexArrayObject(
        BufferObject vbo, Dictionary<uint, VertexAttributeType> attributes
    )
    {
        var buffer = new VertexArrayObject(vbo, attributes);
        _resourceManager.Register(ResourceType.VertexArrayObject, buffer);

        return buffer;
    }

    public ShaderObject CreateShader(string vertexPath, string fragmentPath)
    {
        var shader = new ShaderObject(vertexPath, fragmentPath);
        _resourceManager.Register(ResourceType.Shader, shader);

        return shader;
    }
}