namespace Nayaemir.Core.Resources.Content;

public abstract class ContentResource : IResource
{
    protected abstract ContentResourceEnum ResourceType { get; }

    protected ContentResource()
    {
        ResourceRegistry.Register(ResourceType, this);
    }

    public void Dispose()
    {
        ResourceRegistry.Release(ResourceType, this);
    }
}