using System;

namespace EasyDependencyInjection.Easy.Exceptions
{
    public class InvokerNotFoundException : Exception
    {
        public InvokerNotFoundException(string registrationFullName)
            : base(string.Format("Invoker for type '{0}' not found. Please check InvokerFactory definition.", registrationFullName))
        {
        }
    }
}