using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Common
{
    public interface IActivitiesBookingEngine : IBusinessEngine
    {
        bool IsActivityAvailable(string activityKey, DateTime bookingDate, string bookingTime,
            IEnumerable<ActivityBooking> bookedActivites, int adults,int children, IEnumerable<ActivitiesMaster> allActivities);
        ActivityBooking BookActivityForUser(ActivityBooking bookedActivity);

        void UpdateActivityForUser(string sessionKey, string userKey);

        IEnumerable<ActivityBooking> GetBookedActivitiesForUser(string sessionKey, string guestKey);
    }
}
