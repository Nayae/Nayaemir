using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

internal class VertexArrayObject : GraphicsResource
{
    internal bool UseIndices { get; set; }

    private readonly BufferObject _vertexBuffer;
    private readonly BufferObject _indexBuffer;

    private static uint _currentId;
    private readonly uint _id;

    private uint _attributeLocation;

    public VertexArrayObject(BufferObject vertexBuffer, BufferObject indexBuffer)
    {
        _vertexBuffer = vertexBuffer;
        _indexBuffer = indexBuffer;

        _id = Api.GenVertexArray();
        Bind();

        vertexBuffer.Bind();
        indexBuffer.Bind();
    }

    public unsafe void ConfigureAttribute(int size, uint offset)
    {
        Bind();
        Api.VertexAttribPointer(
            _attributeLocation,
            size,
            VertexAttribPointerType.Float,
            false,
            0,
            (void*)(offset * _vertexBuffer.ElementSize)
        );
        Api.EnableVertexAttribArray(_attributeLocation);

        _attributeLocation++;
    }

    public unsafe void Render(PrimitiveType mode)
    {
        Bind();

        if (UseIndices)
        {
            Api.DrawElements(mode, _indexBuffer.Size, DrawElementsType.UnsignedInt, null);
        }
        else
        {
            Api.DrawArrays(mode, 0, _indexBuffer.Size);
        }
    }

    private void Bind()
    {
        if (_currentId != _id)
        {
            Api.BindVertexArray(_id);
            _currentId = _id;
        }
    }
}