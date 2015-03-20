using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using Telerik.OpenAccess;
using Zakar.DataAccess;
using Zakar.DataAccess.Service;
using Zakar.Models;

[assembly: OwinStartupAttribute(typeof(Zakar.Startup))]
namespace Zakar
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
                .Where(i => i.Name.EndsWith("Service"))
                .InstancePerRequest();
            //register Report builderss
            builder.RegisterAssemblyTypes(persistenceAssembly)
                .Where(i => i.Name.EndsWith("Builder"))
                .InstancePerRequest();
            builder.RegisterGeneric(typeof (Repository<>)).As(typeof (IRepository<>)).InstancePerRequest();
           

            //register the EntitiesModel
            builder.RegisterType<EntitiesModel>().As<OpenAccessContext>().InstancePerRequest();
            builder.RegisterType<EntitiesModel>().As<IUnitOfWork>();
            //register owin integration
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ConfigureAuth(app);
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
