using System;
using EasyDependencyInjection.Abstraction;

namespace EasyDependencyInjection.Easy.Registrations
{
    public abstract class EasyRegistration : Registration
    {
        protected EasyRegistration(Type fromType, Type toType)
            : base(fromType, toType)
        {
        }
    }
}