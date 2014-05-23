using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy.Invokers
{
    internal class FactoryInvoker : InvokerBase<FactoryRegistration>
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