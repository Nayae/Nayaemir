namespace Nayaemir.Core.Resources.Registries;

internal interface IResourceRegistry
{
    void Register(object resource);
    void Release(object resource);
}