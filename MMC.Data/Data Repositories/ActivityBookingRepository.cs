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
                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }
        public IEnumerable<ActivityBooking> GetBookedActivitiesByUserKey(string userKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet
                             where e.GuestKey == userKey
                             && e.IsDeleted == false
                             select e);
                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }

        public IEnumerable<ActivityBooking> GetAllCompanyActivitiesPendingForConfirmation(string companyKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext()) 
            {
                var query = (from e in entityContext.ActivityBookingSet                             
                             join e3 in entityContext.ActivityCompanySet
                             on e.ActivityKey equals e3.ActivityKey
                             where e3.CompanyKey == companyKey
                             && e.IsConfirmed == false &&  e.IsDeleted == false
                             select e);

                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }       
        public IEnumerable<ActivityBooking> GetAllActivitiesPendingForConfirmation()
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet
                             join e1 in entityContext.ActivitiesMasterSet
                             on e.ActivityKey equals e1.ActivitesKey
                             where e.IsConfirmed == false && e.IsDeleted == false
                             select e);

                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }

        public IEnumerable<ActivityBooking> GetAllCompanyActivitiesCompleted(string companyKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet                                                      
                             join e1 in entityContext.ActivityCompanySet
                             on e.ActivityKey equals e1.ActivityKey
                             where e1.CompanyKey == companyKey
                             && e.IsConfirmed == true && e.IsDeleted == false
                             && e.IsPaymentComplete == true && e.BookingDate < DateTime.Now
                             select e);

                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }
        public IEnumerable<ActivityBooking> GetAllActivitiesCompleted()
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet
                             join e1 in entityContext.ActivitiesMasterSet
                             on e.ActivityKey equals e1.ActivitesKey
                             where e.IsConfirmed == true && e.IsDeleted == false
                             && e.IsPaymentComplete == true && e.BookingDate < DateTime.Now
                             select e);

                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }

        public IEnumerable<ActivityBooking> GetAllUpcomingCompanyActivities(string companyKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet                             
                             join e1 in entityContext.ActivityCompanySet
                             on e.ActivityKey equals e1.ActivityKey
                             where e1.CompanyKey == companyKey
                             && e.IsConfirmed == true && e.IsDeleted == false
                             && e.IsPaymentComplete == true && e.BookingDate >= DateTime.Now
                             select e);

                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }
        public IEnumerable<ActivityBooking> GetAllUpcomingActivities()
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                var query = (from e in entityContext.ActivityBookingSet
                             join e1 in entityContext.ActivitiesMasterSet
                             on e.ActivityKey equals e1.ActivitesKey
                             where e.IsConfirmed == true && e.IsDeleted == false
                             && e.IsPaymentComplete == true && e.BookingDate >= DateTime.Now
                             select e);

                return query.OrderBy(e => e.BookingDate).ToList();
            }
        }
    }
}
