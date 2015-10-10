using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityDiscountRepository : DataRepositoryBase<ActivityDiscount>, IActivityDiscountRepository
    {
        protected override ActivityDiscount AddEntity(MyMonkeyCapContext entityContext, ActivityDiscount entity)
        {
            return entityContext.ActivityDiscountSet.Add(entity);
        }

        protected override ActivityDiscount UpdateEntity(MyMonkeyCapContext entityContext, ActivityDiscount entity)
        {
            return (from e in entityContext.ActivityDiscountSet
                    where e.ActivityDiscountKey == entity.ActivityDiscountKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityDiscount> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityDiscountSet
                    select e);
        }

        protected override ActivityDiscount GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityDiscountSet
                         where e.ActivityDiscountKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
