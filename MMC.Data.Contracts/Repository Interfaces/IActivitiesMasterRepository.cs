using Core.Common.Contracts;
using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts.RepositoryInterfaces
{
    public interface IActivitiesMasterRepository : IDataRepository<ActivitiesMaster>
    {
        ActivitiesMaster GetActivityByCompany(string companyId);
        ActivityDetailsDataContract GetActivityByLocation(string locationKey, string activityKey, string userAgent);
        void AddActivityToUserItenerary(string activityKey, string activityDate, int numberOfPeople, string activityTime);
        IEnumerable<ActivitiesMaster> GetAllActivitiesBooked(string userAccountKey);
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationCategory(string locationKey, string activityCategoryKey, string userAgent);      
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationFilteredCategory(string locationKey, string activityCategoryKey, DateTime startDate, DateTime endDate, string userAgent);
    }
}
