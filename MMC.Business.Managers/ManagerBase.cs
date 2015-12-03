using Core.Common.Contracts;
using MMC.Business.Entities;
using MMC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMC.Business.Managers
{
    public class ManagerBase
    {
        protected IDataRepositoryFactory _DataRepositoryFactory;
        protected IBusinessEngineFactory _BusinessEngineFactory;
        protected Account _AuthorizationAccount;
        protected string _LoginName;
        public ManagerBase()
        {
            //OperationContext context = OperationContext.Current;
            //if(context != null)
            //{
            //    _LoginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");
            //}
            //if(!string.IsNullOrWhiteSpace(_LoginName))
            //{
            //    _AuthorizationAccount = LoadAuthorizationValidationAccount(_LoginName);
            //}
        }

        protected virtual Account LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected void ValidateAuthorization(IAccountOwnedEntity entity)
        {
            if(!Thread.CurrentPrincipal.IsInRole(Security.MMCAdminRole))
            {
                if(_AuthorizationAccount != null)
                {
                    if(_LoginName != string.Empty && entity.OwnerAccountId != _AuthorizationAccount.AccountKey)
                    {
                        AuthorizationValidationException ex = new AuthorizationValidationException("Attempt to access a secure record with improper user authorization validation.");
                        throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
                    }
                }
            }
        }
        
        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();
            }
            catch (Exception ex)
            {
                
                throw new FaultException(ex.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
