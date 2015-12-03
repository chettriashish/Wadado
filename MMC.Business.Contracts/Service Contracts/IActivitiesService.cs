using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using MMC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts
{
    [ServiceContract]    
    public interface IActivitiesService
    {
        [OperationContract]
        ActivityDetailsDataContract GetAllActivities(string locationKey, string activityKey, string userAgent);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName);
        [OperationContract]
        bool CheckForActivityAvailablity(string activityKey, int adults, int children, DateTime bookingDate, string time);
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBooking BookActivityForUser(string loginUser, string activityKey, DateTime bookingDate, string time, string accountKey, int adults, int children);
    }
}
