using Nayaemir.Core.Resources.Registries;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Types;

public abstract class GraphicsResource : IDisposable
{
    internal GL Api { get; private set; }

    private IResourceRegistry _registry;

    protected abstract void _Initialize();

    internal void Initialize(GL api, IResourceRegistry registry)
    {
        Api = api;
        _registry = registry;

        _Initialize();
    }

    public void Dispose()
    {
        _registry.Release(this);
    }
}