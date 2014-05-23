using System;

namespace EasyDependencyInjection.Easy.Registrations
{
    internal class FactoryRegistration : EasyRegistration
    {
        public Func<object> InvokeFunction { get; private set; }

        public FactoryRegistration(Type fromType, Type toType, Func<object> invokeFunction)
            : base(fromType, toType)
        {
            InvokeFunction = invokeFunction;
        }
    }
}