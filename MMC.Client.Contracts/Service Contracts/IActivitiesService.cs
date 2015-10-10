using Core.Common.Contracts;
using MMC.Client.Entities;
using MMC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts
{
    [ServiceContract]    
    public interface IActivitiesService:IServiceContract
    {
        #region Sync Operations
        [OperationContract]
        IEnumerable<ActivitiesMaster> GetAllActivities(string locationKey);
        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName);
        [OperationContract]
        bool CheckForActivityAvailablity(string activityKey, int adults, int children, DateTime bookingDate, string time);
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBooking BookActivityForUser(string loginUser, string activityKey, DateTime bookingDate, string time, string accountKey, int adults, int children);              
        #endregion

        #region Async Operations
        [OperationContract]
        Task<IEnumerable<ActivitiesMaster>> GetAllActivitiesAsync(string locationKey);
        [OperationContract]        
        Task<IEnumerable<ActivitiesMaster>> GetAllBookedActivitiesAsync(string loginName);
        [OperationContract]
        Task<bool> CheckForActivityAvailablityAsync(string activityKey, int adults, int children, DateTime bookingDate, string time);
        [OperationContract]        
        Task<ActivityBooking> BookActivityForUserAsync(string loginUser, string activityKey, DateTime bookingDate, string time, string accountKey, int adults, int children);        
        #endregion
    }
}
