using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Services
{
    public class LocationDataService : ILocationDataService
    {
        const string MOBILE = "_mobile";
        const string TABLET = "_tab";

        public LocationDetailsDataContract GetAllActivitiesForSelectedLocation(string locationName, string userAgent)
        {
            LocationDetailsDataContract result = new LocationDetailsDataContract();
            if (userAgent == "smartphone")
            {
                result = GetLocationDetails(userAgent.ToUpper(), locationName);
                foreach (var item in result.AllActivities)
                {
                    item.ImageURL = string.Format("Images/icons/{0}.png", item.ImageURL);
                }
                result.ImageURL = string.Format("Images/mobile/{0}", result.ImageURL);
            }
            else if (userAgent == "tablet")
            {
                result = GetLocationDetails(userAgent.ToUpper(), locationName);
                foreach (var item in result.AllActivities)
                {
                    item.ImageURL = string.Format("Images/icons/{0}.png", item.ImageURL);
                }
                result.ImageURL = string.Format("Images/tablet/{0}", result.ImageURL);
            }
            else
            {
                result = GetLocationDetails(userAgent.ToUpper(), locationName);
                result.ImageURL = string.Format("Images/desktop/{0}", result.ImageURL);
            }
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

            LocationsMaster location7 = new LocationsMaster();
            location7.LocationKey = Guid.NewGuid().ToString();
            location7.LocationName = "Paro";
            location7.Country = "BHUTAN";
            results.Add(location7);

            LocationsMaster location8 = new LocationsMaster();
            location8.LocationKey = Guid.NewGuid().ToString();
            location8.LocationName = "Thimpu";
            location8.Country = "BHUTAN";
            results.Add(location8);

            LocationsMaster location9 = new LocationsMaster();
            location9.LocationKey = Guid.NewGuid().ToString();
            location9.LocationName = "Pokhara";
            location9.Country = "NEPAL";
            results.Add(location9);
            return results;
        }
        private LocationDetailsDataContract GetLocationDetails(string deviceInfo, string locationName)
        {
            LocationDetailsDataContract results = new LocationDetailsDataContract();
            if (deviceInfo != "SMARTPHONE" && deviceInfo != "TABLET")
            {
                results.TopOffersForLocation = GetDummyTopOffers(locationName);
                results.DefaultActivityCategoryKey = "TOPTRENDING";
            }
            else
            {
                results.AllActivities = GetAllActivityCategoriesForLocation(locationName);
            }
            results.ImageURL = "gangtok";
            results.LocationName = "Gangtok";
            results.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

            results.GettingAround = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

            results.MustDrink = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor consectetur adipiscing elit, sed do eiusmod.";
            results.MustEat = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor consectetur adipiscing elit, sed do eiusmod.";
            return results;
        }

        public IEnumerable<ActivitySummaryDataContract> GetTopTrendingActivities(string locationName)
        {
            List<ActivitySummaryDataContract> activities = new List<ActivitySummaryDataContract>();

            ActivitySummaryDataContract activityOne = new ActivitySummaryDataContract();
            activityOne.ActivityKey = "ACTIVITY ONE";
            activityOne.ActivityName = "Learn the art of the famous traditional gurkha khukuri";
            activityOne.Rating = Convert.ToDecimal(3.5);
            activityOne.Cost = 1000;
            activityOne.Currency = "INR";
            activityOne.ImageURL = "Images/khukuri1";
            activityOne.ActivityCategory = "TOP TRENDING ACTIVITIES";
            activityOne.ActivityCategoryKey = "TOPTRENDING";
            activities.Add(activityOne);


            ActivitySummaryDataContract activityTwo = new ActivitySummaryDataContract();
            activityTwo.ActivityKey = "ACTIVITY TWO";
            activityTwo.ActivityName = "Enjoy a tradional maruni dance performance";
            activityTwo.Rating = Convert.ToDecimal(3.5);
            activityTwo.Cost = 1000;
            activityTwo.Currency = "INR";
            activityTwo.ImageURL = "Images/maruni1";
            activityTwo.ActivityCategory = "TOP TRENDING ACTIVITIES";
            activityTwo.ActivityCategoryKey = "TOPTRENDING";
            activities.Add(activityTwo);

            ActivitySummaryDataContract activityThree = new ActivitySummaryDataContract();
            activityThree.ActivityKey = "ACTIVITY THREE";
            activityThree.ActivityName = "Explore the villages of Sikkim and learn and enjoy the old art of butter milk making.";
            activityThree.Rating = Convert.ToDecimal(3.5);
            activityThree.Cost = 1000;
            activityThree.Currency = "INR";
            activityThree.ImageURL = "Images/buttermilk2";
            activityThree.ActivityCategory = "TOP TRENDING ACTIVITIES";
            activityThree.ActivityCategoryKey = "TOPTRENDING";
            activities.Add(activityThree);


            ActivitySummaryDataContract activityFour = new ActivitySummaryDataContract();
            activityFour.ActivityKey = "ACTIVITY FOUR";
            activityFour.ActivityName = "Learn and buy tradiional handicrafts";
            activityFour.Rating = Convert.ToDecimal(3.5);
            activityFour.Cost = 1000;
            activityFour.Currency = "INR";
            activityFour.ImageURL = "Images/village2";
            activityFour.ActivityCategory = "TOP TRENDING ACTIVITIES";
            activityFour.ActivityCategoryKey = "TOPTRENDING";
            activities.Add(activityFour);

            ActivitySummaryDataContract activityFive = new ActivitySummaryDataContract();
            activityFive.ActivityKey = "ACTIVITY FIVE";
            activityFive.ActivityName = "Enjoy a tradional nepali village meal";
            activityFive.Rating = Convert.ToDecimal(3.5);
            activityFive.Cost = 1000;
            activityFive.Currency = "INR";
            activityFive.ImageURL = "Images/village1";
            activityFive.ActivityCategory = "TOP TRENDING ACTIVITIES";
            activityFive.ActivityCategoryKey = "TOPTRENDING";
            activities.Add(activityFive);

            ActivitySummaryDataContract activitySix = new ActivitySummaryDataContract();
            activitySix.ActivityKey = "ACTIVITY SIX";
            activitySix.ActivityName = "Become a farmer for a day";
            activitySix.Rating = Convert.ToDecimal(3.5);
            activitySix.Cost = 1000;
            activitySix.Currency = "INR";
            activitySix.ImageURL = "Images/village3";
            activitySix.ActivityCategory = "TOP TRENDING ACTIVITIES";
            activitySix.ActivityCategoryKey = "TOPTRENDING";
            activities.Add(activitySix);

            return activities;
        }

        private IEnumerable<TopOffersDataContract> GetDummyTopOffers(string locationName)
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

        private IEnumerable<ActivityCategoryDataContract> GetAllActivityCategoriesForLocation(string locationName)
        {
            List<ActivityCategoryDataContract> result = new List<ActivityCategoryDataContract>();

            ActivityCategoryDataContract ac1 = new ActivityCategoryDataContract() { ActivityName = "Adventure", ActivityCount = 12, ImageURL = "Adventure", ActivityKey = "Adventure" };
            ActivityCategoryDataContract ac2 = new ActivityCategoryDataContract() { ActivityName = "Ecotourism", ActivityCount = 20, ImageURL = "EcoTourism", ActivityKey="EcoTourism" };
            ActivityCategoryDataContract ac3 = new ActivityCategoryDataContract() { ActivityName = "Entertainment", ActivityCount = 5, ImageURL = "Entertainment" };
            ActivityCategoryDataContract ac4 = new ActivityCategoryDataContract() { ActivityName = "Events and Festivals", ActivityCount = 2, ImageURL = "events_festivals" };
            ActivityCategoryDataContract ac5 = new ActivityCategoryDataContract() { ActivityName = "Food and Drinks", ActivityCount = 10, ImageURL = "FoodDrinks" };
            ActivityCategoryDataContract ac6 = new ActivityCategoryDataContract() { ActivityName = "Day Trips", ActivityCount = 12, ImageURL = "day_trips" };
            ActivityCategoryDataContract ac7 = new ActivityCategoryDataContract() { ActivityName = "Health and Beauty", ActivityCount = 2, ImageURL = "health_beauty" };
            ActivityCategoryDataContract ac8 = new ActivityCategoryDataContract() { ActivityName = "NightLife", ActivityCount = 3, ImageURL = "NightLife" };
            ActivityCategoryDataContract ac9 = new ActivityCategoryDataContract() { ActivityName = "With Kids", ActivityCount = 2, ImageURL = "kids" };
            ActivityCategoryDataContract ac10 = new ActivityCategoryDataContract() { ActivityName = "Learn Something new", ActivityCount = 3, ImageURL = "Learn" };
            ActivityCategoryDataContract ac11 = new ActivityCategoryDataContract() { ActivityName = "Weekend Getaways", ActivityCount = 4, ImageURL = "WeekendGetaways" };
            result.Add(ac1);
            result.Add(ac2);
            result.Add(ac3);
            result.Add(ac4);
            result.Add(ac5);
            result.Add(ac6);
            result.Add(ac7);
            result.Add(ac8);
            result.Add(ac9);
            result.Add(ac10);
            result.Add(ac11);
            return result;
        }
    }
}