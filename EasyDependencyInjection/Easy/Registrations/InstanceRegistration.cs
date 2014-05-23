using System;

namespace EasyDependencyInjection.Easy.Registrations
{
    public class InstanceRegistration : EasyRegistration
    {
        public object Instance { get; private set; }

        public InstanceRegistration(Type fromType, object instance)
            : base(fromType, instance.GetType())
        {
            Instance = instance;
        }
    }
}