using Nayaemir.Core.AttributeLayouts;

namespace Nayaemir.Core.Rendering._2D;

public abstract class RenderObject2D : IRenderObject<VertexColorTexCoordLayout>
{
    public bool HasDataChanged { get; set; }

    public abstract VertexColorTexCoordLayout[] GetRenderObjectData();

    protected void OnPropertyChanged<T>(ref T property, T value)
    {
        HasDataChanged = true;
        property = value;
    }
}