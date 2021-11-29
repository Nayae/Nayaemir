namespace Nayaemir.Core.Resources.Content;

public abstract class ContentResource : IResource
{
    protected abstract ContentResourceEnum ResourceType { get; }

    protected ContentResource()
    {
        ResourceRegistryContainer.Get(ResourceType).Register(this);
    }

    public void Dispose()
    {
        ResourceRegistryContainer.Get(ResourceType).Release(this);
    }
}