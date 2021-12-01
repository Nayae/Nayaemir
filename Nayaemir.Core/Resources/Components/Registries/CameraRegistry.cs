using Nayaemir.Core.Resources.Components.Types;
using Nayaemir.Core.Resources.Engine.Types;
using Silk.NET.Maths;

namespace Nayaemir.Core.Resources.Components.Registries;

internal class CameraRegistry : ResourceRegistry<Camera>
{
    private Vector2D<int> _framebufferSize;

    protected override void OnRegister(Camera resource)
    {
        resource.SetFramebufferSize(_framebufferSize);
    }

    /// <summary>
    /// Set the initial framebuffer size upon creation of the <see cref="WindowObject"/>
    /// </summary>
    /// <param name="size"></param>
    public void SetInitialFramebufferSize(Vector2D<int> size)
    {
        _framebufferSize = size;
    }

    /// <summary>
    /// Update all <see cref="Camera"/> instances to use the new framebuffer size
    /// </summary>
    /// <param name="size"></param>
    public void OnFramebufferResize(Vector2D<int> size)
    {
        _framebufferSize = size;

        foreach (var camera in _resources)
        {
            camera.OnFramebufferResize(size);
        }
    }
}