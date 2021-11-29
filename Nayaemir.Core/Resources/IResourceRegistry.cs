namespace Nayaemir.Core.Resources;

public interface IResourceRegistry
{
    void Register(IResource resource);
    void Release(IResource resource);
}