using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace RealtimeAngular
{
    public class BackgroundTicker : IRegisteredObject
    {
        private Timer taskTimer;
        private IHubContext hub;
        public double bandWidthPct = 20.0;
        public double cpuPct = 10.0;
        public double salesAmt = 1000.0;
        public double alphaSalesAmt = 700.0;
        public double betaSalesAmt = 300.0;
        public BackgroundTicker()
        {
            HostingEnvironment.RegisterObject(this);
            hub = GlobalHost.ConnectionManager.GetHubContext<MetricHub>();
            taskTimer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(.1));
        }

        private void OnTimerElapsed(object sender)
        {
            hub.Clients.All.broadcastMessage(DateTime.UtcNow.ToString(), bandWidthPct, cpuPct, salesAmt, alphaSalesAmt, betaSalesAmt);
            Random r = new Random();
            bandWidthPct += 15 * r.NextDouble() - 7.5;

            if (bandWidthPct > 100) bandWidthPct = 100;
            if (bandWidthPct < 0) bandWidthPct = 0;

            cpuPct += 15 * r.NextDouble() - 7.5;
            if (cpuPct > 100) cpuPct = 100;
            if (cpuPct < 0) cpuPct = 0;

            alphaSalesAmt += r.NextDouble() * 10;
            if (alphaSalesAmt < 0) alphaSalesAmt = 0;

            betaSalesAmt += r.NextDouble() * 10;
            if (betaSalesAmt < 0) betaSalesAmt = 0;

            salesAmt = alphaSalesAmt + betaSalesAmt;
        }

        public void Stop(bool immediate)
        {
            taskTimer.Dispose();
            HostingEnvironment.UnregisterObject(this);
        }
    }
}