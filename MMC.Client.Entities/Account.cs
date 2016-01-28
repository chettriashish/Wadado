using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    [DataContract(Namespace = "wadado.in")]
    public class Account : ObjectBase
    {
        #region Private variables
        private string _AccountKey;
        private string _Email;
        private string _Password;
        private string _AccountTypeKey; 
        #endregion

        #region Properties
        public string AccountKey
        {
            get { return _AccountKey; }
            set
            {
                _AccountKey = value;
                OnPropertyChanged(() => AccountKey);
            }
        }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged(() => Email);
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged(() => Password);
            }
        }

        public string AccountTypeKey
        {
            get { return _AccountTypeKey; }
            set
            {
                _AccountTypeKey = value;
                OnPropertyChanged(() => AccountTypeKey);
            }
        } 
        #endregion
        
        class AccountValidator:AbstractValidator<Account>
        {
            public AccountValidator()
            {
                RuleFor(obj => obj.AccountKey).NotNull();
                ///Need to do a duplicate email check
                RuleFor(obj => obj.Email).NotNull();
                RuleFor(obj => obj.Password).NotNull();
                //RuleFor(obj => obj.AccountTypeKey).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new AccountValidator();
        }
    }
}
