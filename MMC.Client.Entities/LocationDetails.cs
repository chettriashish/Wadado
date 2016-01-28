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
    public class LocationDetails : ObjectBase
    {
        #region Private variables
        private string _LocationDetailsKey;
        private string _LocationKey;
        private string _ActivityHeader;
        private string _ActivityDescription;
        private bool _IsDeleted; 
        #endregion

        #region Properties
        public string LocationDetailsKey
        {
            get { return _LocationDetailsKey; }
            set
            {
                _LocationDetailsKey = value;
                OnPropertyChanged(() => LocationDetailsKey);
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


        public string ActivityHeader
        {
            get { return _ActivityHeader; }
            set
            {
                _ActivityHeader = value;
                OnPropertyChanged(() => ActivityHeader);
            }
        }



        public string ActivityDescription
        {
            get { return _ActivityDescription; }
            set
            {
                _ActivityDescription = value;
                OnPropertyChanged(() => ActivityDescription);
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
        #endregion
        class LocationDetailsValidator : AbstractValidator<LocationDetails>
        {
            public LocationDetailsValidator()
            {
                RuleFor(obj => obj.LocationDetailsKey).NotNull();
                RuleFor(obj => obj.LocationKey).NotNull();
                RuleFor(obj => obj.ActivityHeader).NotNull();
                RuleFor(obj => obj.ActivityDescription).NotNull();
                RuleFor(obj => obj.IsDeleted).NotNull();                
            }
        }

        protected override IValidator GetValidator()
        {
            return new LocationDetailsValidator();
        }
    }
}
