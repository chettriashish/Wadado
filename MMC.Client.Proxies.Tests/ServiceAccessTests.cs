using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Client.Proxies;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Bootstrapper;
using Core.Common.Contracts;
using Core.Common.Core;
using MMC.Client.Contracts;
using MMC.Client.Entities;
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
            ///ChannelFactory<ILocationService> channelFactory =
            //      new ChannelFactory<ILocationService>("");
            //ILocationService proxy = channelFactory.CreateChannel();
            (proxy as ICommunicationObject).Open();
            //ActivityDetailsDataContract contract = new ActivityDetailsDataContract();
            //contract = proxy.GetAllActivitiesByLocationFilteredCategory("GANGTOK", "ADVENTURE",new DateTime(2016,1,9),new DateTime(2016,1,11), "smartphone");            
            try
            {
                IEnumerable<ActivityBookingDataContract> contract = proxy.GetAllCompanyActivitiesPendingForConfirmation("KewzingKhukuri");
                //IEnumerable<ActivitySummaryDataContract> contract = proxy.GetAllActivitiesByLocation("GANGTOK", "desktop");
                //IEnumerable<LocationDetailsDataContract> location = proxy.GetSelectedLocationDetails("DARJEELING");
                //List<string> newList = new List<string>();
                //newList.Add("223c0391-9c5f-4fe0-9471-bccb70084d6a");
                //IEnumerable<ActivityTypeMaster> contract = proxy.GetSubCategoriesForSelectedActivity("ADVENTURE");
                //proxy.SaveActivityCategoryMapping(newList,"ADVENTURE");
                //ActivityDetailsDataContract contract = proxy.GetAllActivities(default(string), "HIGHFLY", "desktop");
                //Dictionary<string, bool> activityDates = new Dictionary<string, bool>();
                //activityDates.Add("sun", true);
                //activityDates.Add("mon", true);
                //activityDates.Add("tue", true);
                //activityDates.Add("wed", true);
                //activityDates.Add("thu", true);
                //activityDates.Add("fri", true);
                //activityDates.Add("sat", true);

                //List<string> activityTimes = new List<string>();
                //activityTimes.Add("10:00AM");
                //activityTimes.Add("11:00AM");
                //activityTimes.Add("12:00PM");
                //activityTimes.Add("01:00PM");
                //activityTimes.Add("02:00PM");
                //activityTimes.Add("03:00PM");
                //activityTimes.Add("03:00PM");
                //proxy.SaveActivityDetails(contract, activityDates, activityTimes, "GANGTOK", "PARAGLIDING", "TDB");
                (proxy as ICommunicationObject).Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            
        }
    }
}
