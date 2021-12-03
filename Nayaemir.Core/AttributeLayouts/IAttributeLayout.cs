namespace Nayaemir.Core.AttributeLayouts;

[Flags]
public enum VertexAttributes
{
    Vertices,
    Colors,
    TexCoords
}

public interface IAttributeLayout
{
    public static readonly Dictionary<VertexAttributes, uint> AttributeSizes = new()
    {
        { VertexAttributes.Vertices, 3 },
        { VertexAttributes.Colors, 4 },
        { VertexAttributes.TexCoords, 3 }
    };

    VertexAttributes Attributes { get; }
    uint ValueCount { get; }
}