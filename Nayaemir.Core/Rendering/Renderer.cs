using System.Drawing;
using Nayaemir.Core.Resources.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Rendering;

public abstract class Renderer
{
    public double DeltaTime { get; internal set; }

    private readonly GL _api;

    private Color _currentColor;
    private ShaderObject _currentShader;

    protected Renderer(GL api)
    {
        _api = api;
    }

    public void Clear(Color color)
    {
        if (_currentColor != color)
        {
            _currentColor = color;
            _api.ClearColor(color);
        }

        _api.Clear(ClearBufferMask.ColorBufferBit);
    }

    public void UseShader(ShaderObject shader)
    {
        if (_currentShader != shader)
        {
            _currentShader = shader;
            shader.Use();
        }
    }
}