namespace Nayaemir.Core.Resources;

public abstract class Resource : IDisposable
{
    private IResourceRegistry _registry;

    public void Initialize(IResourceRegistry registry)
    {
        _registry = registry;

        _Initialize();
    }

    protected virtual void _Initialize()
    {
    }

    public void Dispose()
    {
        _registry.Release(this);
    }
}