using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Graphics;

public abstract class GraphicsResource : Resource
{
    internal static GL Api { get; set; }
}