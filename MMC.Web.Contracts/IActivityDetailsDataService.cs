using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Contracts
{
    public interface IActivityDetailsDataService
    {
        ActivitiesModel GetSelectedActivityDetails(string userAgent, string activityKey);
        bool CheckForSlotAvailability(string activityKey, DateTime selectedDate, decimal selectedTime, int numberOfAdults, int numberOfChildren);
        List<ActivitiesModel> GetTwoSimilarActivitiesNearby(string userAgent, string activityKey);
    }
}
