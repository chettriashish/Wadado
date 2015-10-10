using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using MMC.Business.Contracts;

namespace MMC.ServiceHost.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void TestActivityManager()
        {
            ChannelFactory<IActivitiesService> channelFactory =
                new ChannelFactory<IActivitiesService>("");

            IActivitiesService proxy = channelFactory.CreateChannel();

            (proxy as ICommunicationObject).Open();

            channelFactory.Close();
        }       
    }
}
