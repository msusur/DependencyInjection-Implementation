using System;

namespace EasyDependencyInjection.Easy.Registrations
{
    public class FactoryRegistration : EasyRegistration
    {
        public Func<object> InvokeFunction { get; private set; }

        public FactoryRegistration(Type fromType, Type toType, Func<object> invokeFunction)
            : base(fromType, toType)
        {
            InvokeFunction = invokeFunction;
        }
    }
}