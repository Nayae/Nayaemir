using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Nayaemir.Core;

public abstract class Engine : IDisposable
{
    public static GL Api { get; private set; }

    private IWindow _window;

    protected abstract void Initialize();
    protected abstract void Render();

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
        
        Initialize();
    }

    public void OnRender(double delta)
    {
        Render();
    }

    public void Dispose()
    {
    }
}