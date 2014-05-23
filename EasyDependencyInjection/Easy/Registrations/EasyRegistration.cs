using System;
using EasyDependencyInjection.Abstraction;

namespace EasyDependencyInjection.Easy.Registrations
{
    internal abstract class EasyRegistration : Registration
    {
        protected EasyRegistration(Type fromType, Type toType)
            : base(fromType, toType)
        {
        }
    }
}