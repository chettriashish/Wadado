using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMC.Web.Model;
using MMC.Client.Entities;

namespace MMC.Web.Contracts
{
    public interface ISearchDataService
    {
        IEnumerable<LocationsMaster> GetAllLocations();
        IEnumerable<ActivitiesMaster> GetAllActivitiesForLocation(string userAgent, string locationKey);
    }
}
