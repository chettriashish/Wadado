using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Client.Proxies;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Bootstrapper;
using Core.Common.Contracts;
using Core.Common.Core;
using MMC.Client.Contracts;
using Microsoft.Practices.Unity;
using System.ServiceModel;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Collections.Generic;


namespace MMC.Client.Proxies.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = Bootstrapper.Bootstrapper.Initialise();
        }

        [TestMethod]
        public void TestLocationClientConnection()
        {
            //ChannelFactory<IUsersService> channelFactory =
            //   new ChannelFactory<IUsersService>("");

            //IUsersService proxy = channelFactory.CreateChannel();
            //(proxy as ICommunicationObject).Open();

            //ActivityDetailsDataContract contract = new ActivityDetailsDataContract();

            ChannelFactory<IActivitiesService> channelFactory =
                   new ChannelFactory<IActivitiesService>("");
            IActivitiesService proxy = channelFactory.CreateChannel();
            (proxy as ICommunicationObject).Open();
            IEnumerable<ActivitySummaryDataContract> contract = new List<ActivitySummaryDataContract>();
            contract = proxy.GetAllActivitiesByLocationFilteredCategory("GANGTOK", "ADVENTURE",new DateTime(2016,1,12),new DateTime(2016,1,16), "smartphone");            
            (proxy as ICommunicationObject).Close();
        }
    }
}
