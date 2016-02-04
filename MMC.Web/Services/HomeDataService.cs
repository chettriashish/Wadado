using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using MMC.Common.Contracts.ServiceContracts;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Services
{
    public class HomeDataService : IHomeDataService
    {
        const string MOBILE = "_mobile";
        const string TABLET = "_tab";
        public IEnumerable<TopOffersDataContract> GetTopOffers(string userAgent)
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            IEnumerable<TopOffersDataContract> results = GetDummyTopOffers();
            if (deviceInfo.GetVirtualCapability("is_mobile") == "true")
            {
                if (deviceInfo.GetVirtualCapability("is_smartphone") == "true")
                {
                    foreach (var item in results)
                    {
                        item.ImageURL = string.Format("{0}{1}", item.ImageURL, MOBILE);
                    }
                }
                else
                {
                    foreach (var item in results)
                    {
                        item.ImageURL = string.Format("{0}{1}", item.ImageURL, TABLET);
                    }
                }
            }           
            return results;
        }

        public IEnumerable<ActivitySummaryDataContract> GetTrendingActivities(string userAgent)
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            IEnumerable<ActivitySummaryDataContract> results = GetDummyData(userAgent);

            if (deviceInfo.GetVirtualCapability("is_mobile") == "true")
            {
                if (deviceInfo.GetVirtualCapability("is_smartphone") == "true")
                {
                    foreach (var item in results)
                    {
                        item.ImageURL = string.Format("{0}{1}", item.ImageURL, MOBILE);
                    }
                }
                else
                {                    
                    foreach (var item in results)
                    {
                        item.ImageURL = string.Format("{0}{1}", item.ImageURL, TABLET);                    
                    }
                }
            }            
            return results;
        }
        private IEnumerable<ActivitySummaryDataContract> GetDummyData(string userAgent)
        {
            List<ActivitySummaryDataContract> results = new List<ActivitySummaryDataContract>();
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);

            ActivitySummaryDataContract activityOne = new ActivitySummaryDataContract();

            activityOne.ActivityKey = "ACTIVITY ONE";
            activityOne.ActivityName = "Learn the art of the famous traditional gurkha khukuri";
            activityOne.Rating = Convert.ToDecimal(3.5);            
            activityOne.Cost = 1000;
            activityOne.Currency = "INR";
            activityOne.ImageURL = "Images/khukuri1";
            results.Add(activityOne);


            ActivitySummaryDataContract activityTwo = new ActivitySummaryDataContract();
            activityTwo.ActivityKey = "ACTIVITY TWO";
            activityTwo.ActivityName = "Enjoy a tradional maruni dance performance";
            activityTwo.Rating = Convert.ToDecimal(3.5);
            activityTwo.Cost = 1000;
            activityTwo.Currency = "INR";
            activityTwo.ImageURL = "Images/maruni1";
            results.Add(activityTwo);

            ActivitySummaryDataContract activityThree = new ActivitySummaryDataContract();
            activityThree.ActivityKey = "ACTIVITY THREE";
            activityThree.ActivityName = "Explore the villages of Sikkim and learn and enjoy the old art of butter milk making.";
            activityThree.Rating = Convert.ToDecimal(3.5);
            activityThree.Cost = 1000;
            activityThree.Currency = "INR";
            activityThree.ImageURL = "Images/buttermilk2";
            results.Add(activityThree);


            ActivitySummaryDataContract activityFour = new ActivitySummaryDataContract();
            activityFour.ActivityKey = "ACTIVITY FOUR";
            activityFour.ActivityName = "Learn and buy tradiional handicrafts";
            activityFour.Rating = Convert.ToDecimal(3.5);
            activityFour.Cost = 1000;
            activityFour.Currency = "INR";
            activityFour.ImageURL = "Images/village2";
            results.Add(activityFour);

            ActivitySummaryDataContract activityFive = new ActivitySummaryDataContract();
            activityFive.ActivityKey = "ACTIVITY FIVE";
            activityFive.ActivityName = "Enjoy a tradional nepali village meal";
            activityFive.Rating = Convert.ToDecimal(3.5);
            activityFive.Cost = 1000;
            activityFive.Currency = "INR";
            activityFive.ImageURL = "Images/village1";
            results.Add(activityFive);

            ActivitySummaryDataContract activitySix = new ActivitySummaryDataContract();
            activitySix.ActivityKey = "ACTIVITY SIX";
            activitySix.ActivityName = "Become a farmer for a day";
            activitySix.Rating = Convert.ToDecimal(3.5);
            activitySix.Cost = 1000;
            activitySix.Currency = "INR";
            activitySix.ImageURL = "Images/village3";
            results.Add(activitySix);            
            return results;
        }
        private IEnumerable<TopOffersDataContract> GetDummyTopOffers()
        {
            List<TopOffersDataContract> result = new List<TopOffersDataContract>();
            TopOffersDataContract topOffer1 = new TopOffersDataContract();            
            topOffer1.TopOffersKey = "TOPOFFER1";
            topOffer1.Discount = 10;
            topOffer1.Cost = 450;
            topOffer1.ImageURL = "Images/khukuri2";
            topOffer1.Currency = "INR";
            topOffer1.Key = "ACTIVITY ONE";
            topOffer1.Location = "GANGTOK";
            topOffer1.OfferType = "Activities";
            topOffer1.Value = "LOREM IPSUM";
            topOffer1.Rating = 3M;
            result.Add(topOffer1);

            TopOffersDataContract topOffer2 = new TopOffersDataContract();
            topOffer2.TopOffersKey = "TOPOFFER2";
            topOffer2.Discount = 10;
            topOffer2.Cost = 500;
            topOffer2.ImageURL = "Images/khukuri1";
            topOffer2.Currency = "INR";
            topOffer2.Key = "ACTIVITY TWO";
            topOffer2.Location = "GANGTOK";
            topOffer2.OfferType = "Activities";
            topOffer2.Value = "LOREM IPSUM";
            topOffer2.Rating = 3.5M;
            result.Add(topOffer2);


            TopOffersDataContract topOffer3 = new TopOffersDataContract();
            topOffer3.TopOffersKey = "TOPOFFER3";
            topOffer3.Discount = 15;
            topOffer3.Cost = 50;
            topOffer3.ImageURL = "Images/maruni1";
            topOffer3.Currency = "INR";
            topOffer3.Key = "ACTIVITY THREE";
            topOffer3.Location = "GANGTOK";
            topOffer3.OfferType = "Activities";
            topOffer3.Value = "LOREM IPSUM";
            topOffer3.Rating = 4M;
            result.Add(topOffer3);


            TopOffersDataContract topOffer4 = new TopOffersDataContract();
            topOffer4.TopOffersKey = "TOPOFFER4";
            topOffer4.Discount = 12;
            topOffer4.Cost = 100;
            topOffer4.ImageURL = "Images/maruni2";
            topOffer4.Currency = "INR";
            topOffer4.Key = "ACTIVITY FOUR";
            topOffer4.Location = "GANGTOK";
            topOffer4.OfferType = "Activities";
            topOffer4.Value = "LOREM IPSUM";
            topOffer4.Rating = 4.5M;
            result.Add(topOffer4);


            TopOffersDataContract topOffer5 = new TopOffersDataContract();
            topOffer5.TopOffersKey = "TOPOFFER5";
            topOffer5.Discount = 10;
            topOffer5.Cost = 150;
            topOffer5.ImageURL = "Images/village2";
            topOffer5.Currency = "INR";
            topOffer5.Key = "ACTIVITY FIVE";
            topOffer5.Location = "GANGTOK";
            topOffer5.OfferType = "Activities";
            topOffer5.Value = "LOREM IPSUM";
            topOffer5.Rating = 4.5M;
            result.Add(topOffer5);
            return result;
        }
        public IEnumerable<LocationsMaster> GetAllLocations()
        {
            List<LocationsMaster> results = new List<LocationsMaster>();
            LocationsMaster location1 = new LocationsMaster();
            location1.LocationKey = Guid.NewGuid().ToString();
            location1.LocationName = "Gangtok";
            location1.Country = "INDIA";
            results.Add(location1);
            LocationsMaster location2 = new LocationsMaster();
            location2.LocationKey = Guid.NewGuid().ToString();
            location2.LocationName = "Darjeeling";
            location2.Country = "INDIA";
            results.Add(location2);
            LocationsMaster location3 = new LocationsMaster();
            location3.LocationKey = Guid.NewGuid().ToString();
            location3.LocationName = "Kalimpong";
            location3.Country = "INDIA";
            results.Add(location3);
            LocationsMaster location4 = new LocationsMaster();
            location4.LocationKey = Guid.NewGuid().ToString();
            location4.LocationName = "Namchi";
            location4.Country = "INDIA";
            results.Add(location4);
            LocationsMaster location5 = new LocationsMaster();
            location5.LocationKey = Guid.NewGuid().ToString();
            location5.LocationName = "Siliguri";
            location5.Country = "INDIA";
            results.Add(location5);
            LocationsMaster location6 = new LocationsMaster();
            location6.LocationKey = Guid.NewGuid().ToString();
            location6.LocationName = "Pelling";
            location6.Country = "INDIA";
            results.Add(location6);
            return results;
        }

        public IEnumerable<NewsModel> GetLastestNews()
        {
            List<NewsModel> results = new List<NewsModel>();

            NewsModel news1 = new NewsModel();
            news1.Details = "Ashish Chettri is the best of the best of the best!!";
            news1.Agency = "BBC World News";
            results.Add(news1);

            NewsModel news2 = new NewsModel();
            news2.Details = "Ashish Chettri is the best of the best of the best!!";
            news2.Agency = "ABC World News";
            results.Add(news2);
            return results;
        }
    }
}