using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityTimeSchedulerRepository : DataRepositoryBase<ActivityTimeScheduler>, IActivityTimeSchedulerRepository
    {
        protected override ActivityTimeScheduler AddEntity(MyMonkeyCapContext entityContext, ActivityTimeScheduler entity)
        {
            return entityContext.ActivityTimeSchedulerSet.Add(entity);
        }

        protected override ActivityTimeScheduler UpdateEntity(MyMonkeyCapContext entityContext, ActivityTimeScheduler entity)
        {
            return (from e in entityContext.ActivityTimeSchedulerSet
                    where e.ActivityTimeSchedulerKey == entity.ActivityTimeSchedulerKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityTimeScheduler> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityTimeSchedulerSet
                    select e);
        }

        protected override ActivityTimeScheduler GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityTimeSchedulerSet
                         where e.ActivityTimeSchedulerKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
