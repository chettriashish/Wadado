using Core.Common.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts
{
    [ServiceContract]
    public interface IUsersService : IServiceContract
    {
        #region Sync Operations
        [OperationContract]
        UserSessionDataContract LogUserSession();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserSessionDataContract AddGuestInformation(UserSessionDataContract userInformation);
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]       
        bool UpdateGuestInformation(UserSessionDataContract userInformation);
        [OperationContract]
        UserSessionDataContract GetGuestInformation(string guestKey);
        [OperationContract]
        bool AddToFavorites(string guestKey, string activityKey);
        [OperationContract]
        IEnumerable<ActivitySummaryDataContract> RemoveFromFavorites(string guestKey, string activityKey, string userAgent);
        [OperationContract]
        IEnumerable<ActivitySummaryDataContract> GetFavorites(string guestKey, string userAgent);
        [OperationContract]
        bool CheckForActivityInFavorites(string guestKey, string activityKey);
        #endregion

        #region Async Operations
        [OperationContract]
        Task<UserSessionDataContract> LogUserSessionAsync();

        [OperationContract]
        Task<UserSessionDataContract> AddGuestInformationAsync(UserSessionDataContract userInformation);
        [OperationContract]
        ///This method updates all activity bookings
        ///during checkout or during anytime the user logs in
        ///after user has booked activities
        Task<bool> UpdateGuestInformationAsync(UserSessionDataContract userInformation);
        [OperationContract]
        Task<UserSessionDataContract> GetGuestInformationAsync(string guestKey);
        #endregion
    }
}
