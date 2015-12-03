using Core.Common.Contracts;
using MMC.Client.Contracts.DataContracts;
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
        ActivityDetailsDataContract GetAllActivities(string locationKey, string activityKey, string userAgent);
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
        Task<ActivityDetailsDataContract> GetAllActivitiesAsync(string locationKey, string activityKey, string userAgent);
        [OperationContract]        
        Task<IEnumerable<ActivitiesMaster>> GetAllBookedActivitiesAsync(string loginName);
        [OperationContract]
        Task<bool> CheckForActivityAvailablityAsync(string activityKey, int adults, int children, DateTime bookingDate, string time);
        [OperationContract]        
        Task<ActivityBooking> BookActivityForUserAsync(string loginUser, string activityKey, DateTime bookingDate, string time, string accountKey, int adults, int children);        
        #endregion
    }
}
