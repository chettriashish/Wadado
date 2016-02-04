using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RealtimeAngular.Startup))]

namespace RealtimeAngular
{
    public class Startup
    {
        private BackgroundTicker _backgroundTicker = null;
        public void Configuration(IAppBuilder app)
        {
            _backgroundTicker = new BackgroundTicker();
            app.MapSignalR();
        }
    }
}
