using Core.Common.Contracts;
using Core.Common.Core;
using MMC.Business.Common;
using MMC.Business.Contracts;
using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class UsersManager : ManagerBase, IUsersService
    {
        public UsersManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
            ObjectBase.Container = Bootstrapper.Bootstrapper.Initialise();
        }
        public UserSessionDataContract LogUserSession()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserApplicationActivityRepository userApplicationActivityRepository
                        = _DataRepositoryFactory.GetDataRepository<IUserApplicationActivityRepository>();
                UserApplicationActivityDetails userActivityDetails = userApplicationActivityRepository.LogUserSession();
                UserSessionDataContract result = new UserSessionDataContract();
                result.SessionKey = userActivityDetails.SessionKey;
                result.IsUserLoggedIn = userActivityDetails.UserLoggedIn;
                result.LoginMethod = userActivityDetails.LoginMethod;

                return result;
            });
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public UserSessionDataContract AddGuestInformation(UserSessionDataContract userInformation)
        {
            return ExecuteFaultHandledOperation(() =>
                {
                    IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                    GuestInformationMaster guestInformation = userDetailsBusinessEngine.AddGuestInformation(userInformation.Name, userInformation.GuestKey, userInformation.Email);
                    IUserApplicationActivityRepository userApplicationActivityRepository
                       = _DataRepositoryFactory.GetDataRepository<IUserApplicationActivityRepository>();
                    userApplicationActivityRepository.UpdateUserSession(userInformation.SessionKey, userInformation.IsUserLoggedIn, userInformation.LoginMethod);
                    //for existing users
                    if (guestInformation.DOB != default(DateTime))
                    {
                        userInformation.DOB = guestInformation.DOB;
                        userInformation.Address = guestInformation.Address;
                        userInformation.City = guestInformation.City;
                        userInformation.Email = guestInformation.Email;                        
                    }
                    return userInformation;
                });
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public bool UpdateGuestInformation(UserSessionDataContract userInformation)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                IActivitiesBookingEngine activitiesBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();
                activitiesBusinessEngine.UpdateActivityForUser(userInformation.SessionKey, userInformation.GuestKey);
                GuestInformationMaster guestInformation = new GuestInformationMaster();
                guestInformation.GuestKey = userInformation.GuestKey;
                guestInformation.Address = userInformation.Address;
                guestInformation.City = userInformation.City;
                guestInformation.DOB = userInformation.DOB;
                guestInformation.Email = userInformation.Email;
                guestInformation.Name = userInformation.Name;
                guestInformation.PhoneNumber = userInformation.PhoneNumber;
                guestInformation.Pin = userInformation.Pin;
                guestInformation.State = userInformation.State;
                return userDetailsBusinessEngine.UpdateGuestInformation(guestInformation, userInformation.SessionKey);
            });
        }

        public UserSessionDataContract GetGuestInformation(string guestKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                IActivitiesBookingEngine activitiesBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();
                GuestInformationMaster guestInformation = userDetailsBusinessEngine.GetUserInformation(guestKey);
                UserSessionDataContract result = new UserSessionDataContract();
                result.GuestKey = guestInformation.GuestKey;
                result.Address = guestInformation.Address;
                result.City = guestInformation.City;
                result.DOB = guestInformation.DOB;
                result.Email = guestInformation.Email;
                result.Name = guestInformation.Name;
                result.PhoneNumber = guestInformation.PhoneNumber;
                result.Pin = guestInformation.Pin;
                result.State = guestInformation.State;
                return result;
            });
        }
    }
}
