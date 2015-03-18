using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zakar.Startup))]
namespace Zakar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
