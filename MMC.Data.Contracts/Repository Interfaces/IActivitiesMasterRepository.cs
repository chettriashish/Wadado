using Core.Common.Contracts;
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
        IEnumerable<ActivitiesMaster> GetActivityByLocation(string locationKey);
        void AddActivityToUserItenerary(string activityKey, string activityDate, int numberOfPeople, string activityTime);
        IEnumerable<ActivitiesMaster> GetAllActivitiesBooked(string userAccountKey);
    }
}
