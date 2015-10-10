using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityRatesRepository : DataRepositoryBase<ActivityRates>, IActivityRatesRepository
    {
        protected override ActivityRates AddEntity(MyMonkeyCapContext entityContext, ActivityRates entity)
        {
            return entityContext.ActivityRatesSet.Add(entity);
        }

        protected override ActivityRates UpdateEntity(MyMonkeyCapContext entityContext, ActivityRates entity)
        {
            return (from e in entityContext.ActivityRatesSet
                    where e.ActivityRatesKey == entity.ActivityRatesKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityRates> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityRatesSet
                    select e);
        }

        protected override ActivityRates GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityRatesSet
                         where e.ActivityRatesKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
