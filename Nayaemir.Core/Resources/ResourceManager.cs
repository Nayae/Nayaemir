using Nayaemir.Core.Resources.Registries;
using Nayaemir.Core.Resources.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources;

internal class ResourceManager
{
    private readonly GL _api;
    private readonly Dictionary<ResourceType, IResourceRegistry> _registries;

    public ResourceManager(GL api)
    {
        _api = api;

        _registries = new Dictionary<ResourceType, IResourceRegistry>
        {
            { ResourceType.BufferObject, new BufferObjectRegistry() },
            { ResourceType.VertexArrayObject, new VertexArrayObjectRegistry() },
            { ResourceType.Shader, new ShaderRegistry() }
        };
    }

    public void Register<T>(ResourceType type, T resource)
    {
        if (resource is GraphicsResource graphicsResource)
        {
            graphicsResource.Initialize(_api, _registries[type]);
        }

        _registries[type].Register(resource);
    }
}