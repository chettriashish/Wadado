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
    public class TouristSeasons : ObjectBase
    {
        #region Private variables
        private string _TouristSeasonKey;
        private string _LocationKey;
        private DateTime _SeasonStartDate;
        private DateTime _SeasonEndDate;
        private string _CreatedBy;
        private DateTime _CreatedOn;
        
        #endregion

        #region Properties
        public string TouristSeasonKey
        {
            get { return _TouristSeasonKey; }
            set
            {
                _TouristSeasonKey = value;
                OnPropertyChanged(() => TouristSeasonKey);
            }
        }
        public string LocationKey
        {
            get { return _LocationKey; }
            set
            {
                _LocationKey = value;
                OnPropertyChanged(() => LocationKey);
            }
        }
        public DateTime SeasonStartDate
        {
            get { return _SeasonStartDate; }
            set
            {
                _SeasonStartDate = value;
                OnPropertyChanged(() => SeasonStartDate);
            }
        }
        public DateTime SeasonEndDate
        {
            get { return _SeasonEndDate; }
            set
            {
                _SeasonEndDate = value;
                OnPropertyChanged(() => SeasonEndDate);
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
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set
            {
                _CreatedOn = value;
                OnPropertyChanged(() => CreatedOn);
            }
        } 
        #endregion

        class TouristSeasonsValidator : AbstractValidator<TouristSeasons>
        {
            public TouristSeasonsValidator()
            {
                RuleFor(obj => obj.TouristSeasonKey).NotNull();
                RuleFor(obj => obj.SeasonEndDate).NotNull();
                RuleFor(obj => obj.SeasonStartDate).NotNull();
                RuleFor(obj => obj.LocationKey).NotNull();
                RuleFor(obj => obj.CreatedBy).NotNull();
                RuleFor(obj => obj.CreatedOn).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new TouristSeasonsValidator();
        }
    }
}
