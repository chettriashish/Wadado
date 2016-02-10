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
    public class LocationsMaster : ObjectBase
    {
        private string _LocationKey;
        private string _LocationName;
        private string _LocationImage;
        private string _country;
        private string _Description;
        private string _BestTimeToVisit;
        private string _LatLng;
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

        public string BestTimeToVisit
        {
            get { return _BestTimeToVisit; }
            set
            {
                _BestTimeToVisit = value;
                OnPropertyChanged(() => BestTimeToVisit);
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(() => Description);
            }
        }       

        public string LatLng
        {
            get { return _LatLng; }
            set
            {
                _LatLng = value;
                OnPropertyChanged(() => LatLng);
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
                RuleFor(obj => obj.BestTimeToVisit).NotEmpty();
                RuleFor(obj => obj.Description).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new LocationMasterValidator();
        }
    }
}
