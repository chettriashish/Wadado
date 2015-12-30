using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using MMC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
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
    }
}
