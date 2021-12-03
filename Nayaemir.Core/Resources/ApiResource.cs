using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources;

public abstract class ApiResource : Resource
{
    internal static GL Api { get; set; }
}