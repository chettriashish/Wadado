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
        private string _activityTypeKey;
        private string _locationKey;
        private bool _isDefault;
        private string _imageURL;
        private bool _isThumbnail;
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

        [ForeignKey("ActivitiesTypeMaster")]
        public string ActivityTypeKey
        {
            get { return _activityTypeKey; }
            set
            {
                _activityTypeKey = value;
                OnPropertyChanged(() => ActivityTypeKey);
            }
        }
        [ForeignKey("LocationsMaster")]
        public string LocationKey
        {
            get { return _locationKey; }
            set
            {
                _locationKey = value;
                OnPropertyChanged(() => LocationKey);
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

        public bool IsThumbnail
        {
            get { return _isThumbnail; }
            set
            {
                _isThumbnail = value;
                OnPropertyChanged(() => IsThumbnail);
            }
        }

        #endregion

        class ActivityImagesValidator : AbstractValidator<ActivityImages>
        {
            public ActivityImagesValidator()
            {
                RuleFor(obj => obj.ActivityImageKey).NotNull();
                RuleFor(obj => obj.ActivityTypeKey).NotNull();
                RuleFor(obj => obj.LocationKey).NotNull();
                RuleFor(obj => obj.ImageURL).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityImagesValidator();
        }

    }
}
