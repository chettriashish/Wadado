using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class LocationDetailsRepository : DataRepositoryBase<LocationDetails>, ILocationDetailsRepository
    {
        protected override LocationDetails AddEntity(MyMonkeyCapContext entityContext, LocationDetails entity)
        {
            return entityContext.LocationDetailsSet.Add(entity);
        }

        protected override LocationDetails UpdateEntity(MyMonkeyCapContext entityContext, LocationDetails entity)
        {
            return (from e in entityContext.LocationDetailsSet
                    where e.LocationDetailsKey == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LocationDetails> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.LocationDetailsSet
                    where e.IsDeleted == false
                    select e);
        }

        protected override LocationDetails GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.LocationDetailsSet
                         where e.LocationDetailsKey == key
                         && e.IsDeleted == false
                         select e);
            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<LocationDetails> GetAllDetailsForSelectedLocation(string locationKey)
        {
            List<LocationDetails> result = new List<LocationDetails>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                result = (from e in entityContext.LocationDetailsSet
                          where e.LocationKey == locationKey
                          && e.IsDeleted == false
                          select e).ToList();
            }
            return result;
        }
    }
}
