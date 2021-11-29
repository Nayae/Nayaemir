using Nayaemir.Core.Resources.Component;
using Nayaemir.Core.Resources.Graphics.Registries;

namespace Nayaemir.Core.Resources.Content;

public class ContentResourceFactory : ResourceFactory<ComponentResourceType, ComponentResource>
{
    protected override Dictionary<ComponentResourceType, IResourceRegistry> CreateRegistries() => new()
    {
        { ComponentResourceType.Mesh, new BufferObjectRegistry() }
    };
}