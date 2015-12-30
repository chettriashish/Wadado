using Core.Common.Contracts;
using Core.Common.Exceptions;
using MMC.Business.Common;
using MMC.Business.Entities;
using MMC.Common;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.BusinessEngines
{
    public class ActivitiesBookingEngine : IActivitiesBookingEngine
    {
        IDataRepositoryFactory _DataRepositoryFactory;
        public ActivitiesBookingEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        ///bookedActivites gives us the list of completely booked activities
        ///activities could come across as available if:
        ///1. firstly based on availablity
        ///2. based on number of participants(total number of people that have already booked and are currently booking)
        ///3. based on time 
        ///4. check for other user booked activity to see if feasible
        ///5. check any other factors        
        public bool IsActivityAvailable(string activityKey, DateTime bookingDate, string bookingTime,
            IEnumerable<ActivityBooking> bookedActivites, int adults, int children, IEnumerable<ActivitiesMaster> allActivities)
        {
            bool available = true;

            //ActivityBooking bookings = bookedActivites.Where(item => item.ActivityKey == activityKey && item.BookingDate == bookingDate);
            //if (bookings != null && (
            //    (bookingDate == bookings.BookingDate && bookingTime == bookings.Time)))
            //{
            //    available = false;
            //}
            if (available)
            {
                ///Getting total number of participants that have already 
                ///booked the activity for the date and time selected 
                ///by the user
                int numAdultParticipantsAlreadyPresent = bookedActivites.Where(item => item.ActivityKey == activityKey)
                    .Where(item => item.Time == bookingTime).Where(item => item.BookingDate == bookingDate).Sum(item => item.Participants);

                int numChildParticipantsAlreadyPresent = bookedActivites.Where(item => item.ActivityKey == activityKey)
                    .Where(item => item.Time == bookingTime).Where(item => item.BookingDate == bookingDate).Sum(item => item.ChildParticipants);

                ActivitiesMaster activity = allActivities.Where(item => item.ActivitesKey == activityKey).FirstOrDefault();
                adults = adults + numAdultParticipantsAlreadyPresent;
                children = children + numChildParticipantsAlreadyPresent;

                if (activity.MaxAdults < adults || activity.MinAdults > adults)
                {
                    available = false;
                }

                if (activity.MaxChildren < children || activity.MinChildren > children)
                {
                    available = false;
                }
            }
            return available;
        }
        public bool CheckIsActivityBookedForGuest(string activityKey, string sessionKey)
        {
            IActivityBookingRepository activityBookingRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
            bool booked = false;
            IEnumerable<ActivityBooking> result = activityBookingRepository.GetBookedActivitiesBySession(sessionKey: sessionKey);

            if (result.Count() > 0 &&
                result.Where(item => item.ActivityKey == activityKey).Count() > 0)
            {
                booked = true;
            }

            return booked;
        }
        public bool CheckIsActivityBookedForLoggedInUser(string activityKey, string userKey)
        {
            IActivityBookingRepository activityBookingRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
            bool booked = false;
            IEnumerable<ActivityBooking> result = activityBookingRepository.GetBookedActivitiesByUserKey(userKey);

            if (result.Count() > 0 &&
                result.Where(item => item.ActivityKey == activityKey).Count() > 0)
            {
                booked = true;
            }

            return booked;
        }
        public void CancelActivityBookedForGuestUser(string activityKey, string sessionKey)
        {
            bool isActivityBooked = CheckIsActivityBookedForGuest(activityKey, sessionKey);
            if (isActivityBooked)
            {
                CancelActivity(() =>
                {
                    CreateActivityCancellationDetails(activityKey);
                });
            }
        }
        public void CancelActivityBookedByInhouseUser(string activityKey, string email)
        {
            bool isActivityBooked = CheckIsActivityBookedForLoggedInUser(activityKey, email);
            if (isActivityBooked)
            {
                CancelActivity(() =>
                {
                    CreateActivityCancellationDetails(activityKey);
                });
            }
        }
        public void CancelActivityBookedForLoggedInUser(string loginUser, string activityKey)
        {
            bool isActivityBooked = CheckIsActivityBookedForLoggedInUser(activityKey, loginUser);
            if (isActivityBooked)
            {
                CancelActivity(() =>
                {
                    CreateActivityCancellationDetails(activityKey);
                });
            }
        }
        private void CreateActivityCancellationDetails(string activityKey)
        {
            IActivityBookingRepository activityBookingRepository
            = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();

            ActivityBooking bookedActivity = activityBookingRepository.Get()
                .Where(item => item.ActivityKey == activityKey)
                .Where(item => item.IsDeleted == false).FirstOrDefault();

            bookedActivity.IsDeleted = true;
            activityBookingRepository.Update(bookedActivity);
        }
        public void CancelActivity(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public ActivityBooking BookActivityForUser(ActivityBooking bookingDetails)
        {
            IActivitiesMasterRepository activitiesMasterRepository
                   = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

            IActivityBookingRepository activityBookingRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();

            IEnumerable<ActivitiesMaster> allActivities = activitiesMasterRepository.Get();

            IEnumerable<ActivityBooking> allBookedActivites = activityBookingRepository.Get();

            if (bookingDetails.BookingDate < DateTime.Now.Date)
            {
                throw new UnableToRentForDateException(string.Format("Cannot book activity for date {0} yet.", bookingDetails.BookingDate.ToShortDateString()));
            }
            bool isActivityAvailable = IsActivityAvailable(bookingDetails.ActivityKey, bookingDetails.BookingDate, bookingDetails.Time, allBookedActivites,
                bookingDetails.Participants, bookingDetails.ChildParticipants, allActivities);

            ///TBD ONCE LOGIN IS IMPLEMENTED
            //IAccountRepository accountRepository
                  //= _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            //Account authAccount = accountRepository.ValidateUserByLogin(loginUser);

            //if (authAccount == null)
            //{
            //    throw new NotFoundException(string.Format("No account found for login '{0}'.", loginUser));
            //}

            ActivityBooking savedActivity = activityBookingRepository.Add(bookingDetails);
            return savedActivity;
        }
        public void UpdateActivityForUser(string sessionKey, string userKey)
        {
            IActivityBookingRepository activityBookingRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();           
            IEnumerable<ActivityBooking> result = activityBookingRepository.GetBookedActivitiesBySession(sessionKey: sessionKey);

            foreach (ActivityBooking booking in result)
            {
                booking.GuestKey = userKey;
                activityBookingRepository.Update(booking);
            }            
        }

        public IEnumerable<ActivityBooking> GetBookedActivitiesForUser(string sessionKey, string guestKey)
        {
            IActivityBookingRepository activityBookingRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();

            IEnumerable<ActivityBooking> result = new List<ActivityBooking>();

            if (sessionKey != default(string))
            {
                result = activityBookingRepository.GetBookedActivitiesBySession(sessionKey: sessionKey);                
            }
            else if(guestKey != default(string))
            {
                result = activityBookingRepository.GetBookedActivitiesByUserKey(guestKey);                
            }
            return result;
        }
    }
}
