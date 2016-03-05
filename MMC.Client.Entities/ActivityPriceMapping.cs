using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityPriceMapping : ObjectBase
    {
        #region Private variables
        private string _ActivityPricingKey;
        private string _ActivityKey;
        private string _OptionDescription;
        private decimal _PriceForAdults;
        private decimal _PriceForChildren;
        private bool _IsDeleted;
        private DateTime? _CreatedDate;
        private string _CreatedBy;
        #endregion

        #region Properties
        public string ActivityPricingKey
        {
            get { return _ActivityPricingKey; }
            set
            {
                _ActivityPricingKey = value;
                OnPropertyChanged(() => ActivityPricingKey);
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
        public string OptionDescription
        {
            get { return _OptionDescription; }
            set
            {
                _OptionDescription = value;
                OnPropertyChanged(() => OptionDescription);
            }
        }       

        public decimal PriceForAdults
        {
            get { return _PriceForAdults; }
            set
            {
                _PriceForAdults = value;
                OnPropertyChanged(() => PriceForAdults);
            }
        }

        public decimal PriceForChildren
        {
            get { return _PriceForChildren; }
            set
            {
                _PriceForChildren = value;
                OnPropertyChanged(() => PriceForChildren);
            }
        }

        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                _IsDeleted = value;
                OnPropertyChanged(() => IsDeleted);
            }
        }

        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                _CreatedDate = value;
                OnPropertyChanged(() => CreatedDate);
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

        class ActivityPriceMappingValidator : AbstractValidator<ActivityPriceMapping>
        {
            public ActivityPriceMappingValidator()
            {
                RuleFor(obj => obj.ActivityPricingKey).NotEmpty();
                RuleFor(obj => obj.ActivityKey).NotEmpty();
                RuleFor(obj => obj.PriceForAdults).NotNull();
                RuleFor(obj => obj.PriceForChildren).NotNull();
                RuleFor(obj => obj.OptionDescription).NotEmpty();
            }
        }
        protected override IValidator GetValidator()
        {
            return new ActivityPriceMappingValidator();
        }
    }
}
