namespace Nayaemir.Core.Resources.Component;

public abstract class ComponentResource : IResource
{
    protected abstract ComponentResourceEnum ResourceType { get; }

    protected ComponentResource()
    {
        ResourceRegistryContainer.Get(ResourceType).Register(this);
    }

    public void Dispose()
    {
        ResourceRegistryContainer.Get(ResourceType).Release(this);
    }
}