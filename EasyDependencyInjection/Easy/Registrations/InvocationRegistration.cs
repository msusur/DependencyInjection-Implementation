using System;

namespace EasyDependencyInjection.Easy.Registrations
{
    internal class InvocationRegistration : EasyRegistration
    {
        public InvocationRegistration(Type fromType, Type toType, RegistrationTypes registrationType)
            : base(fromType, toType)
        {
            RegistrationType = registrationType;
        }
    }
}