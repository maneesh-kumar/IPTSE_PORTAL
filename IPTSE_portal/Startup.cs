using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IPTSE_portal.Startup))]
namespace IPTSE_portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
