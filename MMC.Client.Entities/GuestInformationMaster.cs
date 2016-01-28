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
    public class GuestInformationMaster : ObjectBase
    {
        #region Private variables
        private string _GuestKey;
        private string _Name;
        private DateTime _DOB;
        private string _Address;
        private string _City;
        private string _State;
        private string _Pin;
        private string _Email;
        private string _PhoneNumber; 
        #endregion

        #region Properties
        public string GuestKey
        {
            get { return _GuestKey; }
            set
            {
                _GuestKey = value;
                OnPropertyChanged(() => GuestKey);
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
        public DateTime DOB
        {
            get { return _DOB; }
            set
            {
                _DOB = value;
                OnPropertyChanged(() => DOB);
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
        public string City
        {
            get { return _City; }
            set
            {
                _City = value;
                OnPropertyChanged(() => City);
            }
        }
        public string State
        {
            get { return _State; }
            set
            {
                _State = value;
                OnPropertyChanged(() => State);
            }
        }
        public string Pin
        {
            get { return _Pin; }
            set
            {
                _Pin = value;
                OnPropertyChanged(() => Pin);
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
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged(() => PhoneNumber);
            }
        }
        
        #endregion
        class GuestInformationMasterValidator:AbstractValidator<GuestInformationMaster>
        {
            public GuestInformationMasterValidator()
            {
                RuleFor(obj => obj.GuestKey).NotNull();
                RuleFor(obj => obj.Name).NotNull();
                RuleFor(obj => obj.City).NotNull();
                RuleFor(obj => obj.State).NotNull();
                RuleFor(obj => obj.DOB).NotNull();
                RuleFor(obj => obj.PhoneNumber).Matches("Mobile Number Expression");
                RuleFor(obj => obj.Email).Matches("Email Expression");
            }
        }

        protected override IValidator GetValidator()
        {
            return new GuestInformationMasterValidator();
        }
    }
}
