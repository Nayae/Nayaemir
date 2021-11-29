using Nayaemir.Core.Resources.Component.Registries;

namespace Nayaemir.Core.Resources.Component;

public class ComponentResourceFactory : ResourceFactory<ComponentResourceType, ComponentResource>
{
    protected override Dictionary<ComponentResourceType, IResourceRegistry> CreateRegistries() => new()
    {
        { ComponentResourceType.Mesh, new MeshRegistry() }
    };
}