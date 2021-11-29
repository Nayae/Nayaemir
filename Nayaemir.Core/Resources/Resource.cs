namespace Nayaemir.Core.Resources;

public abstract class Resource : IResource
{
    protected Resource()
    {
        ResourceRegistry.Register(this);
    }

    public void Dispose()
    {
        ResourceRegistry.Release(this);
    }
}