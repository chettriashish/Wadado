using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityCategoryMaster : ObjectBase
    {
        #region Private variables
        private string _activityCategoryKey;
        private string _activityCategory;
        private bool _isValidated; 
        #endregion

        #region Properties
        public string ActivityCategoryKey
        {
            get { return _activityCategoryKey; }
            set
            {
                _activityCategoryKey = value;
                OnPropertyChanged(() => ActivityCategoryKey);
            }
        }

        public string ActivityCategory
        {
            get { return _activityCategory; }
            set
            {
                _activityCategory = value;
                OnPropertyChanged(() => ActivityCategory);
            }
        }

        public bool IsValidated
        {
            get { return _isValidated; }
            set
            {
                if (_isValidated != value)
                {
                    _isValidated = value;
                    OnPropertyChanged(() => IsValidated);
                }
            }
        } 
        #endregion

        /// <summary>
        /// This class is used to validate the entity
        /// </summary>
        class ActivityCategoryMasterValidator : AbstractValidator<ActivityCategoryMaster>
        {
            public ActivityCategoryMasterValidator()
            {
                RuleFor(obj => obj.ActivityCategoryKey).NotEmpty();
                RuleFor(obj => obj.ActivityCategory).NotEmpty();                
            }
        }
        /// <summary>
        /// Adding validation to an object
        /// </summary>
        /// <returns></returns>
        protected override IValidator GetValidator()
        {
            return new ActivityCategoryMasterValidator();
        }

    }
}
