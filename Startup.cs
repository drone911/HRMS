using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRMSProj.Startup))]
namespace HRMSProj
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
