using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class TouristSeasonsRepository : DataRepositoryBase<TouristSeasons>, ITouristSeasonsRepository
    {
        protected override TouristSeasons AddEntity(MyMonkeyCapContext entityContext, TouristSeasons entity)
        {
            return entityContext.TouristSeasonsSet.Add(entity);
        }

        protected override TouristSeasons UpdateEntity(MyMonkeyCapContext entityContext, TouristSeasons entity)
        {
            return (from e in entityContext.TouristSeasonsSet
                    where e.TouristSeasonKey == entity.TouristSeasonKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TouristSeasons> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.TouristSeasonsSet
                    select e);
        }

        protected override TouristSeasons GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.TouristSeasonsSet
                         where e.TouristSeasonKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
