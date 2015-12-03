using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using MMC.Business.Contracts;
using Core.Common.Core;
using MMC.Business.Bootstrapper;
using System.ServiceModel.Web;

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
            proxy.GetAllActivities("GANGTOK", "HIGHFLY", null);            
            channelFactory.Close();
        }       
    }
}
