using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class LocationsMasterRepository : DataRepositoryBase<LocationsMaster>, ILocationsMasterRepository
    {
        protected override LocationsMaster AddEntity(MyMonkeyCapContext entityContext, LocationsMaster entity)
        {
            return entityContext.LocationMasterSet.Add(entity);
        }

        protected override LocationsMaster UpdateEntity(MyMonkeyCapContext entityContext, LocationsMaster entity)
        {
            return (from e in entityContext.LocationMasterSet
                    where e.LocationKey == entity.LocationKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LocationsMaster> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.LocationMasterSet
                    select e);
        }

        protected override LocationsMaster GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.LocationMasterSet
                         where e.LocationKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
