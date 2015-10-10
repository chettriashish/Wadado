using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class CompanyMaster : ObjectBase
    {
        #region Private variables
        private string _CompanyKey;
        private string _Address;
        private string _Name;
        private string _TelephoneNumber;
        private string _Email;
        private decimal _Rating;
        private string _ContactPerson;
        private DateTime _CreatedDate;
        private string _CreatedBy; 
        #endregion

        #region Properties
        public string CompanyKey
        {
            get { return _CompanyKey; }
            set
            {
                _CompanyKey = value;
                OnPropertyChanged(() => CompanyKey);
            }
        }
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged(() => Address);
            }
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(() => Name);
            }
        }
        public string TelephoneNumber
        {
            get { return _TelephoneNumber; }
            set
            {
                _TelephoneNumber = value;
                OnPropertyChanged(() => TelephoneNumber);
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
        public decimal Rating
        {
            get { return _Rating; }
            set
            {
                _Rating = value;
                OnPropertyChanged(() => Rating);
            }
        }
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set
            {
                _ContactPerson = value;
                OnPropertyChanged(() => ContactPerson);
            }
        }
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                _CreatedDate = value;
                OnPropertyChanged(() => CreatedDate);
            }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                OnPropertyChanged(() => CompanyKey);
            }
        } 
        #endregion
        class CompanyMasterValidator:AbstractValidator<CompanyMaster>
        {
            public CompanyMasterValidator()
            {
                RuleFor(obj => obj.CompanyKey).NotNull();
                RuleFor(obj => obj.Name).NotEmpty();
                RuleFor(obj => obj.CreatedBy).NotNull();
                RuleFor(obj => obj.ContactPerson).NotNull();
                RuleFor(obj => obj.CreatedDate).NotNull();
                RuleFor(obj => obj.CompanyKey).NotNull();
                RuleFor(obj => obj.Email).Matches("EmailExpression");
                RuleFor(obj => obj.TelephoneNumber).Matches("TelephoneExpression");
            }            
        }
        protected override IValidator GetValidator()
        {
            return new CompanyMasterValidator();
        }
    }
}
