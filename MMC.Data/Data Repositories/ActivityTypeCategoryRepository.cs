using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityTypeCategoryRepository : DataRepositoryBase<ActivityTypeCategory>, IActivityTypeCategoryRepository
    {
        protected override ActivityTypeCategory AddEntity(MyMonkeyCapContext entityContext, ActivityTypeCategory entity)
        {
            return entityContext.ActivityTypeCategorySet.Add(entity);
        }

        protected override ActivityTypeCategory UpdateEntity(MyMonkeyCapContext entityContext, ActivityTypeCategory entity)
        {
            return (from e in entityContext.ActivityTypeCategorySet
                    where e.ActivityTypeCategoryKey == entity.ActivityTypeCategoryKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityTypeCategory> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityTypeCategorySet
                    select e);
        }

        protected override ActivityTypeCategory GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityTypeCategorySet
                         where e.ActivityTypeCategoryKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<ActivityTypeCategory> GetActivityTypeCategoryMapping(string activityTypeKey)
        {
            using (MyMonkeyCapContext context = new MyMonkeyCapContext())
            {
                return (from entity in context.ActivityTypeCategorySet
                        where entity.ActivityTypeKey == activityTypeKey
                        select entity).ToList();
            }
        }

        public IEnumerable<ActivityTypeCategory> GetActivityCategoryTypeMapping(string activityCategoryKey)
        {
            using (MyMonkeyCapContext context = new MyMonkeyCapContext())
            {
                return (from entity in context.ActivityTypeCategorySet
                        where entity.ActivityTypeCategoryKey == activityCategoryKey
                        select entity).ToList();
            }
        }        
    }
}
