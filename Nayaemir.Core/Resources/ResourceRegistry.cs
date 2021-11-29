namespace Nayaemir.Core.Resources;

internal abstract class ResourceRegistry<T> : IResourceRegistry where T : IResource
{
    protected abstract void _Register(T resource);

    public void Register(IResource resource)
    {
#if DEBUG
        if (typeof(T) != resource.GetType())
        {
            throw new Exception();
        }
#endif

        _Register((T)resource);
    }

    protected abstract void _Release(T resource);

    public void Release(IResource resource)
    {
#if DEBUG
        if (typeof(T) != resource.GetType())
        {
            throw new Exception();
        }
#endif

        _Release((T)resource);
    }
}