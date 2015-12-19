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

        ///TBD ONCE LOGIN IS COMPLETE
        //[OperationContract]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        //ActivityBooking BookActivityForUser(string loginUser, ActivityBooking bookingDetails);

        ///TO BE REMOVED ONCE LOGIN IS COMPLETE
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBooking BookActivityForUser(ActivityBookingDataContract bookingDetails);

        ///TBD ONCE LOGIN IS COMPLETE
        //[OperationContract]
        //IEnumerable<ActivityDetailsDataContract> GetUsersCurrentActivityCart(string loginUser, string sessionKey);

        ///TO BE REMOVED ONCE LOGIN IS COMPLETE
        [OperationContract]
        IEnumerable<ActivityDetailsDataContract> GetUsersCurrentActivityCart(string sessionKey);
        [OperationContract]
        ActivityBooking AddUserActivityToCart(string activityKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey);
        #endregion

        #region Async Operations
        [OperationContract]
        Task<ActivityDetailsDataContract> GetAllActivitiesAsync(string locationKey, string activityKey, string userAgent);
        [OperationContract]        
        Task<IEnumerable<ActivitiesMaster>> GetAllBookedActivitiesAsync(string loginName);
        [OperationContract]
        Task<bool> CheckForActivityAvailablityAsync(string activityKey, int adults, int children, DateTime bookingDate, string time);
        [OperationContract]
        Task<ActivityBooking> BookActivityForUserAsync(ActivityBookingDataContract bookingDetails);
        Task<ActivityBooking> AddUserActivityToCartAsync(string activityKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey);
        #endregion
    }
}
