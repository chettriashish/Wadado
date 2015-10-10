using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class CompanyMasterRepository : DataRepositoryBase<CompanyMaster>, ICompanyMasterRepository
    {
        protected override CompanyMaster AddEntity(MyMonkeyCapContext entityContext, CompanyMaster entity)
        {
            return entityContext.CompanyMasterSet.Add(entity);
        }

        protected override CompanyMaster UpdateEntity(MyMonkeyCapContext entityContext, CompanyMaster entity)
        {
            return (from e in entityContext.CompanyMasterSet
                    where e.CompanyKey == entity.CompanyKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CompanyMaster> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.CompanyMasterSet
                    select e);
        }

        protected override CompanyMaster GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.CompanyMasterSet
                         where e.CompanyKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
