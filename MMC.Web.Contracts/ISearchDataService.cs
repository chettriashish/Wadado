using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMC.Web.Model;
using MMC.Client.Entities;
using MMC.Client.Contracts.DataContracts;

namespace MMC.Web.Contracts
{
    public interface ISearchDataService
    {
        IEnumerable<LocationsMaster> GetAllLocations();
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesForLocation(string userAgent, string locationKey);
    }
}
