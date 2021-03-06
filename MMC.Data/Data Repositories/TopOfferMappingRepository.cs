﻿using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class TopOfferMappingRepository : DataRepositoryBase<TopOfferMapping>, ITopOfferMappingRepository
    {
        protected override TopOfferMapping AddEntity(MyMonkeyCapContext entityContext, TopOfferMapping entity)
        {
            return entityContext.TopOfferMappingSet.Add(entity);
        }

        protected override TopOfferMapping UpdateEntity(MyMonkeyCapContext entityContext, TopOfferMapping entity)
        {
            return (from e in entityContext.TopOfferMappingSet
                    where e.TopOfferMappingKey == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TopOfferMapping> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.TopOfferMappingSet
                    where e.IsDeleted == false
                    select e);
        }

        protected override TopOfferMapping GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.TopOfferMappingSet
                         where e.TopOfferMappingKey == key
                         && e.IsDeleted == false
                         select e);
            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<TopOfferMapping> GetAllTopActivitiesOfferForSelectedLocation(string locationKey)
        {
            IEnumerable<TopOfferMapping> results = new List<TopOfferMapping>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                results = (from e1 in entityContext.TopOffersSet
                           join e2 in entityContext.TopOfferMappingSet
                           on e1.TopOffersKey equals e2.TopOfferKey
                           where e1.LocationKey == locationKey
                           && (e1.OfferEndDate > DateTime.Now && e1.OfferStartDate <= DateTime.Now)
                           select e2).ToList();
            }

            return results;
        }

        public TopOfferMapping CheckAndFetchSingleOfferExists(string mappingKey, string mappingType)
        {
            TopOfferMapping result = new TopOfferMapping();
            //Mapping type can either be 'SINGLE' or 'BUNDLED'
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e1 in entityContext.TopOffersSet
                             join e2 in entityContext.TopOfferMappingSet
                             on e1.TopOffersKey equals e2.TopOfferKey
                             where e2.MappingKey == mappingKey
                             && e2.MappingType == mappingType
                             && (e1.OfferEndDate > DateTime.Now && e1.OfferStartDate <= DateTime.Now)
                             select e2);
                result = query.FirstOrDefault(e => e.TopOfferKey.Count() == 1);
            }
            return result;
        }

        public bool AddAll(IEnumerable<TopOfferMapping> mappings)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                foreach (var item in mappings)
                {
                    AddEntity(entityContext, item);
                }
            }
            return true;
        }
    }
}
