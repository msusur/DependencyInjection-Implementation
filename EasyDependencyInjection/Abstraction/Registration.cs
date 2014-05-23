using System;

namespace EasyDependencyInjection.Abstraction
{
    /// <summary>
    /// The registration.
    /// </summary>
    public abstract class Registration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Registration"/> class.
        /// </summary>
        /// <param name="fromType">
        /// The from type.
        /// </param>
        /// <param name="toType">
        /// The to type.
        /// </param>
        protected Registration(Type fromType, Type toType)
        {
            FromType = fromType;
            ToType = toType;
        }

        /// <summary>
        /// The registration types.
        /// </summary>
        public enum RegistrationTypes
        {
            /// <summary>
            /// The singleton.
            /// </summary>
            Singleton,

            /// <summary>
            /// The transient.
            /// </summary>
            Transient
        }

        /// <summary>
        /// Gets the from type.
        /// </summary>
        public Type FromType { get; private set; }

        /// <summary>
        /// Gets the to type.
        /// </summary>
        public Type ToType { get; private set; }

        /// <summary>
        /// Gets or sets the registration type.
        /// </summary>
        public RegistrationTypes RegistrationType { get; set; }
    }
}