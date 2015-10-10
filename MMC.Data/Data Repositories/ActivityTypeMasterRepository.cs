using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityTypeMasterRepository : DataRepositoryBase<ActivityTypeMaster>, IActivityTypeMasterRepository
    {
        protected override ActivityTypeMaster AddEntity(MyMonkeyCapContext entityContext, ActivityTypeMaster entity)
        {
            return entityContext.ActivityTypeMasterSet.Add(entity);
        }

        protected override ActivityTypeMaster UpdateEntity(MyMonkeyCapContext entityContext, ActivityTypeMaster entity)
        {
            return (from e in entityContext.ActivityTypeMasterSet
                    where e.ActivityTypeKey == entity.ActivityTypeKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityTypeMaster> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityTypeMasterSet
                    select e);
        }

        protected override ActivityTypeMaster GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityTypeMasterSet
                         where e.ActivityTypeKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
