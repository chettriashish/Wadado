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

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesForLocation(string userAgent, string locationKey)
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(userAgent);
            IEnumerable<ActivitySummaryDataContract> results = GetAllActivitiesForSelectedLocation(locationKey);           
            return results;
        }

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesForSelectedLocation(string locationKey)
        {
            List<ActivitySummaryDataContract> results = new List<ActivitySummaryDataContract>();
            ActivitySummaryDataContract activityOne = new ActivitySummaryDataContract();            
            ((ActivitySummaryDataContract)activityOne).ActivityCategory = "ADVENTURE";
            ((ActivitySummaryDataContract)activityOne).ActivityCategoryKey = "ADVENTURE";
            results.Add(activityOne);            

            ActivitySummaryDataContract activityThree = new ActivitySummaryDataContract();
            ((ActivitySummaryDataContract)activityThree).ActivityCategory = "ECO-TOURISM";
            ((ActivitySummaryDataContract)activityThree).ActivityCategoryKey = "ECOTOURISM";
            results.Add(activityThree);

            ActivitySummaryDataContract activityFour = new ActivitySummaryDataContract();
            ((ActivitySummaryDataContract)activityFour).ActivityCategoryKey = "SPORTSANDGAMES";
            ((ActivitySummaryDataContract)activityFour).ActivityCategory = "SPORTS AND GAMES";
            results.Add(activityFour);

            ActivitySummaryDataContract activityFive = new ActivitySummaryDataContract();
            ((ActivitySummaryDataContract)activityFive).ActivityCategoryKey = "TOPTRENDING";
            ((ActivitySummaryDataContract)activityFive).ActivityCategory = "TOP TRENDING ACTIVITIES";
            results.Add(activityFive);       

            return results;
        }
    }
}