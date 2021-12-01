using System.Numerics;
using System.Runtime.CompilerServices;
using Nayaemir.Core.Resources.Components.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics.Types;

public class ShaderObject : GraphicsResource
{
    private static uint _currentShader;

    private readonly string _vertexPath;
    private readonly string _fragmentPath;

    private readonly uint _id;

    private Camera _attachedCamera;

    public ShaderObject(string vertexPath, string fragmentPath)
    {
        _vertexPath = vertexPath;
        _fragmentPath = fragmentPath;

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

    public void Bind()
    {
        if (_id != _currentShader)
        {
            Api.UseProgram(_id);
            _currentShader = _id;
        }
    }

    /// <summary>
    /// Attach a <see cref="Camera"/> which will provide information about the Projection and view matrices
    /// </summary>
    /// <param name="camera"></param>
    public void AttachCamera(Camera camera)
    {
        if (_attachedCamera != null)
        {
            _attachedCamera.ProjectionMatrixChanged -= OnCameraProjectionChanged;
            _attachedCamera.ViewMatrixChanged -= OnCameraViewChanged;
        }

        OnCameraProjectionChanged(camera.ProjectionMatrix);
        OnCameraViewChanged(camera.ViewMatrix);

        camera.ProjectionMatrixChanged += OnCameraProjectionChanged;
        camera.ViewMatrixChanged += OnCameraViewChanged;

        _attachedCamera = camera;
    }

    internal unsafe void SetMatrix4(string location, Matrix4x4 matrix)
    {
        Bind();
        Api.UniformMatrix4(GetUniformLocation(location), 1, false, (float*)Unsafe.AsPointer(ref matrix));
    }

    private int GetUniformLocation(string location)
    {
        return Api.GetUniformLocation(_id, location);
    }

    private void OnCameraProjectionChanged(Matrix4x4 matrix)
    {
        SetMatrix4("uProjection", matrix);
    }

    private void OnCameraViewChanged(Matrix4x4 matrix)
    {
        SetMatrix4("uView", matrix);
    }
}