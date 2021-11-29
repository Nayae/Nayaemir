using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics;

public abstract class GraphicsResource : IResource
{
    protected GL _api => Engine.Api;

    protected abstract GraphicsResourceEnum ResourceType { get; }

    protected GraphicsResource()
    {
        ResourceRegistry.Register(ResourceType, this);
    }

    public void Dispose()
    {
        ResourceRegistry.Release(ResourceType, this);
    }
}