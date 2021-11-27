using Nayaemir.Core.Rendering;
using Nayaemir.Core.Resources;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Nayaemir.Core;

public abstract class Engine : IDisposable
{
    protected ResourceFactory ResourceFactory { get; private set; }

    private IWindow _window;
    private GL _api;

    private ResourceManager _resourceManager;
    private Debugger _debugger;

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
        _api = GL.GetApi(_window);

        _resourceManager = new ResourceManager(_api);
        _debugger = new Debugger(_api);

        ResourceFactory = new ResourceFactory(_resourceManager);

        _renderer3D = new Renderer3D(_api);

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