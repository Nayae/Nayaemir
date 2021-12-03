using Nayaemir.Core.AttributeLayouts;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

internal class VertexArrayObject<TAttributeLayout> : ApiResource where TAttributeLayout : IAttributeLayout, new()
{
    private readonly BufferObject _vertexBuffer;
    private readonly BufferObject _indexBuffer;

    private static uint _currentId;
    private readonly uint _id;

    public VertexArrayObject(BufferObject vertexBuffer, BufferObject indexBuffer)
    {
        _vertexBuffer = vertexBuffer;
        _indexBuffer = indexBuffer;

        _id = Api.GenVertexArray();
        Bind();

        vertexBuffer?.Bind();
        indexBuffer?.Bind();

        ConfigureAttributes();
    }

    public void Bind()
    {
        if (_currentId != _id)
        {
            Api.BindVertexArray(_id);
            _currentId = _id;
        }
    }

    private unsafe void ConfigureAttributes()
    {
        Bind();

        var layout = new TAttributeLayout();
        var attributeLocation = 0u;
        var offset = 0u;
        foreach (var attribute in IAttributeLayout.AttributeSizes.Keys)
        {
            attributeLocation++;

            if (!layout.Attributes.HasFlag(attribute))
            {
                continue;
            }

            Api.VertexAttribPointer(
                attributeLocation - 1,
                (int)IAttributeLayout.AttributeSizes[attribute],
                VertexAttribPointerType.Float,
                false,
                (uint)(layout.ValueCount * _vertexBuffer.ElementSize),
                (void*)(offset * _vertexBuffer.ElementSize)
            );

            offset += IAttributeLayout.AttributeSizes[attribute];

            Api.EnableVertexAttribArray(attributeLocation - 1);
        }
    }
}