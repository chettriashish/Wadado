using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Common.Contracts.ServiceContracts
{
    public interface IHomeService
    {
        IEnumerable<TopOffers> GetTopOffers();
        IEnumerable<ActivitiesMaster> GetTrendingActivities();
    }
}
