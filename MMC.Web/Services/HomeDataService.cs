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
        public IEnumerable<TopOffersModel> GetTopOffers(string userAgent)
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            IEnumerable<TopOffersModel> results = GetDummyTopOffers();
            if (deviceInfo.GetVirtualCapability("is_mobile") == "true")
            {
                if (deviceInfo.GetVirtualCapability("is_smartphone") == "true")
                {
                    foreach (var item in results)
                    {
                        item.Offer.ImageUrl = string.Format("{0}{1}", item.Offer.ImageUrl, MOBILE);
                    }
                }
                else
                {
                    foreach (var item in results)
                    {
                        item.Offer.ImageUrl = string.Format("{0}{1}", item.Offer.ImageUrl, TABLET);
                    }
                }
            }
            else
            {
                ///Then the viewing device is a desktop
            }
            return results;
        }

        public IEnumerable<ActivitiesModel> GetTrendingActivities(string userAgent)
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            IEnumerable<ActivitiesModel> results = GetDummyData(userAgent);

            if (deviceInfo.GetVirtualCapability("is_mobile") == "true")
            {
                if (deviceInfo.GetVirtualCapability("is_smartphone") == "true")
                {
                    foreach (var item in results)
                    {
                        item.DefaultActivityImage.ImageURL = string.Format("{0}{1}", item.DefaultActivityImage.ImageURL, MOBILE);
                    }
                }
                else
                {                    
                    foreach (var item in results)
                    {
                        item.DefaultActivityImage.ImageURL = string.Format("{0}{1}", item.DefaultActivityImage.ImageURL, TABLET);                    
                    }
                }
            }
            else
            {
                ///Then the viewing device is a desktop
            }
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
            ((ActivitiesMaster)activityOne.Activity).DifficultyRating = 1;
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
            ((ActivitiesMaster)activityTwo.Activity).DifficultyRating = 2;
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
            ((ActivitiesMaster)activityThree.Activity).DifficultyRating = 3;
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
            ((ActivitiesMaster)activityFour.Activity).DifficultyRating = 1;
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
            ((ActivitiesMaster)activityFive.Activity).DifficultyRating = 2;
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
            ((ActivitiesMaster)activitySix.Activity).DifficultyRating = 3;
            ((ActivitiesMaster)activitySix.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activitySix.Activity).Address = "Melli,Sikkim";
            ((ActivitiesMaster)activitySix.Activity).Cost = 3000;
            ((ActivityImages)activitySix.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activitySix.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";

            if (deviceInfo.GetVirtualCapability("is_mobile") == "true")
            {
                ((ActivityImages)activityOne.DefaultActivityImage).ImageURL = "Images/khukuri1";
                ((ActivityImages)activityTwo.DefaultActivityImage).ImageURL = "Images/maruni1";
                ((ActivityImages)activityThree.DefaultActivityImage).ImageURL = "Images/buttermilk2";
                ((ActivityImages)activityFour.DefaultActivityImage).ImageURL = "Images/village2";
                ((ActivityImages)activityFive.DefaultActivityImage).ImageURL = "Images/village1";
                ((ActivityImages)activitySix.DefaultActivityImage).ImageURL = "Images/village3";
            }
           
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
            ((ActivitiesMaster)offers1.Activity).DifficultyRating = 1;
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
            ((ActivitiesMaster)offers2.Activity).DifficultyRating = 3;
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
            ((ActivitiesMaster)offers3.Activity).DifficultyRating = 3;
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
            ((ActivitiesMaster)offers5.Activity).DifficultyRating = 3;
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
            ((ActivitiesMaster)offers4.Activity).DifficultyRating = 3;
            ((ActivitiesMaster)offers4.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)offers4.Activity).Address = "LOREM IPSUM";
            ((ActivitiesMaster)offers4.Activity).Cost = 1000;
            offers4.DiscountedPrice = 800;
            result.Add(offers4);

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