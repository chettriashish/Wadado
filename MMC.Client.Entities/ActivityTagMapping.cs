using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityTagMapping : ObjectBase
    {
        private string _ActivityTagKey;
        private string _ActivityKey;
        private string _Tag;
        public string ActivityTagKey
        {
            get { return _ActivityTagKey; }
            set
            {
                _ActivityTagKey = value;
                OnPropertyChanged(() => ActivityTagKey);
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

        public string Tag
        {
            get { return _Tag; }
            set
            {
                _Tag = value;
                OnPropertyChanged(() => Tag);
            }
        }

        class ActivityTagMappingValidator : AbstractValidator<ActivityTagMapping>
        {
            public ActivityTagMappingValidator()
            {
                RuleFor(obj => obj.ActivityTagKey).NotEmpty();
                RuleFor(obj => obj.ActivityKey).NotEmpty();
                RuleFor(obj => obj.Tag).NotEmpty();                
            }
        }
        protected override IValidator GetValidator()
        {
            return new ActivityTagMappingValidator();
        }
    }
}
