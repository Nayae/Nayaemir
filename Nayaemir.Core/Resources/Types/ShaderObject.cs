using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Types;

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
        var vertexShader = Api.CreateShader(ShaderType.VertexShader);
        Api.ShaderSource(vertexShader, File.ReadAllText(_vertexPath));
        Api.CompileShader(vertexShader);

        var fragmentShader = Api.CreateShader(ShaderType.FragmentShader);
        Api.ShaderSource(fragmentShader, File.ReadAllText(_fragmentPath));
        Api.CompileShader(fragmentShader);

        _id = Api.CreateProgram();
        Api.AttachShader(_id, vertexShader);
        Api.AttachShader(_id, fragmentShader);
        Api.LinkProgram(_id);

        Api.DeleteShader(vertexShader);
        Api.DeleteShader(fragmentShader);
    }

    internal void Use()
    {
        Api.UseProgram(_id);
    }
}