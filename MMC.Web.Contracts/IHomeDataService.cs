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
    public interface IHomeDataService
    {
        IEnumerable<TopOffersDataContract> GetTopOffers(string userAgent);
        IEnumerable<ActivitySummaryDataContract> GetTrendingActivities(string userAgent);
        IEnumerable<LocationsMaster> GetAllLocations();
        IEnumerable<NewsModel> GetLastestNews();
    }
}
