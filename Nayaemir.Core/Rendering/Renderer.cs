using System.Drawing;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Rendering;

public abstract class Renderer
{
    protected readonly GL _api = Engine.Api;
    
    public double DeltaTime { get; internal set; }

    private Color _currentColor;
    private ShaderObject _currentShader;

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