using Nayaemir.Core.Resources.Graphics;

namespace Nayaemir.Core.Resources.Component;

public abstract class ComponentResource : Resource
{
    public GraphicsResourceFactory ResourceFactory => ResourceFactories.GraphicsResourceFactory;
}