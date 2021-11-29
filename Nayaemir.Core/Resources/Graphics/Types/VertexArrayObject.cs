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
    protected override GraphicsResourceType ResourceType => GraphicsResourceType.VertexArrayObject;

    private readonly BufferObject _vbo;
    private readonly BufferObject _ebo;

    private readonly uint _id;
    private readonly uint _count;

    public VertexArrayObject(BufferObject vbo, Dictionary<uint, VertexAttributeType> attributes)
        : this(vbo, null, attributes)
    {
    }

    public unsafe VertexArrayObject(
        BufferObject vbo, BufferObject ebo, Dictionary<uint, VertexAttributeType> attributes
    )
    {
        _vbo = vbo;
        _ebo = ebo;
        _count = vbo.ElementCount;

        _id = _api.GenVertexArray();
        Bind();

        _vbo.Bind();

        if (ebo != null)
        {
            _ebo.Bind();
        }

        var stride = (uint)(attributes.Values.Sum(v => (int)v) * _vbo.ElementSize);
        var offset = 0u;

        foreach (var (location, type) in attributes)
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

        if (_ebo != null)
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