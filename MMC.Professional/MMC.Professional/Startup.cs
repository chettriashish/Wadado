using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMC.Professional.Startup))]
namespace MMC.Professional
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
