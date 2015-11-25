using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMC.Web.Model;
using MMC.Client.Entities;

namespace MMC.Web.Contracts
{
    public interface IHomeDataService
    {
        IEnumerable<TopOffersModel> GetTopOffers(string userAgent);
        IEnumerable<ActivitiesModel> GetTrendingActivities(string userAgent);
        IEnumerable<LocationsMaster> GetAllLocations();
        IEnumerable<NewsModel> GetLastestNews();
    }
}
