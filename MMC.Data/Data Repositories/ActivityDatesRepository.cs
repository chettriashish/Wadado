using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityDatesRepository : DataRepositoryBase<ActivityDates>, IActivityDatesRepository
    {
        public IEnumerable<ActivityDates> GetSelectedActivityDates(string activityKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                return (from e in entityContext.ActivityDatesSet
                        where e.ActivityKey == activityKey
                        && e.IsDeleted == false
                        select e).ToList();
            }
        }
        protected override ActivityDates AddEntity(MyMonkeyCapContext entityContext, ActivityDates entity)
        {
            return entityContext.ActivityDatesSet.Add(entity);
        }

        protected override ActivityDates UpdateEntity(MyMonkeyCapContext entityContext, ActivityDates entity)
        {
            return (from e in entityContext.ActivityDatesSet
                    where e.ActivityDatesKey == entity.ActivityDatesKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityDates> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityDatesSet
                    select e);
        }

        protected override ActivityDates GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityDatesSet
                         where e.ActivityDatesKey == key
                         select e);

            var results = query.FirstOrDefault();
            return results;
        }
    }
}
