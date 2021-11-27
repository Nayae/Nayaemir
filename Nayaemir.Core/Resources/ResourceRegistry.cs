namespace Nayaemir.Core.Resources;

internal abstract class ResourceRegistry<T> : IResourceRegistry where T : Resource
{
    protected abstract void _Register(T resource);

    public void Register(Resource resource)
    {
#if DEBUG
        if (typeof(T) != resource.GetType())
        {
            throw new Exception();
        }
#endif

        resource.Initialize(this);
        _Register((T)resource);
    }

    protected abstract void _Release(T resource);

    public void Release(Resource resource)
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