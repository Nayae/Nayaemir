namespace Nayaemir.Core.Resources;

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