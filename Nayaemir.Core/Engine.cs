using Nayaemir.Core.Resources.Engine.Types;
using Nayaemir.Core.Resources.Graphics;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core;

public abstract class Engine : IDisposable
{
    protected GL Api => GraphicsResource.Api;

    protected abstract void Initialize();
    protected abstract void Render(float delta);

    private readonly WindowObject _window;
    private GraphicsDebuggerObject _debugger;

    protected Engine()
    {
        _window = new WindowObject();

        _window.Load += OnLoad;
        _window.Render += Render;
    }

    public void Run()
    {
        _window.Run();
    }

    private void OnLoad()
    {
        _debugger = new GraphicsDebuggerObject();

        Initialize();
    }

    public void Dispose()
    {
    }
}