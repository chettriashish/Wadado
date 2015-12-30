using Core.Common.ServiceModel;
using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Proxies
{    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
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

        public ActivityBookingDataContract AddUserActivityToCart(string activityKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey)
        {
            ActivityBookingDataContract bookingDetails = new ActivityBookingDataContract();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.CreatedDate = DateTime.Now;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            bookingDetails.SessionKey = sessionKey;
            bookingDetails.ConfirmationDate = new DateTime(1753, 1, 1);
            bookingDetails.Time = time;
            bookingDetails.IsConfirmed = false;
            bookingDetails.IsDeleted = false;
            bookingDetails.IsCancelled = false;
            bookingDetails.PaymentAmount = total;
            bookingDetails.RefundAmount = 0;
            bookingDetails.IsPaymentComplete = false;
            //Need to add user information as well. This after login is complete
            return BookActivityForUser(bookingDetails);
        }

        public ActivityBookingDataContract AddUserActivityToCart(string activityKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey, string guestKey)
        {
            ActivityBookingDataContract bookingDetails = new ActivityBookingDataContract();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.CreatedDate = DateTime.Now;
            bookingDetails.GuestKey = guestKey;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            bookingDetails.SessionKey = sessionKey;
            bookingDetails.ConfirmationDate = new DateTime(1753, 1, 1);
            bookingDetails.Time = time;
            bookingDetails.IsConfirmed = false;
            bookingDetails.IsDeleted = false;
            bookingDetails.IsCancelled = false;
            bookingDetails.PaymentAmount = total;
            bookingDetails.RefundAmount = 0;
            bookingDetails.IsPaymentComplete = false;
            //Need to add user information as well. This after login is complete
            return BookActivityForUser(bookingDetails);
        }

        public ActivityBookingDataContract BookActivityForUser(ActivityBookingDataContract bookingDetails)
        {
            return Channel.BookActivityForUser(bookingDetails);
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

        public IEnumerable<ActivityBookingDataContract> GetUsersCurrentActivityCart(string sessionKey, string userAgent)
        {
            return Channel.GetUsersCurrentActivityCart(sessionKey, userAgent);
        }

        public Task<ActivityBookingDataContract> BookActivityForUserAsync(ActivityBookingDataContract bookingDetails)
        {
            return Channel.BookActivityForUserAsync(bookingDetails);
        }


        public Task<ActivityBookingDataContract> AddUserActivityToCartAsync(string activityKey, int adults, int children, DateTime bookingDate,
            string time, decimal total, string sessionKey)
        {
            ActivityBookingDataContract bookingDetails = new ActivityBookingDataContract();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.CreatedDate = DateTime.Now;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            bookingDetails.SessionKey = sessionKey;
            bookingDetails.ConfirmationDate = new DateTime(1753, 1, 1);
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
    }
}
