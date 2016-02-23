using Core.Common.ServiceModel;
using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMC.Client.Proxies
{
    public class LocationClient : UserClientBase<ILocationService>, ILocationService
    {        
        public IEnumerable<LocationsMaster> GetAllLocations()
        {
            return Channel.GetAllLocations();
        }
        public void CreateNewLocation(LocationDetailsDataContract locationDetails)
        {
            Channel.CreateNewLocation(locationDetails);
        }

        public IEnumerable<LocationDetailsDataContract> GetSelectedLocationDetails(string locationKey)
        {
            return Channel.GetSelectedLocationDetails(locationKey);
        }

        public void UpdateLocationDetails(LocationDetailsDataContract locationDetails)
        {
            Channel.UpdateLocationDetails(locationDetails);
        }       
    }
}
