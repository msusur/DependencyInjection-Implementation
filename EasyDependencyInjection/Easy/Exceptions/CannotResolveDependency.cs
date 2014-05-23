using System;

namespace EasyDependencyInjection.Easy.Exceptions
{
    public class CannotResolveDependency : Exception
    {
        public CannotResolveDependency(Type requiredType)
            : this(requiredType, null)
        {
        }

        public CannotResolveDependency(Type requiredType, Exception exception)
            : base(String.Format("Type '{0}' cannot be resolved. Because it is not registered.", requiredType.FullName), exception)
        {
        }
    }
}