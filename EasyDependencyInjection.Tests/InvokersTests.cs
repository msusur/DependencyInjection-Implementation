using System;
using EasyDependencyInjection.Abstraction;
using EasyDependencyInjection.Easy;
using EasyDependencyInjection.Easy.Exceptions;
using EasyDependencyInjection.Easy.Invokers;
using EasyDependencyInjection.Easy.Registrations;
using Moq;
using Xunit;

namespace EasyDependencyInjection.Tests
{
    public class InvokersTests
    {
        class UnknownRegistration : EasyRegistration
        {
            public UnknownRegistration(Type fromType, Type toType)
                : base(fromType, toType)
            {
            }
        }

        interface IFoo
        {
            Guid Id { get; }
        }

        class Foo : IFoo
        {
            public Guid Id { get; private set; }

            public Foo()
            {
                Id = Guid.NewGuid();
            }
        }

        [Fact]
        public void InvokerFactoryProductionPairCheck()
        {
            var mock = new Mock<Type>();
            var containerMock = new Mock<IEasyDependencyContainer>();

            // we only have three kinds of invoker and registration couple for now.

            // Get FactoryInvoker for FactoryRegistration
            var factoryRegistration = new FactoryRegistration(mock.Object, mock.Object, () => new object());
            var factoryInvoker = InvokerFactory.GetInvoker(factoryRegistration, containerMock.Object);
            Assert.IsAssignableFrom<FactoryInvoker>(factoryInvoker);

            // Get InvocationInvoker for InvocationRegistration 
            var invocationRegistration = new InvocationRegistration(mock.Object, mock.Object, Registration.RegistrationTypes.Transient);
            var invocationInvoker = InvokerFactory.GetInvoker(invocationRegistration, containerMock.Object);
            Assert.IsAssignableFrom<InvocationInvoker>(invocationInvoker);

            // Get InstanceInvoker for InstanceRegistration
            var instanceRegistration = new InstanceRegistration(mock.Object, new object());
            var instanceInvoker = InvokerFactory.GetInvoker(instanceRegistration, containerMock.Object);
            Assert.IsAssignableFrom<InstanceInvoker>(instanceInvoker);
        }

        [Fact]
        public void InvokerFactoryShouldThrowInvokerNotFoundExceptionIfAnUnknownRegisterWasAskedForInvoker()
        {
            var mock = new Mock<Type>();
            var registration = new UnknownRegistration(mock.Object, mock.Object);

            var containerMock = new Mock<IEasyDependencyContainer>();

            Assert.Throws<InvokerNotFoundException>(() => InvokerFactory.GetInvoker(registration, containerMock.Object));
        }

        [Fact]
        public void FactoryInvokerExecutesTheFactoryMethodOnRegistrationOnRegisterProcessTransient()
        {
            var container = new EasyDependencyContainer();
            var registration = container.RegisterUsingFactory<IFoo>("factoryName", c => new Foo());
            var foo = container.ResolveType<IFoo>();
            var foo2 = container.ResolveType<IFoo>();


            Assert.IsAssignableFrom<FactoryRegistration>(registration);
            var factoryRegistration = registration as FactoryRegistration;
            Assert.NotNull(factoryRegistration);

            Assert.Same(foo.GetType(), factoryRegistration.InvokeFunction().GetType());
            Assert.NotEqual(foo.Id, foo2.Id);
        }

        [Fact]
        public void FactoryInvokerExecutesTheFactoryMethodOnRegistrationOnRegisterProcessSingleton()
        {
            // singletons are not yet implemented.

            var container = new EasyDependencyContainer();
            var registration = container.RegisterUsingFactory<IFoo>("factoryName", c => new Foo());
            var foo = container.ResolveType<IFoo>();
            var foo2 = container.ResolveType<IFoo>();


            Assert.IsAssignableFrom<FactoryRegistration>(registration);
            var factoryRegistration = registration as FactoryRegistration;
            Assert.NotNull(factoryRegistration);

            Assert.Same(foo.GetType(), factoryRegistration.InvokeFunction().GetType());
            Assert.Equal(foo.Id, foo2.Id);
        }

        [Fact]
        public void InvocationInvokerResolvesTheRequiredTypeTransient()
        {
            var container = new EasyDependencyContainer();
            var registration = container.RegisterType(typeof(IFoo), typeof(Foo), Registration.RegistrationTypes.Transient);
            var foo1 = container.ResolveType<IFoo>();
            var foo2 = container.ResolveType<IFoo>();

            var invocationRegistration = registration as InvocationRegistration;
            Assert.NotNull(invocationRegistration);

            Assert.NotEqual(foo1.Id, foo2.Id);
        }

        [Fact]
        public void InvocationInvokerResolvesTheRequiredTypeSingleton()
        {
            // singletons are not yet implemented.

            var container = new EasyDependencyContainer();
            var registration = container.RegisterType(typeof(IFoo), typeof(Foo), Registration.RegistrationTypes.Singleton);
            var foo1 = container.ResolveType<IFoo>();
            var foo2 = container.ResolveType<IFoo>();

            var invocationRegistration = registration as InvocationRegistration;
            Assert.NotNull(invocationRegistration);

            Assert.Equal(foo1.Id, foo2.Id);
        }

        [Fact]
        public void InstanceInvokerResolvesTheOnlySingletonRegisteredInstance()
        {
            var container = new EasyDependencyContainer();
            var instanceOfType = new Foo();
            var registration = container.RegisterInstance<IFoo>(instanceOfType);

            var instanceRegistration = registration as InstanceRegistration;
            Assert.NotNull(instanceRegistration);

            var foo = container.ResolveType<IFoo>();

            Assert.Equal(foo.Id, instanceOfType.Id);
        }
    }
}
