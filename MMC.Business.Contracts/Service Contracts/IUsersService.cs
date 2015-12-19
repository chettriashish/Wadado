using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts
{
    [ServiceContract]    
    public interface IUsersService
    {
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
    }
}
