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
            return Channel.GetAllActivities(locationKey,activityKey,userAgent);
        }

        public IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName)
        {
            return Channel.GetAllBookedActivities(loginName);
        }

        public bool CheckForActivityAvailablity(string activityKey, int adults, int children, DateTime bookingDate, string time)
        {
            return Channel.CheckForActivityAvailablity(activityKey, adults, children, bookingDate, time);
        }

        public ActivityBooking BookActivityForUser(string loginUser, string activityKey, DateTime bookingDate, string time, string accountKey, int adults, int children)
        {
            return Channel.BookActivityForUser(loginUser, activityKey, bookingDate, time, accountKey,  adults , children);
        }

        public Task<ActivityBooking> BookActivityForUserAsync(string loginUser, string activityKey, DateTime bookingDate, string time, string accountKey, int adults, int children)
        {
            return Channel.BookActivityForUserAsync(loginUser, activityKey, bookingDate, time, accountKey, adults, children);
        }

        public Task<ActivityDetailsDataContract> GetAllActivitiesAsync(string locationKey, string activityKey, string userAgent)
        {
            return Channel.GetAllActivitiesAsync(locationKey, activityKey, userAgent);
        }

        public Task<IEnumerable<ActivitiesMaster>> GetAllBookedActivitiesAsync(string loginName)
        {
            return Channel.GetAllBookedActivitiesAsync(loginName);
        }

        public Task<bool> CheckForActivityAvailablityAsync(string activityKey, int adults, int children, DateTime bookingDate, string time)
        {
            return Channel.CheckForActivityAvailablityAsync(activityKey, adults, children, bookingDate, time);
        }
    }
}
