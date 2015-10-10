using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{
    public class LocationsMaster : ObjectBase
    {
        private string _LocationKey;
        private string _LocationName;
        private string _LocationImage;
        private string _country;

        public string LocationKey
        {
            get { return _LocationKey; }
            set
            {
                _LocationKey = value;
                OnPropertyChanged(() => LocationKey);
            }
        }

        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value;
                OnPropertyChanged(() => LocationName);
            }
        }

        public string LocationImage
        {
            get { return _LocationImage; }
            set
            {
                _LocationImage = value;
                OnPropertyChanged(() => LocationImage);
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(() => Country);
            }
        }


        class LocationMasterValidator : AbstractValidator<LocationsMaster>
        {
            public LocationMasterValidator()
            {
                RuleFor(obj => obj.LocationKey).NotEmpty();
                RuleFor(obj => obj.LocationName).NotEmpty();
                RuleFor(obj => obj.LocationImage).NotEmpty();
                RuleFor(obj => obj.Country).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new LocationMasterValidator();
        }
    }
}
