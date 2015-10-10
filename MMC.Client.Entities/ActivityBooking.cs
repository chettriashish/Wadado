using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityBooking : ObjectBase
    {
        #region Private Variables
        private string _ActivityBookingKey;
        private string _ActivityKey;
        private string _AccountKey;
        private DateTime _BookingDate;
        private string _Time;
        private string _Email;
        private int _Participants;
        private int _ChildParticipants;
        private DateTime _CreatedDate;
        private string _CreatedBy;
        private bool _IsDeleted;
        #endregion

        #region Properties
        public string ActivityBookingKey
        {
            get { return _ActivityBookingKey; }
            set
            {
                _ActivityBookingKey = value;
                OnPropertyChanged(() => ActivityBookingKey);
            }
        }
        public string ActivityKey
        {
            get { return _ActivityKey; }
            set
            {
                _ActivityKey = value;
                OnPropertyChanged(() => ActivityKey);
            }
        }

        public string SessionKey
        {
            get { return _AccountKey; }
            set
            {
                _AccountKey = value;
                OnPropertyChanged(() => SessionKey);
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

        public DateTime BookingDate
        {
            get { return _BookingDate; }
            set
            {
                _BookingDate = value;
                OnPropertyChanged(() => BookingDate);
            }
        }

        public string Time
        {
            get { return _Time; }
            set
            {
                _Time = value;
                OnPropertyChanged(() => Time);
            }
        }

        public int Participants
        {
            get { return _Participants; }
            set
            {
                _Participants = value;
                OnPropertyChanged(() => Participants);
            }
        }

        public int ChildParticipants
        {
            get { return _ChildParticipants; }
            set
            {
                _ChildParticipants = value;
                OnPropertyChanged(() => ChildParticipants);
            }
        }       

        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                _IsDeleted = value;
                OnPropertyChanged(() => IsDeleted);
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
                OnPropertyChanged(() => CreatedBy);
            }
        }
        #endregion
        class ActivityBookingValidator : AbstractValidator<ActivityBooking>
        {
            public ActivityBookingValidator()
            {
                RuleFor(obj => obj.ActivityBookingKey).NotEmpty();
                RuleFor(obj => obj.ActivityKey).NotEmpty();
                RuleFor(obj => obj.BookingDate).NotNull();
                RuleFor(obj => obj.SessionKey).NotEmpty();
                RuleFor(obj => obj.Participants).GreaterThanOrEqualTo(1);
                RuleFor(obj => obj.CreatedDate).NotNull();
                RuleFor(obj => obj.CreatedBy).NotEmpty();
            }
        }
        protected override IValidator GetValidator()
        {
            return new ActivityBookingValidator();
        }
    }
}
