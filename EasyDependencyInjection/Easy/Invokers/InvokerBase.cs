using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy.Invokers
{
    internal abstract class InvokerBase
    {
        protected IEasyDependencyContainer Container { get; private set; }

        public EasyRegistration Registration { get; private set; }

        // ReSharper disable once PublicConstructorInAbstractClass
        public InvokerBase(EasyRegistration registration, IEasyDependencyContainer container)
        {
            Container = container;
            Registration = registration;
        }

        public abstract object Invoke();
    }

    internal abstract class InvokerBase<TRegistrationType> : InvokerBase
         where TRegistrationType : EasyRegistration
    {

        protected TRegistrationType ConvertedRegistration
        {
            get { return Registration as TRegistrationType; }
        }

        protected InvokerBase(TRegistrationType registration, IEasyDependencyContainer container)
            : base(registration, container)
        {
        }
    }
}