using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityHolidays : ObjectBase
    {
        #region Private variables
        private string _ActivityHolidayKey;
        private string _ActivityKey;
        private DateTime _StartDate;
        private int _NumberOfDays;
        private DateTime _CreatedOn;
        private string _CreatedBy; 
        #endregion

        #region Properties
        public string ActivityHolidayKey
        {
            get { return _ActivityHolidayKey; }
            set
            {
                _ActivityHolidayKey = value;
                OnPropertyChanged(() => ActivityHolidayKey);
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

        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                _StartDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }

        public int NumberOfDays
        {
            get { return _NumberOfDays; }
            set
            {
                _NumberOfDays = value;
                OnPropertyChanged(() => NumberOfDays);
            }
        }
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set
            {
                _CreatedOn = value;
                OnPropertyChanged(() => CreatedOn);
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
        class ActivityHolidaysValidator:AbstractValidator<ActivityHolidays>
        {
            public ActivityHolidaysValidator()
            {
                RuleFor(obj => obj.ActivityHolidayKey).NotNull();
                RuleFor(obj => obj.ActivityKey).NotNull();
                RuleFor(obj => obj.StartDate).NotNull();
                RuleFor(obj => obj.NumberOfDays).GreaterThanOrEqualTo(1);
                RuleFor(obj => obj.CreatedOn).NotNull();
                RuleFor(obj => obj.CreatedBy).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityHolidaysValidator();
        }
    }
}
