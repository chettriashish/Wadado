using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
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

        public IEnumerable<TopOffers> GetTopOffersForLocation(string locationKey)
        {
            IEnumerable<TopOffers> results = new List<TopOffers>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.TopOffersSet
                             where e.LocationKey == locationKey
                             && e.OfferStartDate <= DateTime.Now && e.OfferEndDate >= DateTime.Now
                             select e).OrderBy(e => e.OfferStartDate);

                results =  query.ToList();
            }
            return results;
        }

        public IEnumerable<TopOffers> GetOffersForActivity(string activityKey)
        {
            IEnumerable<TopOffers> results = new List<TopOffers>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.TopOffersSet
                             join e1 in entityContext.TopOfferMappingSet
                             on e.TopOffersKey equals e1.TopOfferKey
                             where e1.MappingKey == activityKey                             
                             && e.OfferStartDate <= DateTime.Now && e.OfferEndDate >= DateTime.Now
                             select e).OrderBy(e => e.OfferStartDate);

                results = query.ToList();
            }
            return results;
        }
    }
}
