namespace Nayaemir.Core.Resources.Component;

public abstract class ComponentResource : IResource
{
    protected abstract ComponentResourceEnum ResourceType { get; }

    protected ComponentResource()
    {
        ResourceRegistry.Register(ResourceType, this);
    }

    public void Dispose()
    {
        ResourceRegistry.Release(ResourceType, this);
    }
}