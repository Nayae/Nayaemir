using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics;

public abstract class GraphicsResource : Resource<GraphicsResourceType>
{
    protected GL _api => Engine.Api;
}