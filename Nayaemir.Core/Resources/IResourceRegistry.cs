namespace Nayaemir.Core.Resources;

public interface IResourceRegistry
{
    void Register(Resource resource);
    void Release(Resource resource);
}