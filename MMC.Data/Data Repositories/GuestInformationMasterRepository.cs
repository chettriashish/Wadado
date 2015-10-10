using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class GuestInformationMasterRepository : DataRepositoryBase<GuestInformationMaster>, IGuestInformationMasterRepository
    {
        protected override GuestInformationMaster AddEntity(MyMonkeyCapContext entityContext, GuestInformationMaster entity)
        {
            return entityContext.GuestInformationMasterSet.Add(entity);
        }

        protected override GuestInformationMaster UpdateEntity(MyMonkeyCapContext entityContext, GuestInformationMaster entity)
        {
            return (from e in entityContext.GuestInformationMasterSet
                    where e.GuestKey == entity.GuestKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GuestInformationMaster> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.GuestInformationMasterSet
                    select e);
        }

        protected override GuestInformationMaster GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.GuestInformationMasterSet
                         where e.GuestKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
