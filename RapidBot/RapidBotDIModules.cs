using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using RapidBot.Dialogs;
using RapidBot.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace RapidBot
{
    public class RapidBotDIModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DialogFactory>()
                .Keyed<IDialogFactory>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register the RootDialog as IDialog<object>
            builder.RegisterType<RootDialog>()
                .As<IDialog<object>>()
                .InstancePerDependency();

            // Register the dialogs
            builder.RegisterType<LUISDialog>().InstancePerDependency();

            builder.RegisterType<HttpClient>()
                .Keyed<HttpClient>(FiberModule.Key_DoNotSerialize)
                .AsSelf()
                .InstancePerDependency();
        }
    }
}