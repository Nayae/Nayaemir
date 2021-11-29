using Nayaemir.Core.Resources.Component;

namespace Nayaemir.Core.Resources.Content;

public abstract class ContentResource : Resource
{
    public ComponentResourceFactory ResourceFactory => ResourceFactories.ComponentResourceFactory;
}