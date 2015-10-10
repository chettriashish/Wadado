using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityDayScheduler : ObjectBase
    {
        #region Private Variables
        private string _ActivityDaySchedulerKey;
        private string _ActivityKey;
        private bool _IsSunday;
        private bool _IsMonday;
        private bool _IsTuesday;
        private bool _IsWednesday;
        private bool _IsThursday;
        private bool _IsFriday;
        private bool _IsSaturday; 
        #endregion

        #region Properties
        public string ActivityDaySchedulerKey
        {
            get { return _ActivityDaySchedulerKey; }
            set
            {
                _ActivityDaySchedulerKey = value;
                OnPropertyChanged(() => ActivityDaySchedulerKey);
            }
        }
        public string ActivityKey
        {
            get { return _ActivityKey; }
            set
            {
                _ActivityKey = value;
                OnPropertyChanged(() => ActivityDaySchedulerKey);
            }
        }

        public bool IsSunday
        {
            get { return _IsSunday; }
            set
            {
                _IsSunday = value;
                OnPropertyChanged(() => IsSunday);
            }
        }
        public bool IsMonday
        {
            get { return _IsMonday; }
            set
            {
                _IsMonday = value;
                OnPropertyChanged(() => IsMonday);
            }
        }
        public bool IsTuesday
        {
            get { return _IsTuesday; }
            set
            {
                _IsTuesday = value;
                OnPropertyChanged(() => IsTuesday);
            }
        }
        public bool IsWednesday
        {
            get { return _IsWednesday; }
            set
            {
                _IsWednesday = value;
                OnPropertyChanged(() => IsWednesday);
            }
        }
        public bool IsThursday
        {
            get { return _IsThursday; }
            set
            {
                _IsThursday = value;
                OnPropertyChanged(() => IsThursday);
            }
        }
        public bool IsFriday
        {
            get { return _IsFriday; }
            set
            {
                _IsFriday = value;
                OnPropertyChanged(() => IsFriday);
            }
        }

        public bool IsSaturday
        {
            get { return _IsSaturday; }
            set
            {
                _IsSaturday = value;
                OnPropertyChanged(() => IsSaturday);
            }
        } 
        #endregion

        class ActivityDaySchedulerValidator:AbstractValidator<ActivityDayScheduler>
        {
            public ActivityDaySchedulerValidator()
            {
                RuleFor(obj => obj.ActivityDaySchedulerKey).NotNull();
                RuleFor(obj => obj.ActivityKey).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityDaySchedulerValidator();
        }
    }
}
