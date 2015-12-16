using Core.Common.ServiceModel;
using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Proxies.Proxies
{
    public class ActivityClient : UserClientBase<IActivitiesService>, IActivitiesService
    {
        public ActivityDetailsDataContract GetAllActivities(string locationKey, string activityKey, string userAgent)
        {
            return Channel.GetAllActivities(locationKey, activityKey, userAgent);
        }

        public IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName)
        {
            return Channel.GetAllBookedActivities(loginName);
        }

        public bool CheckForActivityAvailablity(string activityKey, int adults, 
            int children, DateTime bookingDate, string time)
        {
            return Channel.CheckForActivityAvailablity(activityKey, adults, children, bookingDate, time);
        }

        public Task<ActivityBooking> AddUserActivityToCart(ActivityDetailsDataContract activityDetails, int adults, 
            int children, DateTime bookingDate, string time, decimal total)
        {
            ActivityBooking bookingDetails = new ActivityBooking();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityDetails.ActivityKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            //bookingDetails.SessionKey = sessionKey == default(string) ? Guid.NewGuid().ToString() : sessionKey;
            bookingDetails.Time = time;
            bookingDetails.IsConfirmed = false;
            bookingDetails.IsDeleted = false;
            bookingDetails.IsCancelled = false;
            bookingDetails.PaymentAmount = total;
            bookingDetails.RefundAmount = 0;
            bookingDetails.IsPaymentComplete = false;
            //Need to add user information as well. This after login is complete
            return BookActivityForUserAsync(bookingDetails);
        }

        public ActivityBooking BookActivityForUser(ActivityBooking bookingDetails)
        {
            return Channel.BookActivityForUser(bookingDetails);
        }

        public Task<ActivityBooking> BookActivityForUserAsync(ActivityDetailsDataContract activityDetails, 
            int adults, int children, DateTime bookingDate, string time, decimal total)
        {
            ActivityBooking bookingDetails = new ActivityBooking();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityDetails.ActivityKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            bookingDetails.SessionKey = Guid.NewGuid().ToString();
            bookingDetails.Time = time;
            bookingDetails.IsConfirmed = false;
            bookingDetails.IsDeleted = false;
            bookingDetails.IsCancelled = false;
            bookingDetails.PaymentAmount = total;
            bookingDetails.RefundAmount = 0;
            bookingDetails.IsPaymentComplete = false;
            return Channel.BookActivityForUserAsync(bookingDetails);
        }

        public Task<ActivityDetailsDataContract> GetAllActivitiesAsync(string locationKey, 
            string activityKey, string userAgent)
        {
            return Channel.GetAllActivitiesAsync(locationKey, activityKey, userAgent);
        }

        public Task<IEnumerable<ActivitiesMaster>> GetAllBookedActivitiesAsync(string loginName)
        {
            return Channel.GetAllBookedActivitiesAsync(loginName);
        }

        public Task<bool> CheckForActivityAvailablityAsync(string activityKey, int adults, 
            int children, DateTime bookingDate, string time)
        {
            return Channel.CheckForActivityAvailablityAsync(activityKey, adults, children, bookingDate, time);
        }

        public IEnumerable<ActivityDetailsDataContract> GetUsersCurrentActivityCart(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityBooking> BookActivityForUserAsync(ActivityBooking bookingDetails)
        {
            return Channel.BookActivityForUserAsync(bookingDetails);
        }
    }
}
