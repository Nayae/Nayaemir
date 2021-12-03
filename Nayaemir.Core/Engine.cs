using Nayaemir.Core.GUI.Engine;
using Nayaemir.Core.Resources;
using Nayaemir.Core.Resources.Components.Types;
using Nayaemir.Core.Resources.Engine.Types;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core;

public abstract class Engine : IDisposable
{
    protected GL Api => ApiResource.Api;

    protected abstract void Initialize();
    protected abstract void Render(float delta);

    private readonly WindowObject _window;

    private EngineUserInterface _engineUserInterface;
    private ApiDebuggerObject _debugger;

    private Renderer2D _renderer;

    protected Engine()
    {
        _window = new WindowObject();

        _window.Load += OnLoad;
        _window.Render += OnRender;
    }

    public void Run()
    {
        _window.Run();
    }

    private void OnLoad()
    {
        _debugger = new ApiDebuggerObject();

        _renderer = new Renderer2D();
        _engineUserInterface = new EngineUserInterface(_renderer);

        Initialize();
    }

    private void OnRender(float delta)
    {
        Render(delta);

        _renderer.BeginFrame();
        _engineUserInterface.Render(delta);
        _renderer.EndFrame();
    }

    public void Dispose()
    {
    }
}