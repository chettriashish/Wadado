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
    }
}
