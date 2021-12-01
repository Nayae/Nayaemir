using System.Drawing;
using System.Numerics;
using Nayaemir.Core.Resources.Graphics.Types;
using Silk.NET.OpenGL;

namespace Nayaemir.Core.Resources.Components.Types;

[Flags]
public enum VertexAttributes
{
    Vertices = 3,
    Colors = 4
}

public class Mesh : Resource
{
    private delegate float FieldSelectorDelegate<in T>(T input);

    private readonly BufferObject _vertexBuffer;
    private readonly BufferObject _indexBuffer;
    private readonly VertexArrayObject _vertexArray;

    private readonly Dictionary<VertexAttributes, uint> _vertexOffsets;

    private readonly VertexAttributes _attributes;
    private readonly uint _vertexCount;

    public Mesh(VertexAttributes attributes, uint vertexCount)
    {
        _attributes = attributes;
        _vertexCount = vertexCount;

        _vertexOffsets = new Dictionary<VertexAttributes, uint>();

        _vertexBuffer = new BufferObject(BufferTargetARB.ArrayBuffer, BufferUsageARB.StaticDraw);
        _indexBuffer = new BufferObject(BufferTargetARB.ElementArrayBuffer, BufferUsageARB.StaticDraw);
        _vertexArray = new VertexArrayObject(_vertexBuffer, _indexBuffer);

        Clear();
    }

    public void Render()
    {
        _vertexArray.Render(PrimitiveType.Triangles);
    }

    public void SetVertices(Vector3[] vertices, uint offset = 0u)
    {
        UpdateData(vertices, VertexAttributes.Vertices, offset, new FieldSelectorDelegate<Vector3>[]
        {
            vertex => vertex.X,
            vertex => vertex.Y,
            vertex => vertex.Z
        });
    }

    public void SetColors(Color[] colors, uint offset = 0u)
    {
        UpdateData(colors, VertexAttributes.Colors, offset, new FieldSelectorDelegate<Color>[]
        {
            // Normalize the values to range from 0.0f to 1.0f
            color => color.R / 255.0f,
            color => color.G / 255.0f,
            color => color.B / 255.0f,
            color => color.A / 255.0f
        });
    }

    public void SetIndices(uint[] indices)
    {
        _indexBuffer.Resize<uint>((uint)indices.Length);
        _indexBuffer.SetData(indices);

        _vertexArray.UseIndices = true;
    }

    private void UpdateData<T>(
        T[] data, VertexAttributes attribute, uint offset, FieldSelectorDelegate<T>[] propertySelectors
    )
    {
        if (!_attributes.HasFlag(attribute))
        {
            throw new Exception();
        }

        if (data.Length + offset > _vertexCount)
        {
            throw new Exception();
        }

        var values = new float[data.Length * propertySelectors.Length];
        var valueIndex = 0;
        foreach (var entry in data)
        {
            for (var i = 0; i < propertySelectors.Length; i++)
            {
                values[valueIndex + i] = propertySelectors[i](entry);
            }

            valueIndex += propertySelectors.Length;
        }

        _vertexBuffer.SetData(values, _vertexOffsets[attribute] + offset);
    }

    private void Clear()
    {
        _vertexOffsets.Clear();

        var totalSize = Enum.GetValues<VertexAttributes>()
            .Where(a => _attributes.HasFlag(a))
            .Aggregate(0u, (current, attribute) => current + (uint)attribute * _vertexCount);

        _vertexBuffer.Resize<float>(totalSize);

        var offset = 0u;
        foreach (var attribute in Enum.GetValues<VertexAttributes>())
        {
            if (!_attributes.HasFlag(attribute))
            {
                continue;
            }

            _vertexArray.ConfigureAttribute((int)attribute, offset);
            _vertexOffsets[attribute] = offset;

            offset += (uint)attribute * _vertexCount;
        }
    }
}