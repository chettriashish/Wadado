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
                        item.ImageURL = string.Format("../Images/icons/{0}.png", item.ImageURL);
                    }                    
                    result.DefaultLocationImageURL = string.Format("../Images/mobile/{0}", result.SelectedLocation.LocationImage);
                }
                else
                {
                    
                }
            }
            else
            {
                
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
            return results;
        }
        private LocationModel GetLocationDetailsForMobile(string locationName)
        {
            LocationModel result = CreateDummyLocationModel().Where(entity => entity.SelectedLocation.LocationName.ToUpper() == locationName.ToUpper()).ToList().FirstOrDefault();
            return result;
        }
        private IEnumerable<LocationModel> GetLocationDetailsForTab(string locationName)
        {
            List<LocationModel> results = new List<LocationModel>();
            return results;
        }
        private IEnumerable<LocationModel> GetLocationDetails(string locationName)
        {
            List<LocationModel> results = new List<LocationModel>();
            return results;
        }

        private List<LocationModel> CreateDummyLocationModel()
        {
            List<LocationModel> results = new List<LocationModel>();
            LocationModel model1 = new LocationModel();            
            model1.AllActivities = new List<ActivityCategoryModel>();
            model1.SelectedLocation = new LocationsMaster() { LocationKey = "Darjeeling", LocationImage = "darjeeling", LocationName = "Darjeeling", Country = "INDIA" };            
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
    }
}