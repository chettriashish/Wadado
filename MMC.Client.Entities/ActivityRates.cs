using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class ActivityRates : ObjectBase
    {
        #region Private variables
        private string _ActivityRatesKey;
        private string _ActivityKey;
        private decimal _AdultOffSeasonRate;
        private decimal _ChildOffSeasonRate;
        private decimal _AdultSeasonRate;
        private decimal _ChildSeasonRate; 
        #endregion

        #region Properties
        public string ActivityRatesKey
        {
            get { return _ActivityRatesKey; }
            set
            {
                _ActivityRatesKey = value;
                OnPropertyChanged(() => ActivityRatesKey);
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
        public decimal AdultOffSeasonRate
        {
            get { return _AdultOffSeasonRate; }
            set
            {
                _AdultOffSeasonRate = value;
                OnPropertyChanged(() => AdultOffSeasonRate);
            }
        }
        public decimal ChildOffSeasonRate
        {
            get { return _ChildOffSeasonRate; }
            set
            {
                _ChildOffSeasonRate = value;
                OnPropertyChanged(() => ChildOffSeasonRate);
            }
        }
        public decimal AdultSeasonRate
        {
            get { return _AdultSeasonRate; }
            set
            {
                _AdultSeasonRate = value;
                OnPropertyChanged(() => AdultSeasonRate);
            }
        }
        public decimal ChildSeasonRate
        {
            get { return _ChildSeasonRate; }
            set
            {
                _ChildSeasonRate = value;
                OnPropertyChanged(() => ChildSeasonRate);
            }
        } 
        #endregion
        class ActivityRatesValidator:AbstractValidator<ActivityRates>
        {
            public ActivityRatesValidator()
            {
                RuleFor(obj => obj.ActivityRatesKey).NotNull();
                RuleFor(obj => obj.ActivityKey).NotNull();
                RuleFor(obj => obj.AdultSeasonRate).GreaterThan(0);
            }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityRatesValidator();
        }
    }
}
