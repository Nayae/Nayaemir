using Nayaemir.Core.Resources;

namespace Nayaemir.Core;

public abstract class Resource<T> : IResource where T : Enum
{
    protected abstract T ResourceType { get; }

    protected Resource()
    {
        ResourceRegistry.Register(ResourceType, this);
    }

    public void Dispose()
    {
        ResourceRegistry.Release(ResourceType, this);
    }
}