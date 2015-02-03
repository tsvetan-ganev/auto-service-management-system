using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoServiceManagementSystem.Startup))]
namespace AutoServiceManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
