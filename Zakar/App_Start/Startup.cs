using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using Telerik.OpenAccess;
using Zakar.App_Start;
using Zakar.Common.Builders;
using Zakar.Common.FileHandlers;
using Zakar.Common.Formatters;
using Zakar.Common.ListItemBuilders;
using Zakar.Common.Messaging;
using Zakar.DataAccess;
using Zakar.DataAccess.Service;
using Zakar.Models;

[assembly: OwinStartup(typeof(Startup))]
namespace Zakar.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            //register web abstractions
            builder.RegisterModule<AutofacWebTypesModule>();
            //register Model Binders
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            //register filter providers
            builder.RegisterFilterProvider();
            //register persistence services
            var persistenceAssembly = typeof(ChurchService).Assembly;
            builder.RegisterAssemblyTypes(persistenceAssembly)
                .Where(i => i.Name.EndsWith("Service") && !i.Name.Equals("UserService") && !i.Name.Equals("RoleService"))
                .InstancePerRequest();

            //Register File Handlers
            builder.RegisterType<GroupExcelFileHandler>().AsSelf().InstancePerRequest();
            builder.RegisterType<ChapterExcelFileHandler>().AsSelf().InstancePerRequest();
            builder.RegisterType<ZoneExcelFileHandler>().AsSelf().InstancePerRequest();
            //register Report builderss

            builder.RegisterType<UserService>().As<IUserStore<IdentityUser>>();
            builder.RegisterType<UserService>().As<IUserClaimStore<IdentityUser>>();
            builder.RegisterType<UserService>().As<IUserRoleStore<IdentityUser>>();
            builder.RegisterType<UserService>().As<IUserLoginStore<IdentityUser>>();
            builder.RegisterType<UserService>().As<IUserPasswordStore<IdentityUser>>();
            builder.RegisterType<RoleService>().As<IRoleStore<IdentityRole>>();
       
            builder.RegisterAssemblyTypes(persistenceAssembly)
                .Where(i => i.Name.EndsWith("Builder"))
                .InstancePerRequest();
            builder.RegisterGeneric(typeof (Repository<>)).As(typeof (IRepository<>)).InstancePerRequest();
            builder.RegisterType<PhoneNumberFormatter>().As<IPhoneNumberFormatter>().InstancePerDependency();
            builder.RegisterType<XWirelessMessageSender>().As<IMessageSender>().InstancePerDependency();
            builder.RegisterType<PartnershipAlertSMSBuilder>().As<IMessageBuilder<Partnership, string>>().InstancePerDependency();
            builder.RegisterType<PartnershipAnalyticsBuilder>().AsSelf().InstancePerRequest();
            builder.RegisterType<PartnerAnalyticsBuilder>().AsSelf().InstancePerRequest();
            builder.RegisterType<PartnerAlertSmsBuilder>()
                .As<IMessageBuilder<Partner, string>>()
                .InstancePerDependency();
            builder.RegisterType<ChurchListBuilder>().AsSelf();
            builder.RegisterType<CurrencyListBuilder>().AsSelf();
            builder.RegisterType<MonthListBuilder>().AsSelf();
            builder.RegisterType<PartnershipArmListBuilder>().AsSelf();
            builder.RegisterType<PartnerTitleListBuilder>().AsSelf();
            builder.RegisterType<YearListBuilder>().AsSelf();
            builder.RegisterType<EntitiesModel>().As<IUnitOfWork>().InstancePerRequest();
            builder.Register(t => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.RegisterType<ApplicationSignInManager>().AsSelf();
            builder.RegisterType<ApplicationUserManager>().AsSelf();
            //register owin integration
            var container = builder.Build();
            var dependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(dependencyResolver);
            ConfigureAuth(app);
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            
        }
    }
}
