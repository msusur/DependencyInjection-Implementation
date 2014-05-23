using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy.Invokers
{
    public class FactoryInvoker : InvokerBase<FactoryRegistration>
    {
        public FactoryInvoker(FactoryRegistration registration, IEasyDependencyContainer container)
            : base(registration, container)
        {
        }

        public override object Invoke()
        {
            return ConvertedRegistration.InvokeFunction();
        }
    }
}