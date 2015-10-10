using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityImagesRepository : DataRepositoryBase<ActivityImages>, IActivityImagesRepository
    {
        protected override ActivityImages AddEntity(MyMonkeyCapContext entityContext, ActivityImages entity)
        {
            return entityContext.ActivityImagesSet.Add(entity);
        }

        protected override ActivityImages UpdateEntity(MyMonkeyCapContext entityContext, ActivityImages entity)
        {
            return (from e in entityContext.ActivityImagesSet
                    where e.ActivityImageKey == entity.ActivityImageKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityImages> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityImagesSet
                    select e);
        }

        protected override ActivityImages GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityImagesSet
                         where e.ActivityImageKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }        
    }
}
