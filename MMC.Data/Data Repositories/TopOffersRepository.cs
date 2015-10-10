using MMC.Business.Entities;
using MMC.Data.Contracts.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class TopOffersRepository : DataRepositoryBase<TopOffers>, ITopOffersRepository
    {

        protected override TopOffers AddEntity(MyMonkeyCapContext entityContext, TopOffers entity)
        {
            return entityContext.TopOffersSet.Add(entity);
        }

        protected override TopOffers UpdateEntity(MyMonkeyCapContext entityContext, TopOffers entity)
        {
            return (from e in entityContext.TopOffersSet
                    where e.TopOffersKey == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TopOffers> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.TopOffersSet
                    select e);
        }        

        protected override TopOffers GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.TopOffersSet
                         where e.TopOffersKey == key
                         select e);
            var results = query.FirstOrDefault();

            return results;
        }
    }
}
