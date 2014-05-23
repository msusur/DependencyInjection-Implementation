using System;
using EasyDependencyInjection.Abstraction;

namespace EasyDependencyInjection.Easy
{
    public interface IEasyDependencyContainer
    {
        TServiceType ResolveType<TServiceType>();
        
        object ResolveType(Type serviceType);
        
        Registration RegisterInstance<TServiceType>(TServiceType instanceOfType);
        
        Registration RegisterType(Type interfaceType, Type classType, Registration.RegistrationTypes registrationType);
        
        Registration RegisterUsingFactory<T>(string name, Func<IDependencyResolver, T> factoryMethod);

        IEasyDependencyContainer CreateChildContainer(IEasyDependencyContainer container);
        
        void Dispose();
    }
}