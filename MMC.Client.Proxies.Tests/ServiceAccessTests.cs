using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Client.Proxies.Proxies;

namespace MMC.Client.Proxies.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void TestLocationClientConnection()
        {
            ActivityClient proxy = new ActivityClient();

            proxy.Open();
        }
    }
}
