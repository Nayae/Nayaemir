using Nayaemir.Core.Rendering;
using Nayaemir.Core.Resources.Graphics;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Nayaemir.Core;

public abstract class Engine : IDisposable
{
    internal static GL Api { get; private set; }

    protected GraphicsResourceFactory GraphicsResourceFactory { get; private set; }

    private IWindow _window;

    private EngineDebugger _debugger;
    private Renderer3D _renderer3D;

    protected abstract void Initialize();
    protected abstract void Render(Renderer3D renderer);

    public void Run()
    {
        _window = Window.Create(WindowOptions.Default);

        _window.Load += OnLoad;
        _window.Render += OnRender;

        _window.Run();
    }

    public void OnLoad()
    {
        Api = GL.GetApi(_window);

        _debugger = new EngineDebugger();
        _renderer3D = new Renderer3D();

        Initialize();
    }

    public void OnRender(double delta)
    {
        _renderer3D.DeltaTime = delta;

        Render(_renderer3D);
    }

    public void Dispose()
    {
    }
}