using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class UserCompanyMapping : ObjectBase
    {
        #region Private Variables
        private string _UserCompanyKey;
        private string _UserKey;
        private string _CompanyKey;
        private string _IsDeleted; 
        #endregion

        #region Properties

        public string UserCompanyKey
        {
            get { return _UserCompanyKey; }
            set
            {
                _UserCompanyKey = value;
                OnPropertyChanged(() => UserCompanyKey);
            }
        }

        public string UserKey
        {
            get { return _UserKey; }
            set
            {
                _UserKey = value;
                OnPropertyChanged(() => UserKey);
            }
        }

        public string CompanyKey
        {
            get { return _CompanyKey; }
            set
            {
                _CompanyKey = value;
                OnPropertyChanged(() => CompanyKey);
            }
        }

        public string IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                _IsDeleted = value;
                OnPropertyChanged(() => IsDeleted);
            }
        } 
        #endregion

        class UserCompanyMappingValidator : AbstractValidator<UserCompanyMapping>
        {
            public UserCompanyMappingValidator()
            {
                RuleFor(obj => obj.UserCompanyKey).NotEmpty();
                RuleFor(obj => obj.UserKey).NotEmpty();
                RuleFor(obj => obj.CompanyKey).NotEmpty();                
            }
        }
        protected override IValidator GetValidator()
        {
            return new UserCompanyMappingValidator();
        }

    }
}
