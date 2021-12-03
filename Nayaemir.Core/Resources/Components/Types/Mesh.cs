using Nayaemir.Core.AttributeLayouts;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Components.Types;

public class Mesh<TAttributeLayout> : Resource where TAttributeLayout : struct, IAttributeLayout
{
    internal VertexArrayObject<TAttributeLayout> VertexArray { get; }

    private readonly BufferObject _vertexBuffer;
    private readonly BufferObject _indexBuffer;

    public Mesh(uint vertexCount)
    {
        _vertexBuffer = new BufferObject(BufferTargetARB.ArrayBuffer, BufferUsageARB.DynamicDraw);
        _vertexBuffer.Resize<float>(vertexCount);

        _indexBuffer = new BufferObject(BufferTargetARB.ElementArrayBuffer, BufferUsageARB.StaticDraw);

        VertexArray = new VertexArrayObject<TAttributeLayout>(_vertexBuffer, _indexBuffer);
    }

    public void Update(TAttributeLayout[] data, uint offset)
    {
        if (data.Length > 0)
        {
            _vertexBuffer.SetData<TAttributeLayout, float>(data, offset);
        }
    }
}