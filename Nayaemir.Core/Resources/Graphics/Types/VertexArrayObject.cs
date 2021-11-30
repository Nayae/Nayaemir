using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

internal class VertexArrayObject : GraphicsResource
{
    internal bool UseIndices { get; set; }

    private readonly BufferObject _vertexBuffer;
    private readonly BufferObject _indexBuffer;

    private readonly uint _id;
    private uint _attributeLocation;

    public VertexArrayObject(BufferObject vertexBuffer, BufferObject indexBuffer)
    {
        _vertexBuffer = vertexBuffer;
        _indexBuffer = indexBuffer;

        _id = _api.GenVertexArray();
        Bind();

        vertexBuffer.Bind();
        indexBuffer.Bind();
    }

    public unsafe void ConfigureAttribute(int size, uint offset)
    {
        Bind();
        _api.VertexAttribPointer(
            _attributeLocation,
            size,
            VertexAttribPointerType.Float,
            false,
            0,
            (void*)(offset * _vertexBuffer.ElementSize)
        );
        _api.EnableVertexAttribArray(_attributeLocation);

        _attributeLocation++;
    }

    public unsafe void Render(PrimitiveType mode)
    {
        Bind();

        if (UseIndices)
        {
            _api.DrawElements(mode, _indexBuffer.Size, DrawElementsType.UnsignedInt, null);
        }
        else
        {
            _api.DrawArrays(mode, 0, _indexBuffer.Size);
        }
    }

    private void Bind()
    {
        _api.BindVertexArray(_id);
    }
}