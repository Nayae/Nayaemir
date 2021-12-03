using Nayaemir.Core.AttributeLayouts;

namespace Nayaemir.Core.Rendering;

public interface IRenderObject<T> where T : IAttributeLayout
{
    public bool HasDataChanged { get; set; }

    T[] GetRenderObjectData();
}