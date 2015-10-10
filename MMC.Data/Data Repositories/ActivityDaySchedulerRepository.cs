using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityDaySchedulerRepository : DataRepositoryBase<ActivityDayScheduler>, IActivityDaySchedulerRepository
    {
        protected override ActivityDayScheduler AddEntity(MyMonkeyCapContext entityContext, ActivityDayScheduler entity)
        {
            return entityContext.ActivityDaySchedulerSet.Add(entity);
        }

        protected override ActivityDayScheduler UpdateEntity(MyMonkeyCapContext entityContext, ActivityDayScheduler entity)
        {
            return (from e in entityContext.ActivityDaySchedulerSet
                    where e.ActivityDaySchedulerKey == entity.ActivityDaySchedulerKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityDayScheduler> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityDaySchedulerSet
                    select e);
        }

        protected override ActivityDayScheduler GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityDaySchedulerSet
                         where e.ActivityDaySchedulerKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
