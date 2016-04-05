using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class UserCompanyMappingRepository : DataRepositoryBase<UserCompanyMapping>, IUserCompanyMappingRepository
    {
        protected override UserCompanyMapping AddEntity(MyMonkeyCapContext entityContext, UserCompanyMapping entity)
        {
            return entityContext.UserCompanyMappingSet.Add(entity);
        }

        protected override UserCompanyMapping UpdateEntity(MyMonkeyCapContext entityContext, UserCompanyMapping entity)
        {
            return (from e in entityContext.UserCompanyMappingSet
                    where e.UserCompanyKey == entity.UserCompanyKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserCompanyMapping> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.UserCompanyMappingSet
                    select e);
        }

        protected override UserCompanyMapping GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.UserCompanyMappingSet
                         where e.UserCompanyKey == key
                         select e);
            var results = query.FirstOrDefault();

            return results;
        }

        public bool IsUserMappedToCompany(string userId)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                if (entityContext.UserCompanyMappingSet.Any(e => e.UserKey == userId))
                {
                    return true;
                }
                return false;
            }
        }

        public CompanyMaster GetUserCompanyDetails(string userId)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.CompanyMasterSet
                             join e1 in entityContext.UserCompanyMappingSet
                             on e.CompanyKey equals e1.CompanyKey
                             where e1.UserKey == userId
                             select e);
                return query.FirstOrDefault();
            }
        }
    }
}
