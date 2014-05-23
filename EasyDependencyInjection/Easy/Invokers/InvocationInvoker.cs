using System.Collections;
using System.Linq;
using EasyDependencyInjection.Easy.Exceptions;
using EasyDependencyInjection.Easy.Registrations;

namespace EasyDependencyInjection.Easy.Invokers
{
    public class InvocationInvoker : InvokerBase<InvocationRegistration>
    {
        public InvocationInvoker(InvocationRegistration registration, IEasyDependencyContainer container)
            : base(registration, container)
        {
        }

        public override object Invoke()
        {
            var targetType = Registration.ToType;

            var ctors = targetType.GetConstructors();

            var ctor = ctors.OrderByDescending(info => info.GetParameters()).FirstOrDefault();
            if (ctor == null)
            {
                throw new CannotResolveDependency(targetType);
            }

            var parameters = new ArrayList();
            foreach (var parameterInfo in ctor.GetParameters())
            {
                var parameter = Container.ResolveType(parameterInfo.ParameterType);
                parameters.Add(parameter);
            }
            return ctor.Invoke(parameters.ToArray());
        }
    }
}