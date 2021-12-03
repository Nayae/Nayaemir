using System.Numerics;
using System.Runtime.InteropServices;

namespace Nayaemir.Core.AttributeLayouts;

[StructLayout(LayoutKind.Sequential)]
public struct VertexColorTexCoordLayout : IAttributeLayout
{
    public VertexAttributes Attributes => VertexAttributes.Vertices |
                                          VertexAttributes.Colors |
                                          VertexAttributes.TexCoords;

    public uint ValueCount => 10;

    public Vector3 Vertex { get; set; }
    public Vector4 Color { get; set; } = System.Drawing.Color.Red.ToNormalizedVector();
    public Vector3 TexCoord { get; set; }
}