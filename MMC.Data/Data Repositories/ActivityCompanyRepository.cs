using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityCompanyRepository : DataRepositoryBase<ActivityCompany>, IActivityCompanyRepository
    {
        protected override ActivityCompany AddEntity(MyMonkeyCapContext entityContext, ActivityCompany entity)
        {
            return entityContext.ActivityCompanySet.Add(entity);
        }

        protected override ActivityCompany UpdateEntity(MyMonkeyCapContext entityContext, ActivityCompany entity)
        {
            return (from e in entityContext.ActivityCompanySet
                    where e.ActivityCompanyKey == entity.ActivityCompanyKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityCompany> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityCompanySet
                    select e);
        }

        protected override ActivityCompany GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityCompanySet
                         where e.ActivityCompanyKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public CompanyMaster GetCompanyByActivity(string activityKey)
        {
            CompanyMaster result = new CompanyMaster();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.CompanyMasterSet
                             join e1 in entityContext.ActivityCompanySet
                             on e.CompanyKey equals e1.CompanyKey
                             where e1.ActivityKey == activityKey
                             select e).ToList();

                result = query.FirstOrDefault();
            }
            return result;
        }

        public IEnumerable<ActivitiesMaster> GetAllActivitiesForSelectedCompany(string companyKey)
        {
            throw new NotImplementedException();
        }
    }
}
