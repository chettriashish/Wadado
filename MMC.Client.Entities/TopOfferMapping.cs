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
    public class TopOfferMapping : ObjectBase
    {
        #region Private variables
        private int _TopOfferMappingKey;
        private string _TopOfferKey;
        private string _MappingKey;
        private string _MappingType;
        private decimal _Discount;
        private bool _IsDeleted; 
        #endregion

        #region Properties
        public int TopOfferMappingKey
        {
            get { return _TopOfferMappingKey; }
            set
            {
                _TopOfferMappingKey = value;
                OnPropertyChanged(() => TopOfferMappingKey);
            }
        }
        public string TopOfferKey
        {
            get { return _TopOfferKey; }
            set
            {
                _TopOfferKey = value;
                OnPropertyChanged(() => TopOfferKey);
            }
        }

        public string MappingKey
        {
            get { return _MappingKey; }
            set
            {
                _MappingKey = value;
                OnPropertyChanged(() => MappingKey);
            }
        }
        public string MappingType
        {
            get { return _MappingType; }
            set
            {
                _MappingType = value;
                OnPropertyChanged(() => MappingType);
            }
        }


        public decimal Discount
        {
            get { return _Discount; }
            set
            {
                _Discount = value;
                OnPropertyChanged(() => Discount);
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

        class TopOfferMappingValidator : AbstractValidator<TopOfferMapping>
        {
            public TopOfferMappingValidator()
            {
                RuleFor(obj => obj.TopOfferKey).NotNull();
                RuleFor(obj => obj.MappingKey).NotNull();
                RuleFor(obj => obj.MappingType).NotNull();                
            }
        }

        protected override IValidator GetValidator()
        {
            return new TopOfferMappingValidator();
        }
    }
}
