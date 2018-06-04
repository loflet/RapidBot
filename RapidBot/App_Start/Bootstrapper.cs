using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using RapidBot.Data.Infrastructure;
using RapidBot.Data.Repositories;
using RapidBot.Dialogs;
using RapidBot.Mappings;
using RapidBot.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace RapidBot.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();

            //Configure Automapper
            //AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterModule(new DialogModule());


            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
            //register the bot builder module
            // builder.RegisterModule(new LUISDialog());

            // register api controller
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Repositories
            builder.RegisterAssemblyTypes(typeof(CustomerRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Register your services
            builder.RegisterType<CustomerService>()
                .Keyed<ICustomerService>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // Services
            //builder.RegisterAssemblyTypes(typeof(CustomerService).Assembly)
               //.Where(t => t.Name.EndsWith("Service"))
              // .AsImplementedInterfaces().InstancePerRequest();
                        
            //Get your HttpConfiguration
            HttpConfiguration config = GlobalConfiguration.Configuration;

            //Set the dependency resolver to be Autofac.
            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}