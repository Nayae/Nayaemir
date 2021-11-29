using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics;

public abstract class GraphicsResource : IResource
{
    protected GL _api => Engine.Api;

    protected abstract GraphicsResourceEnum ResourceType { get; }

    protected GraphicsResource()
    {
        ResourceRegistryContainer.Get(ResourceType).Register(this);
    }

    public void Dispose()
    {
        ResourceRegistryContainer.Get(ResourceType).Release(this);
    }
}