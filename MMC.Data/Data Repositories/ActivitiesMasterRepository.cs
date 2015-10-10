using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivitiesMasterRepository : DataRepositoryBase<ActivitiesMaster>, IActivitiesMasterRepository
    {
        /// <summary>
        /// overriden methods from datarepositorybase class
        /// </summary>
        /// <param name="entityContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override ActivitiesMaster AddEntity(MyMonkeyCapContext entityContext, ActivitiesMaster entity)
        {
            return entityContext.ActivitiesMasterSet.Add(entity);
        }

        protected override ActivitiesMaster UpdateEntity(MyMonkeyCapContext entityContext, ActivitiesMaster entity)
        {
            return (from e in entityContext.ActivitiesMasterSet
                    where e.ActivitesKey == entity.ActivitesKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivitiesMaster> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivitiesMasterSet
                    select e);
        }

        protected override ActivitiesMaster GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivitiesMasterSet
                         where e.ActivitesKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public ActivitiesMaster GetActivityByCompany(string companyId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActivitiesMaster> GetActivityByLocation(string locationKey)
        {
            throw new NotImplementedException();
        }

        public void AddActivityToUserItenerary(string activityKey, string activityDate, int numberOfPeople, string activityTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fetch user information based on user account details
        /// </summary>
        /// <param name="userAccountKey"></param>
        /// <returns></returns>
        public IEnumerable<ActivitiesMaster> GetAllActivitiesBooked(string userAccountKey)
        {
            throw new NotImplementedException();
        }
    }
}
