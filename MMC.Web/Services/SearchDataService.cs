using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Services
{
    public class SearchDataService:ISearchDataService
    {
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

        public IEnumerable<ActivitiesMaster> GetAllActivitiesForLocation(string userAgent, string locationKey)
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            IEnumerable<ActivitiesMaster> results = GetAllActivitiesForSelectedLocation(locationKey);           
            return results;
        }

        public IEnumerable<ActivitiesMaster> GetAllActivitiesForSelectedLocation(string locationKey)
        {
            List<ActivitiesMaster> results = new List<ActivitiesMaster>();
            ActivitiesMaster activityOne = new ActivitiesMaster();            
            ((ActivitiesMaster)activityOne).ActivitesKey = "ACTIVITY ONE";
            ((ActivitiesMaster)activityOne).Name = "Rope Course Activity at Ranka";
            ((ActivitiesMaster)activityOne).DifficultyRating = 1;
            ((ActivitiesMaster)activityOne).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityOne).Address = "Gangtok,Sikkim";
            ((ActivitiesMaster)activityOne).Cost = 1000;
            ((ActivitiesMaster)activityOne).ActivityTypeKey = "ADVENTURE";            
            results.Add(activityOne);

            ActivitiesMaster activityTwo = new ActivitiesMaster();            
            ((ActivitiesMaster)activityTwo).ActivitesKey = "ACTIVITY TWO";
            ((ActivitiesMaster)activityTwo).Name = "Paragliding from Ranka";
            ((ActivitiesMaster)activityTwo).DifficultyRating = 2;
            ((ActivitiesMaster)activityTwo).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityTwo).Address = "Rumtek,Sikkim";
            ((ActivitiesMaster)activityTwo).Cost = 2000;
            ((ActivitiesMaster)activityOne).ActivityTypeKey = "ADVENTURE";
            results.Add(activityTwo);       

            return results;
        }
    }
}