using Nayaemir.Core.Resources.Component;
using Nayaemir.Core.Resources.Content;
using Nayaemir.Core.Resources.Graphics;

namespace Nayaemir.Core.Resources;

internal static class ResourceFactories
{
    internal static GraphicsResourceFactory GraphicsResourceFactory { get; } = new();
    internal static ContentResourceFactory ContentResourceFactory { get; set; } = new();
    internal static ComponentResourceFactory ComponentResourceFactory { get; set; } = new();
}

public abstract class ResourceFactory<TResourceEnum, TResource>
    where TResourceEnum : Enum
    where TResource : Resource
{
    private readonly Dictionary<TResourceEnum, IResourceRegistry> _registries;

    protected abstract Dictionary<TResourceEnum, IResourceRegistry> CreateRegistries();

    protected ResourceFactory()
    {
        _registries = new Dictionary<TResourceEnum, IResourceRegistry>();
    }

    protected void Register(TResourceEnum type, TResource resource)
    {
        _registries[type].Register(resource);
    }
}