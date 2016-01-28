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

            LocationsMaster location10 = new LocationsMaster();
            location10.LocationKey = Guid.NewGuid().ToString();
            location10.LocationName = "Koh Phi Phi";
            location10.Country = "THAILAND";
            results.Add(location10);
            LocationsMaster location11 = new LocationsMaster();
            location11.LocationKey = Guid.NewGuid().ToString();
            location11.LocationName = "Koh Phanyang";
            location11.Country = "THAILAND";
            results.Add(location11);
            LocationsMaster location12 = new LocationsMaster();
            location12.LocationKey = Guid.NewGuid().ToString();
            location12.LocationName = "Chitwan";
            location12.Country = "NEPAL";
            results.Add(location12);
            LocationsMaster location13 = new LocationsMaster();
            location13.LocationKey = Guid.NewGuid().ToString();
            location13.LocationName = "Koh Samui";
            location13.Country = "THAILAND";
            results.Add(location13);

            LocationsMaster location14 = new LocationsMaster();
            location14.LocationKey = Guid.NewGuid().ToString();
            location14.LocationName = "Koh Tao";
            location14.Country = "THAILAND";
            results.Add(location14);

            LocationsMaster location15 = new LocationsMaster();
            location15.LocationKey = Guid.NewGuid().ToString();
            location15.LocationName = "Bangkok";
            location15.Country = "THAILAND";
            results.Add(location15);

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
            ((ActivitiesMaster)activityOne).Name = "Enjoy rappeling at Ranka";
            ((ActivitiesMaster)activityOne).DifficultyRating = 1;
            ((ActivitiesMaster)activityOne).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityOne).Address = "Gangtok,Sikkim";
            ((ActivitiesMaster)activityOne).Cost = 1000;
            ((ActivitiesMaster)activityOne).ActivityTypeKey = "ADVENTURE";            
            results.Add(activityOne);

            ActivitiesMaster activityTwo = new ActivitiesMaster();            
            ((ActivitiesMaster)activityTwo).ActivitesKey = "ACTIVITY TWO";
            ((ActivitiesMaster)activityTwo).Name = "Paraglide @ 10000 feet from Bulbuley";
            ((ActivitiesMaster)activityTwo).DifficultyRating = 2;
            ((ActivitiesMaster)activityTwo).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityTwo).Address = "Rumtek,Sikkim";
            ((ActivitiesMaster)activityTwo).Cost = 2000;
            ((ActivitiesMaster)activityTwo).ActivityTypeKey = "ADVENTURE";

            ActivitiesMaster activityThree = new ActivitiesMaster();
            ((ActivitiesMaster)activityThree).ActivitesKey = "ACTIVITY THREE";
            ((ActivitiesMaster)activityThree).Name = "Explore a traditional Sikkimese Lepcha village";
            ((ActivitiesMaster)activityThree).DifficultyRating = 1;
            ((ActivitiesMaster)activityThree).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityThree).Address = "Gangtok,Sikkim";
            ((ActivitiesMaster)activityThree).Cost = 1000;
            ((ActivitiesMaster)activityThree).ActivityTypeKey = "ECOTOURISM";
            results.Add(activityThree);

            ActivitiesMaster activityFour = new ActivitiesMaster();
            ((ActivitiesMaster)activityFour).ActivitesKey = "ACTIVITY FOUR";
            ((ActivitiesMaster)activityFour).Name = "Traditional Sikkimese Archery";
            ((ActivitiesMaster)activityFour).DifficultyRating = 4;
            ((ActivitiesMaster)activityFour).Included = "A LOT OF THINGS";
            ((ActivitiesMaster)activityFour).Address = "Rumtek,Sikkim";
            ((ActivitiesMaster)activityFour).Cost = 2000;
            ((ActivitiesMaster)activityFour).ActivityTypeKey = "SPORTS AND GAMES";
            results.Add(activityFour);       

            return results;
        }
    }
}