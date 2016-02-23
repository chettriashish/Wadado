using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{    
    public class ActivityTypeMaster : ObjectBase
    {
        #region Private variables
        private string _ActivityTypeKey;        
        private string _ActivityType;
        private string _CreatedDate;
        private string _CreatedBy;
        #endregion

        #region Properties
        public string ActivityTypeKey
        {
            get { return _ActivityTypeKey; }
            set
            {
                _ActivityTypeKey = value;
                OnPropertyChanged(() => ActivityTypeKey);
            }
        }
        public string ActivityType
        {
            get { return _ActivityType; }
            set { _ActivityType = value; }
        }
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        #endregion

        class ActivityTypeMasterValidator : AbstractValidator<ActivityTypeMaster>
        {
            public ActivityTypeMasterValidator()
            {
                RuleFor(obj => obj.ActivityTypeKey).NotNull();
                RuleFor(obj => obj.ActivityType).NotNull();
                RuleFor(obj => obj.CreatedDate).NotNull();
                RuleFor(obj => obj.CreatedBy).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityTypeMasterValidator();
        }
    }
}
