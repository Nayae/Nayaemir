using Nayaemir.Core.Resources.Component;
using Nayaemir.Core.Resources.Component.Registries;
using Nayaemir.Core.Resources.Content;
using Nayaemir.Core.Resources.Graphics;
using Nayaemir.Core.Resources.Graphics.Registries;

namespace Nayaemir.Core.Resources;

public static class ResourceRegistryContainer
{
    private static readonly Dictionary<GraphicsResourceEnum, IResourceRegistry> _graphicsResourceRegistries;
    private static readonly Dictionary<ComponentResourceEnum, IResourceRegistry> _componentResourceRegistries;
    private static readonly Dictionary<ContentResourceEnum, IResourceRegistry> _contentResourceRegistries;

    static ResourceRegistryContainer()
    {
        _graphicsResourceRegistries = new Dictionary<GraphicsResourceEnum, IResourceRegistry>
        {
            { GraphicsResourceEnum.BufferObject, new BufferObjectRegistry() },
            { GraphicsResourceEnum.VertexArrayObject, new VertexArrayObjectRegistry() },
            { GraphicsResourceEnum.ShaderObject, new ShaderObjectRegistry() }
        };

        _componentResourceRegistries = new Dictionary<ComponentResourceEnum, IResourceRegistry>
        {
            { ComponentResourceEnum.Mesh, new MeshRegistry() }
        };

        _contentResourceRegistries = new Dictionary<ContentResourceEnum, IResourceRegistry>
        {
        };
    }

    public static IResourceRegistry Get(GraphicsResourceEnum type) => _graphicsResourceRegistries[type];
    public static IResourceRegistry Get(ComponentResourceEnum type) => _componentResourceRegistries[type];
    public static IResourceRegistry Get(ContentResourceEnum type) => _contentResourceRegistries[type];
}