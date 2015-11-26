using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Data_Repositories
{
    public class ActivityCategoryMasterRepository : DataRepositoryBase<ActivityCategoryMaster>, IActivityCategoryMasterRepository
    {
        protected override ActivityCategoryMaster AddEntity(MyMonkeyCapContext entityContext, ActivityCategoryMaster entity)
        {
            return entityContext.ActivityCategoryMasterSet.Add(entity);
        }

        protected override ActivityCategoryMaster UpdateEntity(MyMonkeyCapContext entityContext, ActivityCategoryMaster entity)
        {
            return (from e in entityContext.ActivityCategoryMasterSet
                    where e.ActivityCategoryKey == entity.ActivityCategoryKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityCategoryMaster> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityCategoryMasterSet
                    select e);
        }

        protected override ActivityCategoryMaster GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityCategoryMasterSet
                         where e.ActivityCategoryKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
