using System;

namespace EasyDependencyInjection.Abstraction
{
    /// <summary>
    /// The dependency resolver interface.
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        /// <summary>
        /// The resolve.
        /// </summary>
        /// <typeparam name="TServiceType">
        /// Type of service to resolve.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TServiceType"/>.
        /// </returns>
        TServiceType Resolve<TServiceType>();

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object Resolve(Type serviceType);

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="registrationType">
        /// The registration Type.
        /// </param>
        /// <typeparam name="TInterface">
        /// Type of the interface.
        /// </typeparam>
        /// <typeparam name="TClass">
        /// Type of the class that is derived from TInterface.
        /// </typeparam>
        /// <returns>
        /// The <see>
        ///         <cref>Registration</cref>
        ///     </see>
        ///     .
        /// </returns>
        Registration Register<TInterface, TClass>(Registration.RegistrationTypes registrationType = Registration.RegistrationTypes.Transient)
            where TClass : TInterface
            where TInterface : class;

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="instanceOfType">
        /// The instance of type.
        /// </param>
        /// <typeparam name="TInterface">
        /// Type of the interface of the instance.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Registration"/>.
        /// </returns>
        Registration Register<TInterface>(TInterface instanceOfType)
            where TInterface : class;

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="interfaceType">
        /// The interface type.
        /// </param>
        /// <param name="classType">
        /// The class type.
        /// </param>
        /// <param name="registrationType">
        /// The registration Type.
        /// </param>
        /// <returns>
        /// The <see cref="Registration"/>.
        /// </returns>
        Registration Register(Type interfaceType, Type classType, Registration.RegistrationTypes registrationType = Registration.RegistrationTypes.Transient);

        /// <summary>
        /// The create child container.
        /// </summary>
        /// <returns>
        /// The <see cref="IDependencyResolver"/>.
        /// </returns>
        IDependencyResolver CreateChildContainer();

        /// <summary>
        /// The register using factory.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="factoryMethod">
        /// The factory method.
        /// </param>
        /// <typeparam name="TServiceType">
        /// Type of the service that is going to be resolved.
        /// </typeparam>
        void RegisterUsingFactory<TServiceType>(string name, Func<IDependencyResolver, TServiceType> factoryMethod)
                where TServiceType : class;
    }
}