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
        private string _season1Start;
        private string _season2Start;
        private string _season3Start;
        private string _season1End;
        private string _season2End;
        private string _season3End;
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

        public string Season1Start
        {
            get { return _season1Start; }
            set
            {
                _season1Start = value;
                OnPropertyChanged(() => Season1Start);
            }
        }       

        public string Season1End
        {
            get { return _season1End; }
            set
            {
                _season1End = value;
                OnPropertyChanged(() => Season1End);
            }
        }


        public string Season2Start
        {
            get { return _season2Start; }
            set
            {
                _season2Start = value;
                OnPropertyChanged(() => Season2Start);
            }
        }

        public string Season2End
        {
            get { return _season2End; }
            set
            {
                _season2End = value;
                OnPropertyChanged(() => Season2End);
            }
        }
        public string Season3Start
        {
            get { return _season3Start; }
            set
            {
                _season3Start = value;
                OnPropertyChanged(() => Season3Start);
            }
        }

        public string Season3End
        {
            get { return _season3End; }
            set
            {
                _season3End = value;
                OnPropertyChanged(() => Season3End);
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
