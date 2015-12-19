using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class UserApplicationActivityRepository : DataRepositoryBase<UserApplicationActivityDetails>, IUserApplicationActivityRepository
    {
        protected override UserApplicationActivityDetails AddEntity(MyMonkeyCapContext entityContext, UserApplicationActivityDetails entity)
        {
            return entityContext.UserApplicationActivityDetailsSet.Add(entity);
        }

        protected override UserApplicationActivityDetails UpdateEntity(MyMonkeyCapContext entityContext, UserApplicationActivityDetails entity)
        {
            return (from e in entityContext.UserApplicationActivityDetailsSet
                    where e.SessionKey == entity.SessionKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserApplicationActivityDetails> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.UserApplicationActivityDetailsSet
                    select e);
        }

        protected override UserApplicationActivityDetails GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.UserApplicationActivityDetailsSet
                         where e.SessionKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public UserApplicationActivityDetails LogUserSession()
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                UserApplicationActivityDetails result = new UserApplicationActivityDetails();
                result.SessionKey = string.Format("{0}{1}", Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                result.LastUpdateSessionTime = DateTime.Now;
                result.UserLoggedIn = false;
                AddEntity(entityContext, result);
                entityContext.SaveChanges();
                return result;
            }            
        }

        public void UpdateUserSession(string sessionKey, bool isUserLoggedIn, string loginMethod)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                UserApplicationActivityDetails result = GetEntity(entityContext, sessionKey);                
                result.LastUpdateSessionTime = DateTime.Now;
                result.UserLoggedIn = true;
                result.LoginMethod = loginMethod;
                Update(result);                
            }            
        }
    }
}
