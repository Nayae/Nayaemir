using Nayaemir.Core.Resources.Component;
using Nayaemir.Core.Resources.Component.Registries;
using Nayaemir.Core.Resources.Content;
using Nayaemir.Core.Resources.Graphics;
using Nayaemir.Core.Resources.Graphics.Registries;

namespace Nayaemir.Core.Resources;

internal static class ResourceRegistry
{
    private static readonly Dictionary<Type, Dictionary<Enum, IResourceRegistry>> _registries;

    static ResourceRegistry()
    {
        _registries = new Dictionary<Type, Dictionary<Enum, IResourceRegistry>>
        {
            {
                typeof(GraphicsResourceEnum), new Dictionary<Enum, IResourceRegistry>
                {
                    { GraphicsResourceEnum.BufferObject, new BufferObjectRegistry() },
                    { GraphicsResourceEnum.VertexArrayObject, new VertexArrayObjectRegistry() },
                    { GraphicsResourceEnum.ShaderObject, new ShaderObjectRegistry() }
                }
            },
            {
                typeof(ComponentResourceEnum), new Dictionary<Enum, IResourceRegistry>
                {
                    { ComponentResourceEnum.Mesh, new MeshRegistry() }
                }
            },
            {
                typeof(ContentResourceEnum), new Dictionary<Enum, IResourceRegistry>()
            }
        };
    }

    public static void Register<T>(T type, IResource resource) where T : Enum =>
        _registries[typeof(T)][type].Register(resource);

    public static void Release<T>(T type, IResource resource) where T : Enum =>
        _registries[typeof(T)][type].Release(resource);
}

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