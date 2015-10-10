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
    public class ActivityImages : ObjectBase
    {
        #region Private variables
        private string _activityImageKey;
        private string _activityKey;
        private bool _isDefault;
        private string _imageURL; 
        #endregion

        #region Properties
        public string ActivityImageKey
        {
            get { return _activityImageKey; }
            set
            {
                _activityImageKey = value;
                OnPropertyChanged(() => ActivityImageKey);
            }
        }

        [ForeignKey("ActivitiesMaster")]
        public string ActivityKey
        {
            get { return _activityKey; }
            set
            {
                _activityKey = value;
                OnPropertyChanged(() => ActivityKey);
            }
        }

        public string ImageURL
        {
            get { return _imageURL; }
            set
            {
                _imageURL = value;
                OnPropertyChanged(() => ImageURL);
            }
        }


        public bool IsDefault
        {
            get { return _isDefault; }
            set
            {
                _isDefault = value;
                OnPropertyChanged(() => IsDefault);
            }
        } 
        #endregion

        class ActivityImagesValidator : AbstractValidator<ActivityImages>
        {
            public ActivityImagesValidator()
            {
                RuleFor(obj => obj.ActivityImageKey).NotNull();
                RuleFor(obj => obj.ActivityKey).NotNull();
                RuleFor(obj => obj.ImageURL).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityImagesValidator();
        }

    }
}
