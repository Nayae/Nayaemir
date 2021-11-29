using Nayaemir.Core.Resources.Component.Registries;
using Nayaemir.Core.Resources.Graphics.Registries;

namespace Nayaemir.Core.Resources;

internal static class ResourceRegistry
{
    private static readonly Dictionary<Type, IResourceRegistry> _registries;

    static ResourceRegistry()
    {
        _registries = new Dictionary<Type, IResourceRegistry>();

        // Graphics resource registries
        CreateRegistry<BufferObjectRegistry>();
        CreateRegistry<VertexArrayObjectRegistry>();
        CreateRegistry<ShaderObjectRegistry>();

        // Component resource registries
        CreateRegistry<MeshRegistry>();

        // Content resource registries
    }

    private static void CreateRegistry<T>() where T : IResourceRegistry, new()
    {
        var registry = new T();
        _registries[registry.ResourceType] = registry;
    }

    public static void Register(IResource resource) => _registries[resource.GetType()].Register(resource);
    public static void Release(IResource resource) => _registries[resource.GetType()].Release(resource);
}

internal abstract class ResourceRegistry<T> : IResourceRegistry where T : IResource
{
    public Type ResourceType => typeof(T);

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