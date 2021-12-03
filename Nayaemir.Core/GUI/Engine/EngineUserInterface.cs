using Nayaemir.Core.Rendering._2D;
using Nayaemir.Core.Resources.Components.Types;
using Nayaemir.Core.Resources.Graphics.Types;

namespace Nayaemir.Core.GUI.Engine;

public class EngineUserInterface
{
    private readonly Renderer2D _renderer;
    private Rectangle2D _rectangle;
    private Texture2D _texture;

    public EngineUserInterface(Renderer2D renderer)
    {
        _renderer = renderer;

        _texture = new Texture2D(new TextureObject("Resources/container.jpg"))
        {
            X = 0,
            Y = 0,
            Width = 100,
            Height = 100
        };

        _rectangle = new Rectangle2D
        {
            Width = 100,
            Height = 100
        };

        _renderer.AddRenderObject(_texture);
    }

    public void Render(float delta)
    {
    }
}