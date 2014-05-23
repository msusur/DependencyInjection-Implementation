using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy.Invokers
{
    public class InstanceInvoker : InvokerBase<InstanceRegistration>
    {
        public InstanceInvoker(InstanceRegistration registration, IEasyDependencyContainer container)
            : base(registration, container)
        {
        }

        public override object Invoke()
        {
            return ConvertedRegistration.Instance;
        }
    }
}