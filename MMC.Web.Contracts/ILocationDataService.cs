using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Contracts
{
    public interface ILocationDataService
    {
        LocationDetailsDataContract GetAllActivitiesForSelectedLocation(string locationName, string userAgent);
        IEnumerable<LocationsMaster> GetAllLocations();
        IEnumerable<ActivitySummaryDataContract> GetTopTrendingActivities(string locationName);
    }
}
