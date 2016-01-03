using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityTypeCategory : ObjectBase
    {
        #region Private variables
        private string _activityTypeCategoryKey;
        private string _activityTypeKey;
        private string _activityCategoryKey;
        private bool _isPrimary;
        #endregion

        #region Properties
        public string ActivityTypeCategoryKey
        {
            get { return _activityTypeCategoryKey; }
            set
            {
                _activityTypeCategoryKey = value;
                OnPropertyChanged(() => ActivityTypeCategoryKey);
            }
        }


        public string ActivityTypeKey
        {
            get { return _activityTypeKey; }
            set
            {
                _activityTypeKey = value;
                OnPropertyChanged(() => ActivityTypeKey);
            }
        }


        public string ActivityCategoryKey
        {
            get { return _activityCategoryKey; }
            set
            {
                _activityCategoryKey = value;
                OnPropertyChanged(() => ActivityCategoryKey);
            }
        }       

        public bool IsPrimary
        {
            get { return _isPrimary; }
            set
            {
                _isPrimary = value;
                OnPropertyChanged(() => IsPrimary);
            }
        }

        #endregion

        class ActivityTypeCategoryValidator : AbstractValidator<ActivityTypeCategory>
        {
            public ActivityTypeCategoryValidator()
            {
                RuleFor(obj => obj.ActivityTypeCategoryKey).NotNull();
                RuleFor(obj => obj.ActivityTypeKey).NotNull();
                RuleFor(obj => obj.ActivityCategoryKey).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityTypeCategoryValidator();
        }
    }
}
