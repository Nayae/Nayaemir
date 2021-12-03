using Nayaemir.Core.AttributeLayouts;
using Nayaemir.Core.Rendering;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Components.Types;

public class Renderer2D : ApiResource
{
    private const int MaxVertices = ushort.MaxValue;

    private readonly Camera _camera;
    private readonly Mesh<VertexColorTexCoordLayout> _mesh;

    private readonly ShaderObject _shader;

    private readonly Dictionary<IRenderObject<VertexColorTexCoordLayout>, uint> _renderObjects;
    private uint _nextOffset;

    public Renderer2D()
    {
        _camera = new Camera();
        _camera.SetOrthographicMode(0.01f, 100.0f);
        _camera.SetView(0.0f, 0.0f, -3.0f);

        _shader = new ShaderObject("Resources/Shaders/engine_ui.vert", "Resources/Shaders/engine_ui.frag");
        _shader.AttachCamera(_camera);

        _mesh = new Mesh<VertexColorTexCoordLayout>(MaxVertices);
        _renderObjects = new Dictionary<IRenderObject<VertexColorTexCoordLayout>, uint>();
    }

    public void AddRenderObject(IRenderObject<VertexColorTexCoordLayout> renderObject)
    {
        _renderObjects.Add(renderObject, _nextOffset);
    }

    public void RemoveRenderObject(IRenderObject<VertexColorTexCoordLayout> renderObject)
    {
        _renderObjects.Remove(renderObject);
    }

    public void BeginFrame()
    {
    }

    public void EndFrame()
    {
        foreach (var (renderObject, offset) in _renderObjects)
        {
            if (renderObject.HasDataChanged)
            {
                _mesh.Update(renderObject.GetRenderObjectData(), offset);
                renderObject.HasDataChanged = false;
            }
        }

        _shader.Bind();
        _mesh.VertexArray.Bind();

        Api.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
    }
}