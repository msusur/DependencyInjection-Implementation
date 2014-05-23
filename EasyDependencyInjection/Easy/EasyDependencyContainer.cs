using System;
using System.Collections.Generic;
using EasyDependencyInjection.Abstraction;
using EasyDependencyInjection.Easy.Exceptions;
using EasyDependencyInjection.Easy.Invokers;
using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy
{
    public class EasyDependencyContainer : IEasyDependencyContainer
    {
        private readonly Dictionary<Type, EasyRegistration> _registrations;

        private static readonly object SyncRoot = new object();

        private EasyDependencyContainer(Dictionary<Type, EasyRegistration> registrations)
        {
            _registrations = new Dictionary<Type, EasyRegistration>(registrations);
        }

        public EasyDependencyContainer()
            : this(new Dictionary<Type, EasyRegistration>())
        {
        }

        public TServiceType ResolveType<TServiceType>()
        {
            return (TServiceType)ResolveType(typeof(TServiceType));
        }

        public object ResolveType(Type serviceType)
        {
            var hasType = _registrations.ContainsKey(serviceType);

            if (!hasType)
            {
                throw new CannotResolveDependency(serviceType);
            }

            return ResolveDependencies(serviceType);
        }

        public Registration RegisterInstance<TServiceType>(TServiceType instanceOfType)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (instanceOfType == null)
            {
                throw new ArgumentNullException("instanceOfType");
            }

            Type serviceType = typeof(TServiceType);
            CheckIfAlreadyRegistered(serviceType, instanceOfType.GetType());

            lock (SyncRoot)
            {
                var registration = new InstanceRegistration(serviceType, instanceOfType);

                _registrations.Add(serviceType, registration);

                return registration;
            }
        }

        public Registration RegisterType(Type interfaceType, Type classType, Registration.RegistrationTypes registrationType)
        {
            CheckIfAlreadyRegistered(interfaceType, classType);

            lock (SyncRoot)
            {
                var registration = new InvocationRegistration(interfaceType, classType, registrationType);

                _registrations.Add(interfaceType, registration);

                return registration;
            }
        }

        public Registration RegisterUsingFactory<T>(string name, Func<IDependencyResolver, T> factoryMethod)
        {
            Type interfaceType = typeof(T);

            var registration = new FactoryRegistration(interfaceType, interfaceType, () => factoryMethod(null));

            lock (SyncRoot)
            {
                var hasDependency = _registrations.ContainsKey(interfaceType);
                if (hasDependency)
                {
                    _registrations[interfaceType] = registration;
                }
                else
                {
                    _registrations.Add(interfaceType, registration);
                }
            }

            return registration;
        }

        public IEasyDependencyContainer CreateChildContainer(IEasyDependencyContainer container)
        {
            return new EasyDependencyContainer(_registrations);
        }

        public void Dispose()
        {

        }

        private void CheckIfAlreadyRegistered(Type interfaceType, Type toType)
        {
            var hasRegistered = _registrations.ContainsKey(interfaceType);

            if (hasRegistered)
            {
                throw new TypeIsAlreadyRegisteredException(interfaceType, toType);
            }
        }

        private object ResolveDependencies(Type mainType)
        {
            var registration = _registrations[mainType];

            var invoker = InvokerFactory.GetInvoker(registration, this);
            try
            {
                return invoker.Invoke();
            }
            catch (Exception ex)
            {
                throw new CannotResolveDependency(mainType, ex);
            }
        }
    }
}