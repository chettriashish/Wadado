using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Services
{
    public class ActivityDetailsDataService : IActivityDetailsDataService
    {
        public ActivitiesModel GetSelectedActivityDetails(string userAgent , string activityKey)
        {
            throw new NotImplementedException();
        }

        public bool CheckForSlotAvailability(string activityKey, DateTime selectedDate, decimal selectedTime, int numberOfAdults, int numberOfChildren)
        {
            throw new NotImplementedException();
        }

        public List<ActivitiesModel> GetTwoSimilarActivitiesNearby(string userAgent, string activityType, string locationCode)
        {
            throw new NotImplementedException();
        }
    }
}