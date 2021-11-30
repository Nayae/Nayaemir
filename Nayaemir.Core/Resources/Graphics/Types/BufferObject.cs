using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

internal class BufferObject : GraphicsResource
{
    internal int ElementSize { get; private set; }
    internal uint Size { get; private set; }

    private readonly BufferTargetARB _target;
    private readonly BufferUsageARB _usage;

    private readonly uint _id;

    public BufferObject(BufferTargetARB target, BufferUsageARB usage)
    {
        _target = target;
        _usage = usage;

        _id = _api.GenBuffer();
    }

    public unsafe void Clear<T>(uint size) where T : unmanaged
    {
        Bind();
        _api.BufferData(_target, (nuint)(size * sizeof(T)), null, _usage);
       
        Size = size;
        ElementSize = sizeof(T);
    }

    public unsafe void SetData<T>(T[] data, uint offset = 0) where T : unmanaged
    {
        Bind();
        _api.BufferSubData(_target, (nint)(offset * sizeof(T)), new ReadOnlySpan<T>(data));
    }

    public void Bind()
    {
        _api.BindBuffer(_target, _id);
    }
}