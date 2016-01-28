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
    public class ActivityTimeScheduler : ObjectBase
    {
        #region Private variable
        private string _ActivityTimeSchedulerKey;
        private string _ActivityKey;
        private string _ActivityTime; 
        #endregion

        #region Properties
        public string ActivityTimeSchedulerKey
        {
            get { return _ActivityTimeSchedulerKey; }
            set
            {
                _ActivityTimeSchedulerKey = value;
                OnPropertyChanged(() => ActivityTimeSchedulerKey);
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

        public string ActivityTime
        {
            get { return _ActivityTime; }
            set
            {
                _ActivityTime = value;
                OnPropertyChanged(() => ActivityTime);
            }
        } 
        #endregion
        class ActivityTimeSchedulerValidator : AbstractValidator<ActivityTimeScheduler>
        {
            public ActivityTimeSchedulerValidator()
            {
                RuleFor(obj => obj.ActivityTimeSchedulerKey).NotNull();
                RuleFor(obj => obj.ActivityKey).NotNull();
                RuleFor(obj => obj.ActivityTime).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityTimeSchedulerValidator();
        }
    }
}
