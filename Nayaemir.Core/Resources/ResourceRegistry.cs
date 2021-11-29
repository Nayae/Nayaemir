namespace Nayaemir.Core.Resources;

internal abstract class ResourceRegistry<T> : IResourceRegistry where T : IResource
{
    protected abstract void OnRegister(T resource);

    public void Register(IResource resource)
    {
#if DEBUG
        if (typeof(T) != resource.GetType())
        {
            throw new Exception();
        }
#endif

        OnRegister((T)resource);
    }

    protected abstract void OnRelease(T resource);

    public void Release(IResource resource)
    {
#if DEBUG
        if (typeof(T) != resource.GetType())
        {
            throw new Exception();
        }
#endif

        OnRelease((T)resource);
    }
}