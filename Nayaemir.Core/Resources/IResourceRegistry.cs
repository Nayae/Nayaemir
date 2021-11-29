namespace Nayaemir.Core.Resources;

internal interface IResourceRegistry
{
    void Register(IResource resource);
    void Release(IResource resource);
}