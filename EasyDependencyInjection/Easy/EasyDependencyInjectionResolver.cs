using System;
using EasyDependencyInjection.Abstraction;

namespace EasyDependencyInjection.Easy
{
    public class EasyDependencyInjectionResolver : IDependencyResolver
    {
        private readonly IEasyDependencyContainer _container;

        public EasyDependencyInjectionResolver(IEasyDependencyContainer container)
        {
            _container = container;
        }

        public EasyDependencyInjectionResolver()
            : this(new EasyDependencyContainer())
        {
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public TServiceType Resolve<TServiceType>()
        {
            return _container.ResolveType<TServiceType>();
        }

        public object Resolve(Type serviceType)
        {
            return _container.ResolveType(serviceType);
        }

        public Registration Register<TInterface, TClass>(Registration.RegistrationTypes registrationType = Registration.RegistrationTypes.Transient) where TInterface : class where TClass : TInterface
        {
            return Register(typeof (TInterface), typeof (TClass), registrationType);
        }

        public Registration Register<TInterface>(TInterface instanceOfType) where TInterface : class
        {
            return _container.RegisterInstance(instanceOfType);
        }

        public Registration Register(Type interfaceType, Type classType, Registration.RegistrationTypes registrationType = Registration.RegistrationTypes.Transient)
        {
            return _container.RegisterType(interfaceType, classType, registrationType);
        }

        public IDependencyResolver CreateChildContainer()
        {
            return new EasyDependencyInjectionResolver(_container.CreateChildContainer(_container));
        }

        public void RegisterUsingFactory<TServiceType>(string name, Func<IDependencyResolver, TServiceType> factoryMethod) 
            where TServiceType : class
        {
            _container.RegisterUsingFactory(name, factoryMethod);
        }
    }
}
