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
    public interface IActivitiesMasterRepository: IDataRepository<ActivitiesMaster>
    {
        ActivitiesMaster GetActivityByCompany(string companyId);
        ActivityDetailsDataContract GetActivityByLocation(string locationKey, string activityKey, string userAgent);
        void AddActivityToUserItenerary(string activityKey, string activityDate, int numberOfPeople, string activityTime);
        IEnumerable<ActivitiesMaster> GetAllActivitiesBooked(string userAccountKey);
    }
}
