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
        ActivityBooking BookActivityForUser(string loginUser, string activityKey, DateTime bookingDate, 
            string time, string accountKey, int adults, int children);
    }
}
