namespace Nayaemir.Core.Resources;

public abstract class Resource : IDisposable
{
    private IResourceRegistry _registry;

    protected abstract void _Initialize();

    public void Initialize(IResourceRegistry registry)
    {
        _registry = registry;

        _Initialize();
    }

    public void Dispose()
    {
        _registry.Release(this);
    }
}