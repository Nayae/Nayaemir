using Nayaemir.Core.Resources.Components.Registries;

namespace Nayaemir.Core.Resources;

internal static class ResourceRegistry
{
    private static readonly Dictionary<Type, IResourceRegistry> _registryByResourceType;
    private static readonly Dictionary<Type, IResourceRegistry> _registryByRegistryType;

    static ResourceRegistry()
    {
        _registryByResourceType = new Dictionary<Type, IResourceRegistry>();
        _registryByRegistryType = new Dictionary<Type, IResourceRegistry>();

        // Engine resource registries

        // Graphics resource registries

        // Component resource registries
        CreateRegistry<CameraRegistry>();

        // Content resource registries
    }

    private static void CreateRegistry<T>() where T : IResourceRegistry, new()
    {
        var registry = new T();

        _registryByResourceType[registry.ResourceType] = registry;
        _registryByRegistryType[typeof(T)] = registry;
    }

    public static void Register(IResource resource)
    {
        if (_registryByResourceType.TryGetValue(resource.GetType(), out var registry))
        {
            registry.Register(resource);
        }
    }

    public static void Release(IResource resource)
    {
        if (_registryByResourceType.TryGetValue(resource.GetType(), out var registry))
        {
            registry.Release(resource);
        }
    }

    public static T Get<T>() where T : IResourceRegistry
    {
        if (_registryByRegistryType.TryGetValue(typeof(T), out var registry))
        {
            return (T)registry;
        }

        throw new Exception();
    }
}

internal abstract class ResourceRegistry<T> : IResourceRegistry where T : IResource
{
    public Type ResourceType => typeof(T);

    protected readonly List<T> _resources = new();

    public void Register(IResource resource)
    {
#if DEBUG
        if (typeof(T) != resource.GetType())
        {
            throw new Exception();
        }
#endif

        _resources.Add((T)resource);
        OnRegister((T)resource);
    }

    public void Release(IResource resource)
    {
#if DEBUG
        if (typeof(T) != resource.GetType())
        {
            throw new Exception();
        }
#endif

        _resources.Remove((T)resource);
        OnRelease((T)resource);
    }

    protected virtual void OnRegister(T resource)
    {
    }

    protected virtual void OnRelease(T resource)
    {
    }
}