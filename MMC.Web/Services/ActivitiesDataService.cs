using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Services
{
    public class ActivitiesDataService : IActivitiesDataService
    {
        const string MOBILE = "_mobile";
        const string TABLET = "_tab";
        const string THUMBNAIL = "_thumb";
        public IEnumerable<ActivitiesModel> GetSelectedActivityType(string userAgent, string activityType)
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            IEnumerable<ActivitiesModel> results = GetSelectedActivityResults(activityType);
            if (deviceInfo.GetVirtualCapability("is_mobile") == "true")
            {
                if (deviceInfo.GetVirtualCapability("is_smartphone") == "true")
                {
                    foreach (var item in results)
                    {
                        item.DefaultActivityImage.ImageURL = string.Format("Images/{0}{1}{2}", item.DefaultActivityImage.ImageURL, THUMBNAIL, MOBILE);
                    }
                }
                else
                {
                    foreach (var item in results)
                    {
                        item.DefaultActivityImage.ImageURL = string.Format("Images/{0}{1}{2}", item.DefaultActivityImage.ImageURL, THUMBNAIL, TABLET);
                    }
                }
            }
            else
            {
                ///Then the viewing device is a desktop
            }
            return results;
        }

        public IEnumerable<ActivitiesModel> GetSelectedActivityResults(string activityType)
        {
            List<ActivitiesModel> results = new List<ActivitiesModel>();

            ActivitiesModel activityOne = new ActivitiesModel();
            activityOne.Activity = new ActivitiesMaster();
            activityOne.DefaultActivityImage = new ActivityImages();
            activityOne.ActivityType = activityType;
            ((ActivitiesMaster)activityOne.Activity).ActivitesKey = "MEDIUMFLY";
            ((ActivitiesMaster)activityOne.Activity).Name = "Rope Course Activity at Ranka";
            ((ActivitiesMaster)activityOne.Activity).DifficultyRating = 1;
            ((ActivitiesMaster)activityOne.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityOne.Activity).Address = "Gangtok,Sikkim";
            ((ActivitiesMaster)activityOne.Activity).Cost = 1000;
            ((ActivitiesMaster)activityOne.Activity).ActivityTypeKey = "ADVENTURE";
            ((ActivityImages)activityOne.DefaultActivityImage).ActivityKey = "ACTIVITY ONE";
            ((ActivityImages)activityOne.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";
            ((ActivityImages)activityOne.DefaultActivityImage).ImageURL = "ropesactivity";
            results.Add(activityOne);

            ActivitiesModel activityTwo = new ActivitiesModel();
            activityTwo.Activity = new ActivitiesMaster();
            activityTwo.DefaultActivityImage = new ActivityImages();
            activityTwo.ActivityType = activityType;
            ((ActivitiesMaster)activityTwo.Activity).ActivitesKey = "HIGHFLY";
            ((ActivitiesMaster)activityTwo.Activity).Name = "Paragliding from Ranka";
            ((ActivitiesMaster)activityTwo.Activity).DifficultyRating = 2;
            ((ActivitiesMaster)activityTwo.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityTwo.Activity).Address = "Rumtek,Sikkim";
            ((ActivitiesMaster)activityTwo.Activity).Cost = 2000;
            ((ActivityImages)activityTwo.DefaultActivityImage).ActivityKey = "ADVENTURE";
            ((ActivityImages)activityTwo.DefaultActivityImage).ActivityImageKey = "ACTIVITY ONE IMAGE";
            ((ActivityImages)activityTwo.DefaultActivityImage).ImageURL = "paragliding";
            results.Add(activityTwo);

            ActivitiesModel activityThree = new ActivitiesModel();
            activityThree.Activity = new ActivitiesMaster();
            activityThree.DefaultActivityImage = new ActivityImages();
            activityThree.ActivityType = activityType;
            ((ActivitiesMaster)activityThree.Activity).ActivitesKey = "HIGHFLY2";
            ((ActivitiesMaster)activityThree.Activity).Name = "Rock Climbing at Mt. Simvo ";
            ((ActivitiesMaster)activityThree.Activity).DifficultyRating = 3;
            ((ActivitiesMaster)activityThree.Activity).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityThree.Activity).Address = "Melli,Sikkim";
            ((ActivitiesMaster)activityThree.Activity).Cost = 3000;
            ((ActivityImages)activityThree.DefaultActivityImage).ActivityKey = "ADVENTURE";
            ((ActivityImages)activityThree.DefaultActivityImage).ActivityImageKey = "ACTIVITY TWO IMAGE";
            ((ActivityImages)activityThree.DefaultActivityImage).ImageURL = "rockclimbing";
            results.Add(activityThree);          

            return results;
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
    }
}