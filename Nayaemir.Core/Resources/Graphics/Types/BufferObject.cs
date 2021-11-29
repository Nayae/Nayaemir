using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

public class BufferObject : GraphicsResource
{
    protected override GraphicsResourceEnum ResourceType => GraphicsResourceEnum.BufferObject;

    internal uint ElementSize { get; private set; }
    internal uint ElementCount { get; private set; }

    private readonly BufferTargetARB _target;

    private readonly BufferUsageARB _usage;

    private readonly uint _id;
    private bool _isInitialized;

#if DEBUG
    private Type _bufferType;
#endif

    public BufferObject(BufferTargetARB target, BufferUsageARB usage)
    {
        _target = target;
        _usage = usage;

        _id = _api.GenBuffer();
    }

    public unsafe void SetData<T>(T[] data, nint offset = 0) where T : unmanaged
    {
        Bind();

        if (_isInitialized)
        {
#if DEBUG
            if (typeof(T) != _bufferType)
            {
                throw new Exception();
            }
#endif

            _api.BufferSubData(_target, offset * sizeof(T), new ReadOnlySpan<T>(data));
        }
        else
        {
            _api.BufferData(_target, new ReadOnlySpan<T>(data), _usage);

            ElementSize = (uint)sizeof(T);
            ElementCount = (uint)data.Length;
            _isInitialized = true;

#if DEBUG
            _bufferType = typeof(T);
#endif
        }
    }

    internal void Bind()
    {
        _api.BindBuffer(_target, _id);
    }
}