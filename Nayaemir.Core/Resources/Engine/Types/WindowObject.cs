using Nayaemir.Core.Resources.Components.Registries;
using Nayaemir.Core.Resources.Graphics;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Nayaemir.Core.Resources.Engine.Types;

internal class WindowObject : Resource
{
    public Action Load { get; set; }
    public Action<float> Render { get; set; }

    private readonly IWindow _window;

    public WindowObject()
    {
        Window.PrioritizeSdl();

        _window = Window.Create(WindowOptions.Default);

        _window.Load += OnLoad;
        _window.Render += OnRender;
        _window.FramebufferResize += OnFramebufferResize;
    }

    public void Run()
    {
        _window.Run();
    }

    private void OnLoad()
    {
        GraphicsResource.Api = GL.GetApi(_window);

        // Initialize the viewport size before any camera is created
        ResourceRegistry.Get<CameraRegistry>().SetInitialFramebufferSize(_window.Size);

        // Initialize the engine and implicitly any subsequent components
        Load?.Invoke();
    }

    private void OnRender(double delta)
    {
        Render?.Invoke((float)delta);
    }

    private void OnFramebufferResize(Vector2D<int> size)
    {
        GraphicsResource.Api.Viewport(size);

        ResourceRegistry.Get<CameraRegistry>().OnFramebufferResize(size);
    }
}