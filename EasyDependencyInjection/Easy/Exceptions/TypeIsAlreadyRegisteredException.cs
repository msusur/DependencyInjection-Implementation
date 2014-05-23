using System;

namespace EasyDependencyInjection.Easy.Exceptions
{
    public class TypeIsAlreadyRegisteredException : Exception
    {
        public TypeIsAlreadyRegisteredException(Type interfaceType, Type classType)
            : base(String.Format("The type '{0}' is already registered to '{1}'. Cannot be registered again.", interfaceType.FullName, classType.FullName))
        {
        }
    }
}