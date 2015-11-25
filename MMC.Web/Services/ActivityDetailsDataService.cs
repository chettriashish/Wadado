using MMC.Web.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Services
{
    public class ActivityDetailsDataService : IActivityDetailsDataService
    {
        public Model.ActivitiesModel GetSelectedActivityDetails(string userAgent , string activityKey)
        {
            throw new NotImplementedException();
        }

        public bool CheckForSlotAvailability(string activityKey, DateTime selectedDate, decimal selectedTime, int numberOfAdults, int numberOfChildren)
        {
            throw new NotImplementedException();
        }

        public List<Model.ActivitiesModel> GetTwoSimilarActivitiesNearby(string userAgent, string activityKey)
        {
            throw new NotImplementedException();
        }
    }
}