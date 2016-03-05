using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityPriceMappingRepository : DataRepositoryBase<ActivityPriceMapping>, IActivityPriceMappingRepository
    {
        protected override ActivityPriceMapping AddEntity(MyMonkeyCapContext entityContext, ActivityPriceMapping entity)
        {
            return entityContext.ActivityPriceMappingSet.Add(entity);
        }

        protected override ActivityPriceMapping UpdateEntity(MyMonkeyCapContext entityContext, ActivityPriceMapping entity)
        {
            return (from e in entityContext.ActivityPriceMappingSet
                    where e.ActivityPricingKey == entity.ActivityPricingKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityPriceMapping> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityPriceMappingSet
                    select e);
        }

        protected override ActivityPriceMapping GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityPriceMappingSet
                         where e.ActivityPricingKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ActivityPriceMapping> GetAllPriceOptionsForSelectedActivity(string activityKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityPriceMappingSet
                             where e.ActivityKey == activityKey
                             select e);
                var results = query.ToList();
                return results;
            }            
        }
    }
}
