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
using System.Reflection;


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
            //ActivitySearchDataContract test = new ActivitySearchDataContract();
            //MethodInfo testMethod = test.GetType().GetMethod("helloWorld",BindingFlags.Instance | BindingFlags.NonPublic);
            //testMethod.Invoke(test, null);
            //ChannelFactory<IUsersService> channelFactory =
            //   new ChannelFactory<IUsersService>("");

            //IUsersService proxy = channelFactory.CreateChannel();
            //(proxy as ICommunicationObject).Open();

            //ActivityDetailsDataContract contract = new ActivityDetailsDataContract();

            ChannelFactory<IActivitiesService> channelFactory =
                   new ChannelFactory<IActivitiesService>("");
            IActivitiesService proxy = channelFactory.CreateChannel();
            //ChannelFactory<ILocationService> channelFactory =
                  new ChannelFactory<ILocationService>("");
            //ILocationService proxy = channelFactory.CreateChannel();
            (proxy as ICommunicationObject).Open();

            //CompanyMaster newCompany = new CompanyMaster
            //{
            //    Address = "asdsd",
            //    ContactPerson = "dasdsad",
            //    Email = "dasdasd",
            //    Name = "dasdsads",
            //    TelephoneNumber = "DSadasd",
            //    CreatedDate = DateTime.Now,
            //    CreatedBy = "nexus1234@gmail.com",
            //    Rating = 0,
            //};

            //CompanyMaster company = proxy.CreateCompanyForSelectedUser("nexus1234@gmail.com", newCompany);
            //ActivityDetailsDataContract contract = new ActivityDetailsDataContract();
            
            //contract = proxy.GetAllActivitiesByLocationFilteredCategory("GANGTOK", "ADVENTURE",new DateTime(2016,1,9),new DateTime(2016,1,11), "smartphone");            
            //IEnumerable<ActivityBookingDataContract> contract = proxy.GetAllActivitiesPendingForConfirmation();
            try
            {
                //IEnumerable<ActivitySummaryDataContract> contract = proxy.GetAllActivitiesByLocation("GANGTOK", "desktop");            
                List<string> tags = new List<string>() { "GANG" };
                var result = proxy.GetActivitiesForSelectedSearchTag(tags);
                //IEnumerable<ActivityBookingDataContract> contract = proxy.GetAllCompanyActivitiesPendingForConfirmation("KewzingKhukuri");
                //IEnumerable<ActivitySummaryDataContract> contract = proxy.GetAllActivitiesByLocation("GANGTOK", "desktop");
                //IEnumerable<LocationDetailsDataContract> location = proxy.GetSelectedLocationDetails("DARJEELING");
                //List<string> newList = new List<string>();
                //newList.Add("223c0391-9c5f-4fe0-9471-bccb70084d6a");
                //IEnumerable<ActivityTypeMaster> contract = proxy.GetSubCategoriesForSelectedActivity("ADVENTURE");
                //proxy.SaveActivityCategoryMapping(newList,"ADVENTURE");
                //ActivityDetailsDataContract contract = proxy.GetAllActivities("GANGTOK", "9a244d63-8e8b-49f9-9d2b-afed95fbee69", "desktop");
                //contract.AllowInstantBooking = true;
                //Dictionary<string, bool> activityDates = new Dictionary<string, bool>();
                //activityDates.Add("sun", true);
                //activityDates.Add("mon", true);
                //activityDates.Add("tue", true);
                //activityDates.Add("wed", true);
                //activityDates.Add("thu", true);
                //activityDates.Add("fri", true);
                //activityDates.Add("sat", true);
                //contract.AllActivityUniqueDates = new List<ActivityDates>();
                //contract.Tags = new List<string>(){"gangtok","trekking","biking","sikkim"};
                //List<ActivityDates> result = new List<ActivityDates>();
                //ActivityDates newDate = new ActivityDates
                //{
                //    Date = DateTime.Now,
                //    Time = "10AM",
                //    IsDeleted = false
                //};
                //result.Add(newDate);
                //contract.AllActivityUniqueDates = result;
                //ActivityPriceMapping priceOptions = new ActivityPriceMapping() { OptionDescription = "With Guide", PriceForAdults = 1800, PriceForChildren = 1500, CreatedDate = DateTime.Now };
                //List<ActivityPriceMapping> allPricingOptions = new List<ActivityPriceMapping>();
                //allPricingOptions.Add(priceOptions);
                //contract.ActivityPriceOption = allPricingOptions;
                //List<string> activityTimes = new List<string>();
                //activityTimes.Add("10:00AM");
                //activityTimes.Add("11:00AM");
                //activityTimes.Add("12:00PM");
                //activityTimes.Add("01:00PM");
                //activityTimes.Add("02:00PM");
                //activityTimes.Add("03:00PM");
                //activityTimes.Add("03:00PM");
                //proxy.SaveActivityDetails(contract, activityDates, activityTimes, "GANGTOK", "4a449eff-e3d6-4eab-90ef-116bcb9b90d3", "TDB");

                ////IEnumerable<LocationDetailsDataContract> results = proxy.GetSelectedLocationDetailsForClientApplication("Wellington", "desktop");
                (proxy as ICommunicationObject).Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
