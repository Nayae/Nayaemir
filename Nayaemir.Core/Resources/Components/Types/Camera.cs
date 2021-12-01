using System.Numerics;
using Nayaemir.Core.Resources.Components.Registries;
using Nayaemir.Core.Resources.Engine.Types;
using Silk.NET.Maths;

namespace Nayaemir.Core.Resources.Components.Types;

public class Camera : Resource
{
    private enum CameraMode
    {
        Perspective,
        Orthographic
    }

    internal Matrix4x4 ProjectionMatrix { get; private set; }
    internal Action<Matrix4x4> ProjectionMatrixChanged { get; set; }
    internal Matrix4x4 ViewMatrix { get; private set; }
    internal Action<Matrix4x4> ViewMatrixChanged { get; set; }

    private Vector2D<int> _framebufferSize;

    private CameraMode _cameraMode;
    private float _fieldOfView;
    private float _nearPlane;
    private float _farPlane;

    public void SetPerspectiveMode(float fieldOfView, float nearPlane, float farPlane)
    {
        _cameraMode = CameraMode.Perspective;
        _fieldOfView = fieldOfView;
        _nearPlane = nearPlane;
        _farPlane = farPlane;

        ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
            Scalar.DegreesToRadians(_fieldOfView),
            _framebufferSize.X / (float)_framebufferSize.Y,
            _nearPlane,
            _farPlane
        );

        ProjectionMatrixChanged?.Invoke(ProjectionMatrix);
    }

    public void SetOrthographicMode(float nearPlane, float farPlane)
    {
        _cameraMode = CameraMode.Orthographic;
        _nearPlane = nearPlane;
        _farPlane = farPlane;

        ProjectionMatrix = Matrix4x4.CreateOrthographic(
            _framebufferSize.X,
            _framebufferSize.Y,
            _nearPlane,
            _farPlane
        );

        ProjectionMatrixChanged?.Invoke(ProjectionMatrix);
    }

    public void SetView(float x, float y, float z)
    {
        ViewMatrix = Matrix4x4.CreateTranslation(x, y, z);
        ViewMatrixChanged?.Invoke(ViewMatrix);
    }

    /// <summary>
    /// Automatically invoked in the <see cref="CameraRegistry"/> upon creating a new camera
    /// </summary>
    /// <param name="size"></param>
    internal void SetFramebufferSize(Vector2D<int> size)
    {
        _framebufferSize = size;
    }

    /// <summary>
    /// Automatically invoked in the <see cref="CameraRegistry"/> upon a change in the <see cref="WindowObject"/> framebuffer
    /// </summary>
    /// <param name="size"></param>
    internal void OnFramebufferResize(Vector2D<int> size)
    {
        _framebufferSize = size;

        switch (_cameraMode)
        {
            case CameraMode.Perspective:
                SetPerspectiveMode(_fieldOfView, _nearPlane, _farPlane);
                break;
            case CameraMode.Orthographic:
                SetOrthographicMode(_nearPlane, _farPlane);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}