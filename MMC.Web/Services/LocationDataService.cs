using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Services
{
    public class LocationDataService:ILocationDataService
    {
        const string MOBILE = "_mobile";
        const string TABLET = "_tab";

        public LocationModel GetAllActivitiesForSelectedLocation(string locationName, string userAgent)
        {
            LocationModel result = new LocationModel();
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            if (deviceInfo.GetVirtualCapability("is_mobile") == "true")
            {
                if (deviceInfo.GetVirtualCapability("is_smartphone") == "true")
                {
                    result = GetLocationDetailsForMobile(locationName);
                    foreach (var item in result.AllActivities)
                    {
                        item.ImageURL = string.Format("Images/icons/{0}.png", item.ImageURL);
                    }                    
                    result.DefaultLocationImageURL = string.Format("Images/mobile/{0}", result.SelectedLocation.LocationImage);
                }
                else
                {
                    result = GetLocationDetailsForTab(locationName);
                    foreach (var item in result.AllActivities)
                    {
                        item.ImageURL = string.Format("Images/icons/{0}.png", item.ImageURL);
                    }
                    result.DefaultLocationImageURL = string.Format("Images/tablet/{0}", result.SelectedLocation.LocationImage);
                }
            }
            else
            {
                result = GetLocationDetails(locationName);
                foreach (var item in result.AllActivities)
                {
                    item.ImageURL = string.Format("Images/icons/{0}.png", item.ImageURL);
                }
                result.DefaultLocationImageURL = string.Format("Images/desktop/{0}", result.SelectedLocation.LocationImage);
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
        private LocationModel GetLocationDetailsForMobile(string locationName)
        {
            LocationModel result = CreateDummyLocationModel().Where(entity => entity.SelectedLocation.LocationName.ToUpper() == locationName.ToUpper()).ToList().FirstOrDefault();
            return result;
        }
        private LocationModel GetLocationDetailsForTab(string locationName)
        {
            LocationModel result = CreateDummyLocationModel().Where(entity => entity.SelectedLocation.LocationName.ToUpper() == locationName.ToUpper()).ToList().FirstOrDefault();
            return result;
        }
        private LocationModel GetLocationDetails(string locationName)
        {
            LocationModel results = CreateDummyLocationModel().Where(entity => entity.SelectedLocation.LocationName.ToUpper() == locationName.ToUpper()).ToList().FirstOrDefault();
            return results;
        }
        private List<LocationModel> CreateDummyLocationModel()
        {
            List<LocationModel> results = new List<LocationModel>();
            LocationModel model1 = new LocationModel();            
            model1.AllActivities = new List<ActivityCategoryModel>();
            model1.SelectedLocation = new LocationsMaster() { LocationKey = "Gangtok", LocationImage = "gangtok", LocationName = "Gangtok", Country = "INDIA" };            
            model1.SelectedLocation.Season1Start = "Mar";
            model1.SelectedLocation.Season1End = "Jun";
            model1.SelectedLocation.Season2Start = "Sept";
            model1.SelectedLocation.Season2End = "Nov";
            model1.BestMonthsToVisit = new List<string>();
            if (model1.SelectedLocation.Season1Start == "Jan" && model1.SelectedLocation.Season1End == "Dec")
            {
                model1.BestMonthsToVisit.Add("All Year Round");
            }
            else
            {
                model1.BestMonthsToVisit.Add("Mar");
                model1.BestMonthsToVisit.Add("Apr");
                model1.BestMonthsToVisit.Add("May");
                model1.BestMonthsToVisit.Add("Jun");
                model1.BestMonthsToVisit.Add("Sept");
                model1.BestMonthsToVisit.Add("Oct");
                model1.BestMonthsToVisit.Add("Nov");
            }
            ActivityCategoryModel ac1 = new ActivityCategoryModel() { ActivityName = "Adventure", ActivityCount = 4, ImageURL = "Adventure" };
            ActivityCategoryModel ac2 = new ActivityCategoryModel() { ActivityName = "Ecotourism", ActivityCount = 2, ImageURL = "EcoTourism" };
            ActivityCategoryModel ac3 = new ActivityCategoryModel() { ActivityName = "Entertainment", ActivityCount = 6, ImageURL = "Entertainment" };
            ActivityCategoryModel ac4 = new ActivityCategoryModel() { ActivityName = "Events&Festivals", ActivityCount = 6, ImageURL = "Entertainment" };
            ActivityCategoryModel ac5 = new ActivityCategoryModel() { ActivityName = "Food & Drinks", ActivityCount = 3, ImageURL = "FoodDrinks" };
            ActivityCategoryModel ac6 = new ActivityCategoryModel() { ActivityName = "Adventure", ActivityCount = 4, ImageURL = "Adventure" };
            ActivityCategoryModel ac7 = new ActivityCategoryModel() { ActivityName = "Ecotourism", ActivityCount = 2, ImageURL = "EcoTourism" };
            ActivityCategoryModel ac8 = new ActivityCategoryModel() { ActivityName = "Entertainment", ActivityCount = 6, ImageURL = "Entertainment" };
            ActivityCategoryModel ac9 = new ActivityCategoryModel() { ActivityName = "Events&Festivals", ActivityCount = 6, ImageURL = "Entertainment" };
            ActivityCategoryModel ac10 = new ActivityCategoryModel() { ActivityName = "Food & Drinks", ActivityCount = 3, ImageURL = "FoodDrinks" };
            ActivityCategoryModel ac11 = new ActivityCategoryModel() { ActivityName = "Adventure", ActivityCount = 4, ImageURL = "Adventure" };
            ActivityCategoryModel ac12 = new ActivityCategoryModel() { ActivityName = "Ecotourism", ActivityCount = 2, ImageURL = "EcoTourism" };
            ActivityCategoryModel ac13 = new ActivityCategoryModel() { ActivityName = "Entertainment", ActivityCount = 6, ImageURL = "Entertainment" };
            ActivityCategoryModel ac14 = new ActivityCategoryModel() { ActivityName = "Events&Festivals", ActivityCount = 6, ImageURL = "Entertainment" };            
            model1.AllActivities.Add(ac1);
            model1.AllActivities.Add(ac2);
            model1.AllActivities.Add(ac3);
            model1.AllActivities.Add(ac4);
            model1.AllActivities.Add(ac5);
            model1.AllActivities.Add(ac6);
            model1.AllActivities.Add(ac7);
            model1.AllActivities.Add(ac8);
            model1.AllActivities.Add(ac9);
            model1.AllActivities.Add(ac10);
            model1.AllActivities.Add(ac11);
            model1.AllActivities.Add(ac12);
            model1.AllActivities.Add(ac13);
            model1.AllActivities.Add(ac14);
            results.Add(model1);
            return results;
        }

        private IEnumerable<ActivitiesModel> GetDummyData(string userAgent)
        {
            List<ActivitiesModel> results = new List<ActivitiesModel>();
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);

            ActivitiesModel activityOne = new ActivitiesModel();
            activityOne.Activity = new ActivitiesMaster();
            activityOne.DefaultActivityImage = new ActivityImages();

            ((ActivitiesMaster)activityOne.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)activityOne.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)activityOne.Activity).AverageUserRating = Convert.ToDecimal(3.5);
            ((ActivitiesMaster)activityOne.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityOne.Activity).Address = "Gangtok,Sikkim";
            ((ActivitiesMaster)activityOne.Activity).Cost = 1000;
            ((ActivityImages)activityOne.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activityOne.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";




            ActivitiesModel activityTwo = new ActivitiesModel();
            activityTwo.Activity = new ActivitiesMaster();
            activityTwo.DefaultActivityImage = new ActivityImages();

            ((ActivitiesMaster)activityTwo.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)activityTwo.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)activityTwo.Activity).AverageUserRating = 3;
            ((ActivitiesMaster)activityTwo.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityTwo.Activity).Address = "Rumtek,Sikkim";
            ((ActivitiesMaster)activityTwo.Activity).Cost = 2000;
            ((ActivityImages)activityTwo.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activityTwo.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";



            ActivitiesModel activityThree = new ActivitiesModel();
            activityThree.Activity = new ActivitiesMaster();
            activityThree.DefaultActivityImage = new ActivityImages();

            ((ActivitiesMaster)activityThree.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)activityThree.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)activityThree.Activity).AverageUserRating = Convert.ToDecimal(4.5);
            ((ActivitiesMaster)activityThree.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityThree.Activity).Address = "Melli,Sikkim";
            ((ActivitiesMaster)activityThree.Activity).Cost = 3000;
            ((ActivityImages)activityThree.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activityThree.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";

            ActivitiesModel activityFour = new ActivitiesModel();
            activityFour.Activity = new ActivitiesMaster();
            activityFour.DefaultActivityImage = new ActivityImages();

            ((ActivitiesMaster)activityFour.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)activityFour.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)activityFour.Activity).AverageUserRating = 4;
            ((ActivitiesMaster)activityFour.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityFour.Activity).Address = "Gangtok,Sikkim";
            ((ActivitiesMaster)activityFour.Activity).Cost = 1000;
            ((ActivityImages)activityFour.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activityFour.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";




            ActivitiesModel activityFive = new ActivitiesModel();
            activityFive.Activity = new ActivitiesMaster();
            activityFive.DefaultActivityImage = new ActivityImages();

            ((ActivitiesMaster)activityFive.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)activityFive.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)activityFive.Activity).AverageUserRating = 3;
            ((ActivitiesMaster)activityFive.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityFive.Activity).Address = "Rumtek,Sikkim";
            ((ActivitiesMaster)activityFive.Activity).Cost = 2000;
            ((ActivityImages)activityFive.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activityFive.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";



            ActivitiesModel activitySix = new ActivitiesModel();
            activitySix.Activity = new ActivitiesMaster();
            activitySix.DefaultActivityImage = new ActivityImages();

            ((ActivitiesMaster)activitySix.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)activitySix.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)activitySix.Activity).AverageUserRating = Convert.ToDecimal(4.5);
            ((ActivitiesMaster)activitySix.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activitySix.Activity).Address = "Melli,Sikkim";
            ((ActivitiesMaster)activitySix.Activity).Cost = 3000;
            ((ActivityImages)activitySix.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activitySix.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";

            ((ActivityImages)activityOne.DefaultActivityImage).ImageURL = "Images/khukuri1";
            ((ActivityImages)activityTwo.DefaultActivityImage).ImageURL = "Images/maruni1";
            ((ActivityImages)activityThree.DefaultActivityImage).ImageURL = "Images/buttermilk2";
            ((ActivityImages)activityFour.DefaultActivityImage).ImageURL = "Images/village2";
            ((ActivityImages)activityFive.DefaultActivityImage).ImageURL = "Images/village1";
            ((ActivityImages)activitySix.DefaultActivityImage).ImageURL = "Images/village3";

            results.Add(activityOne);
            results.Add(activityTwo);
            results.Add(activityThree);
            results.Add(activityFour);
            results.Add(activityFive);
            results.Add(activitySix);
            return results;
        }
        private IEnumerable<TopOffersModel> GetDummyTopOffers()
        {
            List<TopOffersModel> result = new List<TopOffersModel>();
            TopOffersModel offers1 = new TopOffersModel();
            TopOffers topOffer1 = new TopOffers();
            topOffer1.TopOffersKey = "TOPOFFER1";
            topOffer1.Discount = 10;
            topOffer1.ImageUrl = "Images/khukuri2";
            offers1.Offer = topOffer1;
            offers1.Activity = new ActivitiesMaster();
            ((ActivitiesMaster)offers1.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)offers1.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)offers1.Activity).AverageUserRating = Convert.ToDecimal(3.5);
            ((ActivitiesMaster)offers1.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)offers1.Activity).Address = "LOREM IPSUM";
            ((ActivitiesMaster)offers1.Activity).Cost = 1000;
            offers1.DiscountedPrice = 800;
            result.Add(offers1);

            TopOffersModel offers2 = new TopOffersModel();
            TopOffers topOffer2 = new TopOffers();
            topOffer2.TopOffersKey = "TOPOFFER1";
            topOffer2.Discount = 10;
            topOffer2.ImageUrl = "Images/khukuri1";
            offers2.Offer = topOffer2;
            offers2.Activity = new ActivitiesMaster();
            ((ActivitiesMaster)offers2.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)offers2.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)offers2.Activity).AverageUserRating = 3;
            ((ActivitiesMaster)offers2.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)offers2.Activity).Address = "LOREM IPSUM";
            ((ActivitiesMaster)offers2.Activity).Cost = 1000;
            offers2.DiscountedPrice = 800;
            result.Add(offers2);

            TopOffersModel offers3 = new TopOffersModel();
            TopOffers topOffer3 = new TopOffers();
            topOffer3.TopOffersKey = "TOPOFFER1";
            topOffer3.Discount = 10;
            topOffer3.ImageUrl = "Images/maruni1";
            offers3.Offer = topOffer3;
            offers3.Activity = new ActivitiesMaster();
            ((ActivitiesMaster)offers3.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)offers3.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)offers3.Activity).AverageUserRating = Convert.ToDecimal(4.5);
            ((ActivitiesMaster)offers3.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)offers3.Activity).Address = "LOREM IPSUM";
            ((ActivitiesMaster)offers3.Activity).Cost = 1000;
            offers3.DiscountedPrice = 800;
            result.Add(offers3);

            TopOffersModel offers5 = new TopOffersModel();
            TopOffers topOffer5 = new TopOffers();
            topOffer5.TopOffersKey = "TOPOFFER1";
            topOffer5.Discount = 10;
            topOffer5.ImageUrl = "Images/maruni2";
            offers5.Offer = topOffer5;
            offers5.Activity = new ActivitiesMaster();
            ((ActivitiesMaster)offers5.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)offers5.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)offers5.Activity).AverageUserRating = Convert.ToDecimal(4.5);
            ((ActivitiesMaster)offers5.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)offers5.Activity).Address = "LOREM IPSUM";
            ((ActivitiesMaster)offers5.Activity).Cost = 1000;
            offers5.DiscountedPrice = 800;
            result.Add(offers5);

            TopOffersModel offers4 = new TopOffersModel();
            TopOffers topOffer4 = new TopOffers();
            topOffer4.TopOffersKey = "TOPOFFER1";
            topOffer4.Discount = 10;
            topOffer4.ImageUrl = "Images/village2";
            offers4.Offer = topOffer4;
            offers4.Activity = new ActivitiesMaster();
            ((ActivitiesMaster)offers4.Activity).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)offers4.Activity).Name = "LOREM IPSUM";
            ((ActivitiesMaster)offers4.Activity).AverageUserRating = 3;
            ((ActivitiesMaster)offers4.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)offers4.Activity).Address = "LOREM IPSUM";
            ((ActivitiesMaster)offers4.Activity).Cost = 1000;
            offers4.DiscountedPrice = 800;
            result.Add(offers4);

            return result;
        }
    }
}