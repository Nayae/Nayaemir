namespace Nayaemir.Core.Resources.Registries;

internal abstract class ResourceRegistry<T> : IResourceRegistry
{
    protected abstract void _Register(T resource);

    public void Register(object resource)
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

    public void Release(object resource)
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