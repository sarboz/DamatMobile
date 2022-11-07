using System.Linq;
using Autofac;
using DamatMobile.Core.Abstractions;

namespace DamatMobile.Core.Services
{
    public class DependencyResolver : IDependencyResolver
    {
        public T Resolve<T>(params (string paramentrName, object value)[] parameters)
        {
            var namedParameters = parameters.Select(param => new NamedParameter(param.paramentrName, param.value));
            return DependencyInitializerCore.Container.BeginLifetimeScope().Resolve<T>(namedParameters);
        }
    }
}