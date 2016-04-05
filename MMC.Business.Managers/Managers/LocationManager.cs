using Core.Common.Contracts;
using Core.Common.Core;
using MMC.Business.Contracts;
using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using MMC.Common;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Managers
{
    /// <summary>
    /// Setting InstanceMode to per call so that the service proxy does not remain
    /// open for the application lifecycle. 
    /// Setting the Concurrency mode to Multiple so that multiple service calls are 
    /// handled concurrently and not one at a time.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class LocationManager : ManagerBase, ILocationService
    {
        const string ACTIVITY = "Activity";
        public LocationManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            ObjectBase.Container = Bootstrapper.Bootstrapper.Initialise();
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public void CreateNewLocation(LocationDetailsDataContract locationDetails)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ILocationsMasterRepository locationsMasterRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                ILocationDetailsRepository locationDetailsRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationDetailsRepository>();

                LocationsMaster newLocation = new LocationsMaster() { LocationKey = Guid.NewGuid().ToString() };
                newLocation.LocationName = locationDetails.LocationName;
                newLocation.LatLng = locationDetails.LatLong;
                newLocation.LocationImage = locationDetails.ImageURL;
                locationsMasterRepository.Add(newLocation);

                for (int count = 0; count < 4; count++)
                {
                    LocationDetails locationDetail = new LocationDetails() { LocationDetailsKey = Guid.NewGuid().ToString(), LocationKey = newLocation.LocationKey };
                    locationDetail.IsDeleted = false;
                    switch (count)
                    {
                        case 0:
                            locationDetail.ActivityDescription = locationDetails.Description;
                            locationDetail.ActivityHeader = BusinessResource.DESCRIPTION; break;

                        case 1:
                            locationDetail.ActivityDescription = locationDetails.MustDrink;
                            locationDetail.ActivityHeader = BusinessResource.MUSTDRINK; break;

                        case 2:
                            locationDetail.ActivityDescription = locationDetails.MustEat;
                            locationDetail.ActivityHeader = BusinessResource.MUSTEAT; break;


                        case 3:
                            locationDetail.ActivityDescription = locationDetails.GettingAround;
                            locationDetail.ActivityHeader = BusinessResource.GETTINGAROUND; break;
                    }
                    locationDetailsRepository.Add(locationDetail);
                }
            });
        }
        public IEnumerable<LocationDetailsDataContract> GetSelectedLocationDetails(string locationKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<LocationDetailsDataContract> result = new List<LocationDetailsDataContract>();

                ILocationsMasterRepository locationsMasterRepository
                                                = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();

                ILocationDetailsRepository locationDetailsRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationDetailsRepository>();

                LocationsMaster selectedLocation = locationsMasterRepository.Get(locationKey);
                IEnumerable<LocationDetails> allDetailsForSelectedLocation = locationDetailsRepository.GetAllDetailsForSelectedLocation(locationKey);

                if (allDetailsForSelectedLocation.Count() > 0)
                {
                    LocationDetailsDataContract locationDetail = new LocationDetailsDataContract() { LocationKey = locationKey, LocationName = selectedLocation.LocationName };
                    foreach (var detail in allDetailsForSelectedLocation)
                    {
                        locationDetail.LatLong = selectedLocation.LatLng;
                        if (detail.ActivityHeader == BusinessResource.DESCRIPTION)
                        {
                            locationDetail.Description = detail.ActivityDescription;
                        }
                        else if (detail.ActivityHeader == BusinessResource.GETTINGAROUND)
                        {
                            locationDetail.GettingAround = detail.ActivityDescription;
                        }
                        else if (detail.ActivityHeader == BusinessResource.MUSTDRINK)
                        {
                            locationDetail.MustDrink = detail.ActivityDescription;
                        }
                        else if (detail.ActivityHeader == BusinessResource.MUSTEAT)
                        {
                            locationDetail.MustEat = detail.ActivityDescription;
                        }
                    }
                    result.Add(locationDetail);
                }
                else
                {
                    LocationDetailsDataContract locationDetail = new LocationDetailsDataContract() { LocationKey = locationKey, LocationName = selectedLocation.LocationName };
                    locationDetail.LatLong = selectedLocation.LatLng;
                    locationDetail.ImageURL = selectedLocation.LocationImage;
                    result.Add(locationDetail);
                }

                return result;
            });
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateLocationDetails(LocationDetailsDataContract locationDetails)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ILocationsMasterRepository locationsMasterRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                ILocationDetailsRepository locationDetailsRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationDetailsRepository>();

                LocationsMaster location = locationsMasterRepository.Get(locationDetails.LocationKey);
                location.LocationName = locationDetails.LocationName;
                location.LatLng = locationDetails.LatLong;
                location.LocationImage = locationDetails.ImageURL;
                locationsMasterRepository.Update(location);
                List<LocationDetails> allLocations = new List<LocationDetails>();

                allLocations = locationDetailsRepository.GetAllDetailsForSelectedLocation(locationDetails.LocationKey).ToList();

                if (allLocations.Count() > 0)
                {
                    foreach (var locationDetail in allLocations)
                    {
                        if (locationDetail.ActivityHeader == BusinessResource.DESCRIPTION)
                        {
                            locationDetail.ActivityDescription = locationDetails.Description;
                        }
                        else if (locationDetail.ActivityHeader == BusinessResource.GETTINGAROUND)
                        {
                            locationDetail.ActivityDescription = locationDetails.GettingAround;
                        }
                        else if (locationDetail.ActivityHeader == BusinessResource.MUSTDRINK)
                        {
                            locationDetail.ActivityDescription = locationDetails.MustDrink;
                        }
                        else if (locationDetail.ActivityHeader == BusinessResource.MUSTEAT)
                        {
                            locationDetail.ActivityDescription = locationDetails.MustEat;
                        }
                    }
                    locationDetailsRepository.UpdateAll(allLocations);
                }
                else
                {
                    for (int count = 0; count < 4; count++)
                    {
                        LocationDetails locationDetail = new LocationDetails() { LocationDetailsKey = Guid.NewGuid().ToString(), LocationKey = locationDetails.LocationKey };
                        locationDetail.IsDeleted = false;
                        switch (count)
                        {
                            case 0:
                                locationDetail.ActivityDescription = locationDetails.Description;
                                locationDetail.ActivityHeader = BusinessResource.DESCRIPTION; break;

                            case 1:
                                locationDetail.ActivityDescription = locationDetails.MustDrink;
                                locationDetail.ActivityHeader = BusinessResource.MUSTDRINK; break;

                            case 2:
                                locationDetail.ActivityDescription = locationDetails.MustEat;
                                locationDetail.ActivityHeader = BusinessResource.MUSTEAT; break;


                            case 3:
                                locationDetail.ActivityDescription = locationDetails.GettingAround;
                                locationDetail.ActivityHeader = BusinessResource.GETTINGAROUND; break;
                        }
                        locationDetailsRepository.Add(locationDetail);
                    }
                }
            });
        }
        public IEnumerable<LocationsMaster> GetAllLocations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ILocationsMasterRepository locationsMasterRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                return locationsMasterRepository.Get().OrderBy(entity => entity.LocationName);
            });
        }

        public IEnumerable<LocationDetailsDataContract> GetSelectedLocationDetailsForClientApplication(string locationKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<LocationDetailsDataContract> result = new List<LocationDetailsDataContract>();

                ILocationsMasterRepository locationsMasterRepository
                                                = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                ILocationDetailsRepository locationDetailsRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationDetailsRepository>();


                LocationsMaster selectedLocation = locationsMasterRepository.Get(locationKey);
                IEnumerable<LocationDetails> allDetailsForSelectedLocation = locationDetailsRepository.GetAllDetailsForSelectedLocation(locationKey);

                if (allDetailsForSelectedLocation.Count() > 0)
                {
                    LocationDetailsDataContract locationDetail = new LocationDetailsDataContract() { LocationKey = locationKey, LocationName = selectedLocation.LocationName };
                    foreach (var detail in allDetailsForSelectedLocation)
                    {
                        locationDetail.LatLong = selectedLocation.LatLng;
                        if (detail.ActivityHeader == BusinessResource.DESCRIPTION)
                        {
                            locationDetail.Description = detail.ActivityDescription;
                        }
                        else if (detail.ActivityHeader == BusinessResource.GETTINGAROUND)
                        {
                            locationDetail.GettingAround = detail.ActivityDescription;
                        }
                        else if (detail.ActivityHeader == BusinessResource.MUSTDRINK)
                        {
                            locationDetail.MustDrink = detail.ActivityDescription;
                        }
                        else if (detail.ActivityHeader == BusinessResource.MUSTEAT)
                        {
                            locationDetail.MustEat = detail.ActivityDescription;
                        }
                    }
                    locationDetail.LocationName = selectedLocation.LocationName;
                    locationDetail.LatLong = selectedLocation.LatLng;
                    locationDetail.ImageURL = selectedLocation.LocationImage;
                    result.Add(locationDetail);
                }
                else
                {
                    LocationDetailsDataContract locationDetail = new LocationDetailsDataContract() { LocationKey = locationKey, LocationName = selectedLocation.LocationName };
                    locationDetail.LocationName = selectedLocation.LocationName;
                    locationDetail.LatLong = selectedLocation.LatLng;
                    locationDetail.ImageURL = selectedLocation.LocationImage;
                    result.Add(locationDetail);
                }
                if (userAgent != "smartphone" && userAgent != "tablet")
                {
                    result.FirstOrDefault().TopOffersForLocation = GetTopOffersForSelectedLocation(locationKey, result.FirstOrDefault().LocationName);
                    result.FirstOrDefault().DefaultActivityCategoryKey = "TOPTRENDING";
                }
                else
                {
                    //Get All activity categories
                    result.FirstOrDefault().AllActivities = GetAllActivityCategoriesForLocation(locationKey);
                }
                if (userAgent == "smartphone")
                {                  
                    foreach (var item in result.FirstOrDefault().AllActivities)
                    {
                        item.ImageURL = string.Format("Images/icons/{0}.png", item.ImageURL);
                    }
                    result.FirstOrDefault().ImageURL = string.Format("Images/mobile/{0}", result.FirstOrDefault().ImageURL);
                }
                else if (userAgent == "tablet")
                {
                    foreach (var item in result.FirstOrDefault().AllActivities)
                    {
                        item.ImageURL = string.Format("Images/icons/{0}.png", item.ImageURL);
                    }
                    result.FirstOrDefault().ImageURL = string.Format("Images/tablet/{0}", result.FirstOrDefault().ImageURL);
                }
                else
                {
                    result.FirstOrDefault().ImageURL = string.Format("Images/desktop/{0}", result.FirstOrDefault().ImageURL);
                }
                return result;
            });
        }

        private IEnumerable<TopOffersDataContract> GetTopOffersForSelectedLocation(string locationKey, string locationName)
        {
            ITopOffersRepository topOffersRepository = _DataRepositoryFactory.GetDataRepository<ITopOffersRepository>();
            ITopOfferMappingRepository topOffersMappingRepository = _DataRepositoryFactory.GetDataRepository<ITopOfferMappingRepository>();
            IActivitiesMasterRepository activityMasterrepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
            IEnumerable<TopOffers> allTopOffers = topOffersRepository.GetTopOffersForLocation(locationKey);
            IEnumerable<TopOfferMapping> allTopOffersMapping = topOffersMappingRepository.GetAllTopActivitiesOfferForSelectedLocation(locationKey);
            List<TopOffersDataContract> results = new List<TopOffersDataContract>();
            IActivityImagesRepository activityImageRepository = _DataRepositoryFactory.GetDataRepository<IActivityImagesRepository>();
            foreach (var topOffers in allTopOffers)
            {
                if (allTopOffersMapping.Any(e => e.TopOfferKey == topOffers.TopOffersKey))
                {
                    TopOfferMapping offerMapping = allTopOffersMapping.FirstOrDefault(e => e.TopOfferKey == topOffers.TopOffersKey);
                    ActivitiesMaster activity = activityMasterrepository.Get(offerMapping.MappingKey);
                    ActivityImages img = activityImageRepository.GetImagesForSelectedActivity(activity.ActivitesKey).FirstOrDefault(e => e.IsDefault == true);
                    decimal activityCost = Math.Ceiling(activity.Cost - ((offerMapping.Discount / 100) * activity.Cost));
                    TopOffersDataContract topOffer = new TopOffersDataContract
                    {
                        TopOffersKey = topOffers.TopOffersKey,
                        Discount = offerMapping.Discount,
                        Cost = activityCost,
                        ImageURL = string.Format("Images/{0}", topOffers.ImageUrl), //string.Format("Images/{0}", img.ImageURL),
                        Currency = activity.Currency,
                        Location = locationName,
                        Key = offerMapping.MappingType,
                        OfferType = ACTIVITY,
                        Value = activity.Name,
                        Rating = activity.AverageUserRating
                    };
                    results.Add(topOffer);
                }
            }

            return results;
        }

        /// <summary>
        /// This method returns hard 
        /// coded data. This will have to change
        /// to fetching data from the database.
        /// </summary>
        /// <param name="locationKey"></param>
        /// <returns></returns>
        private IEnumerable<ActivityCategoryDataContract> GetAllActivityCategoriesForLocation(string locationKey)
        {
            List<ActivityCategoryDataContract> result = new List<ActivityCategoryDataContract>();

            ActivityCategoryDataContract ac1 = new ActivityCategoryDataContract() { ActivityName = "Adventure", ActivityCount = 12, ImageURL = "Adventure", ActivityKey = "Adventure" };
            ActivityCategoryDataContract ac2 = new ActivityCategoryDataContract() { ActivityName = "Ecotourism", ActivityCount = 20, ImageURL = "EcoTourism", ActivityKey = "EcoTourism" };
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
