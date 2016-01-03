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
        //[FaultContract(typeof(AuthorizationValidationException))]
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
        ActivityBookingDataContract BookActivityForUser(ActivityBookingDataContract bookingDetails);

        ///TBD ONCE LOGIN IS COMPLETE
        //[OperationContract]
        //IEnumerable<ActivityDetailsDataContract> GetUsersCurrentActivityCart(string loginUser, string sessionKey);

        ///TO BE REMOVED ONCE LOGIN IS COMPLETE
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetUsersCurrentActivityCart(string sessionKey, string userAgent);
        [OperationContract]
        ActivityBookingDataContract AddUserActivityToCart(string activityKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey);

        ActivityBookingDataContract AddUserActivityToCart(string activityKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey, string guestKey);
        [OperationContract]
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationAndType(string locationKey, string activityCategoryKey, string userAgent);
        [OperationContract]
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationFilteredCategory(string locationKey, string activityCategoryKey, DateTime startDate, DateTime endDate, string userAgent);
        #endregion

        #region Async Operations
        [OperationContract]
        Task<ActivityDetailsDataContract> GetAllActivitiesAsync(string locationKey, string activityKey, string userAgent);
        [OperationContract]        
        Task<IEnumerable<ActivitiesMaster>> GetAllBookedActivitiesAsync(string loginName);
        [OperationContract]
        Task<bool> CheckForActivityAvailablityAsync(string activityKey, int adults, int children, DateTime bookingDate, string time);
        [OperationContract]
        Task<ActivityBookingDataContract> BookActivityForUserAsync(ActivityBookingDataContract bookingDetails);
        Task<ActivityBookingDataContract> AddUserActivityToCartAsync(string activityKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey);
        #endregion
    }
}
