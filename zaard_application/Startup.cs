using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(zaard_application.Startup))]
namespace zaard_application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
