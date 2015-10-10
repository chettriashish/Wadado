using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityLocationRepository : DataRepositoryBase<ActivityLocation>, IActivityLocationRepository
    {
        protected override ActivityLocation AddEntity(MyMonkeyCapContext entityContext, ActivityLocation entity)
        {
            return entityContext.ActivityLocationSet.Add(entity);
        }

        protected override ActivityLocation UpdateEntity(MyMonkeyCapContext entityContext, ActivityLocation entity)
        {
            return (from e in entityContext.ActivityLocationSet
                    where e.ActivityLocationKey == entity.ActivityLocationKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityLocation> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityLocationSet
                    select e);
        }

        protected override ActivityLocation GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityLocationSet
                         where e.ActivityLocationKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
