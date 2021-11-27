using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

public enum VertexAttributeType
{
    vec1 = 1,
    vec2 = 2,
    vec3 = 3,
    vec4 = 4
}

public class VertexArrayObject : GraphicsResource
{
    private readonly BufferObject _vbo;
    private readonly BufferObject _ebo;
    private readonly Dictionary<uint, VertexAttributeType> _attributes;

    private readonly uint _count;
    private readonly bool _useIndices;
    private uint _id;

    internal VertexArrayObject(
        BufferObject vbo, BufferObject ebo, Dictionary<uint, VertexAttributeType> attributes
    ) : this(vbo, attributes)
    {
        _ebo = ebo;
        _useIndices = true;
        _count = ebo.ElementCount;
    }

    internal VertexArrayObject(BufferObject vbo, Dictionary<uint, VertexAttributeType> attributes)
    {
        _vbo = vbo;
        _useIndices = false;
        _count = vbo.ElementCount;

        _attributes = attributes;
    }

    protected override unsafe void _Initialize()
    {
        _id = _api.GenVertexArray();
        Bind();

        _vbo.Bind();

        if (_useIndices)
        {
            _ebo.Bind();
        }

        var stride = (uint)(_attributes.Values.Sum(v => (int)v) * _vbo.ElementSize);
        var offset = 0u;

        foreach (var (location, type) in _attributes)
        {
            _api.VertexAttribPointer(
                location, (int)type, VertexAttribPointerType.Float, false, stride, (void*)(offset * _vbo.ElementSize)
            );
            _api.EnableVertexAttribArray(location);

            offset += (uint)type;
        }
    }

    public unsafe void Render(PrimitiveType mode)
    {
        Bind();

        if (_useIndices)
        {
            _api.DrawElements(mode, _count, DrawElementsType.UnsignedInt, null);
        }
        else
        {
            _api.DrawArrays(mode, 0, _count);
        }
    }

    private void Bind()
    {
        _api.BindVertexArray(_id);
    }
}