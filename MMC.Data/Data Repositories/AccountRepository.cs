using Core.Common.Contracts;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        /// <summary>
        /// overriden methods from datarepositorybase class
        /// </summary>
        /// <param name="entityContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override Account AddEntity(MyMonkeyCapContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override Account UpdateEntity(MyMonkeyCapContext entityContext, Account entity)
        {
            return (from e in entityContext.AccountSet
                    where e.AccountKey == entity.AccountKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Account> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.AccountSet
                    select e);
        }

        protected override Account GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.AccountSet
                         where e.AccountKey == key
                         select e);
            var results = query.FirstOrDefault();

            return results;
        }
        public Account ValidateUserByLogin(string userEmail)
        {
            using(MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.AccountSet
                             where e.Email == userEmail
                             select e);
                var results = query.FirstOrDefault();

                return results;
            }            
        }
    }
}
