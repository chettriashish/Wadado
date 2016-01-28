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
    public class ActivityDiscount : ObjectBase
    {
        #region Private Variables
        private string _ActivityDiscountKey;
        private string _ActivityKey;
        private decimal _DiscountPercentage;
        private DateTime _DiscountStartDate;
        private DateTime _DiscountEndDate;
        private DateTime _CreatedOn;
        private string _CreatedBy; 
        #endregion

        #region Properties
        public string ActivityDiscountKey
        {
            get { return _ActivityDiscountKey; }
            set
            {
                _ActivityDiscountKey = value;
                OnPropertyChanged(() => ActivityDiscountKey);
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
        public decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set
            {
                _DiscountPercentage = value;
                OnPropertyChanged(() => DiscountPercentage);
            }
        }
        public DateTime DiscountStartDate
        {
            get { return _DiscountStartDate; }
            set
            {
                _DiscountStartDate = value;
                OnPropertyChanged(() => DiscountStartDate);
            }
        }
        public DateTime DiscountEndDate
        {
            get { return _DiscountEndDate; }
            set
            {
                _DiscountEndDate = value;
                OnPropertyChanged(() => DiscountEndDate);
            }
        }
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set
            {
                _CreatedOn = value;
                OnPropertyChanged(() => CreatedOn);
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

        class ActivityDiscountValidator:AbstractValidator<ActivityDiscount>
        {
            public ActivityDiscountValidator()
            {
                RuleFor(obj => obj.ActivityDiscountKey).NotEmpty();
                RuleFor(obj => obj.ActivityKey).NotEmpty();
                RuleFor(obj => obj.DiscountStartDate).NotNull();
                RuleFor(obj => obj.DiscountEndDate).NotNull();
                RuleFor(obj => obj.DiscountPercentage).GreaterThan(0);
                RuleFor(obj => obj.CreatedOn).NotNull();
                RuleFor(obj => obj.CreatedBy).NotEmpty();
            }
        }
        protected override IValidator GetValidator()
        {
            return new ActivityDiscountValidator();
        }
    }
}
