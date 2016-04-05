using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityTagMappingRepository : DataRepositoryBase<ActivityTagMapping>, IActivityTagMappingRepository
    {
        public IEnumerable<ActivitiesMaster> GetActivitiesForSelectedSearchTag(IEnumerable<string> tags)
        {
            List<ActivitiesMaster> result = new List<ActivitiesMaster>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                List<ActivitiesMaster> subresult = (from e in entityContext.ActivityTagMappingSet
                                                    join e1 in entityContext.ActivitiesMasterSet
                                                    on e.ActivityKey equals e1.ActivitesKey
                                                    select e1).ToList();
                foreach (var tag in tags)
                {
                    subresult = (from e1 in subresult
                                 join e2 in entityContext.ActivityTagMappingSet
                                 on e1.ActivitesKey equals e2.ActivityKey
                                 where e2.Tag.Contains(tag)
                                 select e1).GroupBy(e => e.ActivitesKey).Select(grp => grp.First()).ToList();
                }
                result = subresult;
                return result;
            }
        }

        protected override ActivityTagMapping AddEntity(MyMonkeyCapContext entityContext, ActivityTagMapping entity)
        {
            return entityContext.ActivityTagMappingSet.Add(entity);
        }

        protected override ActivityTagMapping UpdateEntity(MyMonkeyCapContext entityContext, ActivityTagMapping entity)
        {
            return (from e in entityContext.ActivityTagMappingSet
                    where e.ActivityTagKey == entity.ActivityTagKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityTagMapping> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityTagMappingSet
                    select e);
        }

        protected override ActivityTagMapping GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityTagMappingSet
                         where e.ActivityTagKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
