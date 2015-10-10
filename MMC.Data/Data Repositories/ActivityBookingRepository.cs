using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityBookingRepository : DataRepositoryBase<ActivityBooking>, IActivityBookingRepository
    {
        protected override ActivityBooking AddEntity(MyMonkeyCapContext entityContext, ActivityBooking entity)
        {
            return entityContext.ActivityBookingSet.Add(entity);
        }

        protected override ActivityBooking UpdateEntity(MyMonkeyCapContext entityContext, ActivityBooking entity)
        {
            return (from e in entityContext.ActivityBookingSet
                    where e.ActivityBookingKey == entity.ActivityBookingKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityBooking> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityBookingSet
                    select e);
        }

        protected override ActivityBooking GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityBookingSet
                         where e.ActivityBookingKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ActivityBooking> GetBookedActivitiesBySession(string sessionKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet
                             where e.SessionKey == sessionKey
                             && e.IsDeleted == false
                             select e);
                return query;
            }
        }
        public IEnumerable<ActivityBooking> GetBookedActivitiesByUserEmail(string email)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet
                             where e.Email == email
                             && e.IsDeleted == false
                             select e);
                return query;
            }
        }
    }
}
