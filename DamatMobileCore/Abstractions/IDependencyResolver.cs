namespace DamatMobile.Core.Abstractions
{
    public interface IDependencyResolver
    {
        T Resolve<T>(params (string paramentrName, object value)[] parameters);
    }
}