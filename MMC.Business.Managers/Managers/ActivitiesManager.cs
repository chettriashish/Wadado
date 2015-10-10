using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MMC.Business.Contracts;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System.ServiceModel;
using MMC.Business.Common;
using System.Security.Permissions;
using MMC.Common;
using Core.Common.Exceptions;

namespace MMC.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ActivitiesManager : ManagerBase, IActivitiesService
    {
        public ActivitiesManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.MMCAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.MMCUser)]
        public IEnumerable<ActivitiesMaster> GetAllActivities(string locationKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IEnumerable<ActivitiesMaster> allActivitiesForLocation = activitiesMasterRepository.GetActivityByLocation(locationKey: locationKey);
                return allActivitiesForLocation;
            });
        }
        [PrincipalPermission(SecurityAction.Demand, Role = Security.MMCAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.MMCUser)]        
        public IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName)
        {            
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                        = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IAccountRepository accountRepository
                    = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account authAccount = accountRepository.ValidateUserByLogin(loginName);
                if (authAccount == null)
                {
                    NotFoundException ex = 
                    new NotFoundException(string.Format("cannot find account for login name {0} to use for security trimming", loginName));

                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                ///validating the account to check with actual logged in users account.
                ///This helps us cross check whether the logged in user is actually the user
                ///whose credentials are being passed.

                ValidateAuthorization(authAccount);

                return activitiesMasterRepository.GetAllActivitiesBooked(userAccountKey: authAccount.AccountKey);
            });
        }
        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository 
                = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            Account authAccount = accountRepository.ValidateUserByLogin(loginName);

            if(authAccount == null)
            {
                throw new NotFoundException(string.Format("cannot find account for login name {0} to use for security trimming",loginName));
            }           

            return authAccount;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = Security.MMCAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.MMCUser)]        
        public bool CheckForActivityAvailablity(string activityKey, int adults,int children, DateTime bookingDate, string time)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                    = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IActivityBookingRepository activityBookingRepository
                    = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();

                IActivitiesBookingEngine activitiesBookingEngine
                    = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();

                IEnumerable<ActivitiesMaster> allActivities = activitiesMasterRepository.Get();

                IEnumerable<ActivityBooking> allBookedActivites = activityBookingRepository.Get();

                return activitiesBookingEngine.IsActivityAvailable(activityKey, bookingDate, time, allBookedActivites, adults,children, allActivities);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.MMCAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.MMCUser)]  
        public ActivityBooking BookActivityForUser(string loginUser, string activityKey, DateTime bookingDate, string time, string accountKey, int adults, int children)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesBookingEngine activitiesBookingEngine
                  = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();

                return activitiesBookingEngine.BookActivityForUser(loginUser, activityKey, bookingDate, time, accountKey, adults, children);
            });
        }
    }
}
