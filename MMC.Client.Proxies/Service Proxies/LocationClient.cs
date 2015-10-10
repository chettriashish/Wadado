using Core.Common.ServiceModel;
using MMC.Client.Contracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMC.Client.Proxies.Proxies
{
    public class LocationClient : UserClientBase<ILocationService>, ILocationService
    {        
        public IEnumerable<LocationsMaster> GetAllLocations()
        {
            return Channel.GetAllLocations();
        }

        public void CreateNewLocation(LocationsMaster location)
        {
            Channel.CreateNewLocation(location);
        }
    }
}
