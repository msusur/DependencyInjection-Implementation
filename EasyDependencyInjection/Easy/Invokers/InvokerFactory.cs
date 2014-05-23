using System;
using System.Collections.Generic;
using EasyDependencyInjection.Easy.Exceptions;
using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy.Invokers
{
    public class InvokerFactory
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
                // Activator mechanism should be abstracted.
                return Activator.CreateInstance(Invokers[registrationType], registration, container) as InvokerBase;
            }
            throw new InvokerNotFoundException(registrationType.FullName);
        }
    }
}