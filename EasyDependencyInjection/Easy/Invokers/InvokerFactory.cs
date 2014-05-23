using System;
using System.Collections.Generic;
using EasyDependencyInjection.Easy.Exceptions;
using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy.Invokers
{
    internal class InvokerFactory
    {
        private static readonly Dictionary<Type, Type> Invokers = new Dictionary<Type, Type>
        {
            { typeof(FactoryRegistration), typeof(FactoryInvoker) },
            { typeof(InvocationRegistration), typeof(InvocationInvoker)},
            { typeof(InstanceRegistration), typeof(InstanceInvoker)}
        };

        public static InvokerBase GetInvoker(EasyRegistration registration, IEasyDependencyContainer container)
        {
            var registrationType = registration.GetType();
            var hasInvoker = Invokers.ContainsKey(registrationType);
            if (hasInvoker)
            {
                return Activator.CreateInstance(Invokers[registrationType], registration, container) as InvokerBase;
            }
            throw new InvokerNotFoundException(registrationType.FullName);
        }
    }
}