using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

public class ShaderObject : GraphicsResource
{
    private readonly string _vertexPath;
    private readonly string _fragmentPath;

    private uint _id;

    internal ShaderObject(string vertexPath, string fragmentPath)
    {
        _vertexPath = vertexPath;
        _fragmentPath = fragmentPath;
    }

    protected override void _Initialize()
    {
        var vertexShader = _api.CreateShader(ShaderType.VertexShader);
        _api.ShaderSource(vertexShader, File.ReadAllText(_vertexPath));
        _api.CompileShader(vertexShader);

        var fragmentShader = _api.CreateShader(ShaderType.FragmentShader);
        _api.ShaderSource(fragmentShader, File.ReadAllText(_fragmentPath));
        _api.CompileShader(fragmentShader);

        _id = _api.CreateProgram();
        _api.AttachShader(_id, vertexShader);
        _api.AttachShader(_id, fragmentShader);
        _api.LinkProgram(_id);

        _api.DeleteShader(vertexShader);
        _api.DeleteShader(fragmentShader);
    }

    internal void Use()
    {
        _api.UseProgram(_id);
    }
}