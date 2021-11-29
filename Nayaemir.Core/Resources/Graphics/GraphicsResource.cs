using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics;

public abstract class GraphicsResource : Resource
{
    protected GL _api => Engine.Api;
}