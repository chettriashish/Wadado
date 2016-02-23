using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common.Core;
using MMC.Client.Bootstrapper;
using MMC.Client.Contracts;
using MMC.Client.Proxies;
using Microsoft.Practices.Unity;
using Core.Common.Contracts;
namespace MMC.Client.Proxies.Tests
{
    [TestClass]
    public class ProxyObtainmentTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = Bootstrapper.Bootstrapper.Initialise();
        }

        [TestMethod]
        public void ObtainProxyFromContainerUsingServiceContract()
        {
            ILocationService proxy =
                ObjectBase.Container.Resolve<ILocationService>();

            Assert.IsTrue(proxy is LocationClient);
        }
        [TestMethod]
        public void ObtainServiceFactoryAndproxyFromContainer()
        {
            IServiceFactory factory = 
                ObjectBase.Container.Resolve<IServiceFactory>();

            ILocationService proxy = factory.CreateClient<ILocationService>();            
            Assert.IsTrue(proxy is LocationClient);
        }
    }
}
