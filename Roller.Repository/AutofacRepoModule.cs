using Autofac;
using Roller.DContext;
using Roller.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Roller.Repository
{
   public class AutofacRepoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RollerDataContext>().InstancePerLifetimeScope();

            // register dependency convention
            builder.RegisterAssemblyTypes(typeof(IDependencyRegister).Assembly)
                .AssignableTo<IDependencyRegister>().Where(t => t.Name.EndsWith("Manager"))
                .As<IDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);


        }
    }
}
