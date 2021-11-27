using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Types;

public class BufferObject : GraphicsResource
{
    internal uint ElementSize { get; private set; }
    internal uint ElementCount { get; private set; }

    private readonly BufferTargetARB _target;

    private readonly BufferUsageARB _usage;

    private uint _id;
    private bool _isInitialized;

#if DEBUG
    private Type _bufferType;
#endif

    internal BufferObject(BufferTargetARB target, BufferUsageARB usage)
    {
        _target = target;
        _usage = usage;
    }

    protected override void _Initialize()
    {
        _id = Api.GenBuffer();
    }

    internal unsafe void SetData<T>(T[] data, nint offset = 0) where T : unmanaged
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

            Api.BufferSubData(_target, offset * sizeof(T), new ReadOnlySpan<T>(data));
        }
        else
        {
            Api.BufferData(_target, new ReadOnlySpan<T>(data), _usage);

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
        Api.BindBuffer(_target, _id);
    }
}