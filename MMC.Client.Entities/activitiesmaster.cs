using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common.Core;
using FluentValidation;
using FluentValidation.Validators;
namespace MMC.Client.Entities
{
    public class ActivitiesMaster : ObjectBase
    {
        #region Private Variables
        private string _activitesKey;
        private string _ActivityTypeKey;
        private string _name;
        private string _description;
        private decimal _cost;
        private string _address;
        private string _duration;
        private string _additionalFeatures;
        private string _pickup;
        private int _maxPeople;
        private int _minPeople;
        private string _thingsToCarry;
        private string _included;
        private bool _isPermitRequired;
        private decimal _difficultyRating;
        private string _advice;
        private string _cancellationPolicy;
        private string _ourReview;
        private DateTime _createdDate;
        private string _createdBy;
        private bool _isValidated;
        #endregion

        #region Properties
        public string ActivitesKey
        {
            get { return _activitesKey; }
            set
            {
                if (_activitesKey != value)
                {
                    _activitesKey = value;
                    OnPropertyChanged(() => ActivitesKey, true);
                }
            }
        }
        public string ActivityTypeKey
        {
            get { return _ActivityTypeKey; }
            set
            {
                _ActivityTypeKey = value;
                OnPropertyChanged(() => ActivityTypeKey, true);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }


        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }


        public decimal Cost
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                {
                    _cost = value;
                    OnPropertyChanged(() => Cost);
                }
            }
        }


        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(() => Address);
                }
            }
        }


        public string Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged(() => Duration);
                }
            }
        }


        public string AdditionalFeatures
        {
            get { return _additionalFeatures; }
            set
            {
                if (_additionalFeatures != value)
                {
                    _additionalFeatures = value;
                    OnPropertyChanged(() => AdditionalFeatures);
                }
            }
        }

        public string Pickup
        {
            get { return _pickup; }
            set
            {
                if (_pickup != value)
                {
                    _pickup = value;
                    OnPropertyChanged(() => Pickup);
                }
            }
        }


        public int MaxPeople
        {
            get { return _maxPeople; }
            set
            {
                if (_maxPeople != value)
                {
                    _maxPeople = value;
                    OnPropertyChanged(() => MaxPeople);
                }
            }
        }

        public int MinPeople
        {
            get { return _minPeople; }
            set
            {
                if (_minPeople != value)
                {
                    _minPeople = value;
                    OnPropertyChanged(() => MinPeople);
                }
            }
        }

        public string ThingsToCarry
        {
            get { return _thingsToCarry; }
            set
            {
                if (_thingsToCarry != value)
                {
                    _thingsToCarry = value;
                    OnPropertyChanged(() => ThingsToCarry);
                }
            }
        }

        public string Included
        {
            get { return _included; }
            set
            {
                if (_included != value)
                {
                    _included = value;
                    OnPropertyChanged(() => Included);
                }
            }
        }

        public bool IsPermitRequired
        {
            get { return _isPermitRequired; }
            set
            {
                if (_isPermitRequired != value)
                {
                    _isPermitRequired = value;
                    OnPropertyChanged(() => IsPermitRequired);
                }
            }
        }
        public decimal DifficultyRating
        {
            get { return _difficultyRating; }
            set
            {
                if (_difficultyRating != value)
                {
                    _difficultyRating = value;
                    OnPropertyChanged(() => DifficultyRating);
                }

            }
        }

        public string Advice
        {
            get { return _advice; }
            set
            {
                if (_advice != value)
                {
                    _advice = value;
                    OnPropertyChanged(() => Advice);
                }
            }
        }

        public string CancellationPolicy
        {
            get { return _cancellationPolicy; }
            set
            {
                if (_cancellationPolicy != value)
                {
                    _cancellationPolicy = value;
                    OnPropertyChanged(() => CancellationPolicy);
                }
            }
        }

        public string OurReview
        {
            get { return _ourReview; }
            set
            {
                if (_ourReview != value)
                {
                    _ourReview = value;
                    OnPropertyChanged(() => OurReview);
                }
            }
        }
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set
            {
                if (_createdDate != value)
                {
                    _createdDate = value;
                    OnPropertyChanged(() => CreatedDate);
                }

            }
        }
        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (_createdBy != value)
                {
                    _createdBy = value;
                    OnPropertyChanged(() => CreatedBy);
                }
            }
        }

        public bool IsValidated
        {
            get { return _isValidated; }
            set
            {
                if (_isValidated != value)
                {
                    _isValidated = value;
                    OnPropertyChanged(() => IsValidated);
                }
            }
        }

        #endregion
        /// <summary>
        /// This class is used to validate the entity
        /// </summary>
        class ActivitiesMasterValidator : AbstractValidator<ActivitiesMaster>
        {
            public ActivitiesMasterValidator()
            {
                RuleFor(obj => obj.ActivitesKey).NotEmpty();
                RuleFor(obj => obj.ActivityTypeKey).NotEmpty();
                RuleFor(obj => obj.Pickup).NotEmpty();
                RuleFor(obj => obj.Cost).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.Description).NotEmpty();
                RuleFor(obj => obj.Name).NotEmpty();
                RuleFor(obj => obj.MaxPeople).GreaterThanOrEqualTo(1);
                RuleFor(obj => obj.MinPeople).GreaterThanOrEqualTo(1);
                RuleFor(obj => obj.CancellationPolicy).NotEmpty();
                RuleFor(obj => obj.CreatedDate).NotNull();
                RuleFor(obj => obj.CreatedBy).NotEmpty();
            }
        }
        /// <summary>
        /// Adding validation to an object
        /// </summary>
        /// <returns></returns>
        protected override IValidator GetValidator()
        {
            return new ActivitiesMasterValidator();
        }
    }
}
