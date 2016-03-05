using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common.Core;
using FluentValidation;
using FluentValidation.Validators;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MMC.Client.Entities
{    
    public class ActivityDates : ObjectBase
    {
        #region Private variables
        private string _activityDatesKey;
        private bool _isDeleted;
        private string _activityKey;
        private DateTime _date;
        private string _time; 
        #endregion

        #region Properties
        public string ActivityDatesKey
        {
            get { return _activityDatesKey; }
            set
            {
                _activityDatesKey = value;
                OnPropertyChanged(() => ActivityDatesKey);
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(() => Date);
            }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
                OnPropertyChanged(() => IsDeleted);
            }
        }

        public string ActivityKey
        {
            get { return _activityKey; }
            set
            {
                _activityKey = value;
                OnPropertyChanged(() => ActivityKey);
            }
        }

        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(() => Time);
            }
        } 
        #endregion

        /// <summary>
        /// This class is used to validate the entity
        /// </summary>
        class ActivityDatesValidator : AbstractValidator<ActivityDates>
        {
            public ActivityDatesValidator()
            {
                RuleFor(obj => obj.ActivityDatesKey).NotEmpty();
                RuleFor(obj => obj.ActivityKey).NotEmpty();
                RuleFor(obj => obj.Date).NotEmpty();                               
            }
        }
        /// <summary>
        /// Adding validation to an object
        /// </summary>
        /// <returns></returns>
        protected override IValidator GetValidator()
        {
            return new ActivityDatesValidator();
        }

    }
}
