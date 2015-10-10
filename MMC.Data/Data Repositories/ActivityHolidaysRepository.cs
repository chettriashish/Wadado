using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityHolidaysRepository : DataRepositoryBase<ActivityHolidays>, IActivityHolidaysRepository
    {
        protected override ActivityHolidays AddEntity(MyMonkeyCapContext entityContext, ActivityHolidays entity)
        {
            return entityContext.ActivityHolidaysSet.Add(entity);
        }

        protected override ActivityHolidays UpdateEntity(MyMonkeyCapContext entityContext, ActivityHolidays entity)
        {
            return (from e in entityContext.ActivityHolidaysSet
                    where e.ActivityHolidayKey == entity.ActivityHolidayKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityHolidays> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityHolidaysSet
                    select e);
        }

        protected override ActivityHolidays GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityHolidaysSet
                         where e.ActivityHolidayKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
