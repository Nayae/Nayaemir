namespace Nayaemir.Core.Resources;

internal interface IResourceRegistry
{
    Type ResourceType { get; }
    void Register(IResource resource);
    void Release(IResource resource);
}