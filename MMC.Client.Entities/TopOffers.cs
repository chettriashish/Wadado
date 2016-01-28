using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.Runtime.Serialization;

namespace MMC.Client.Entities
{
    [DataContract(Namespace = "wadado.in")]
    public class TopOffers : ObjectBase
    {
        #region Private Variables
        private string _TopOffersKey;
        private string _LocationKey;
        private string _ActivityKey;
        private decimal _Discount;
        private DateTime _OfferStartDate;
        private DateTime _OfferEndDate;
        private bool _ShowOnHomePage;
        private string _ImageUrl; 
        #endregion

        #region Properties
        public string TopOffersKey
        {
            get { return _TopOffersKey; }
            set
            {
                _TopOffersKey = value;
                OnPropertyChanged(() => TopOffersKey);
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
        public string ActivityKey
        {
            get { return _ActivityKey; }
            set { _ActivityKey = value; }
        }
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        public DateTime OfferStartDate
        {
            get { return _OfferStartDate; }
            set { _OfferStartDate = value; }
        }
        public DateTime OfferEndDate
        {
            get { return _OfferEndDate; }
            set { _OfferEndDate = value; }
        }

        public bool ShowOnHomePage
        {
            get { return _ShowOnHomePage; }
            set { _ShowOnHomePage = value; }
        }

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        } 
        #endregion
        class TopOffersValidator : AbstractValidator<TopOffers>
        {
            public TopOffersValidator()
            {
                RuleFor(obj => obj.TopOffersKey).NotNull();                
                RuleFor(obj => obj.ImageUrl).NotNull();
                RuleFor(obj => obj.LocationKey).NotNull();
                RuleFor(obj => obj.OfferStartDate).NotNull();
                RuleFor(obj => obj.OfferEndDate).NotNull();
                RuleFor(obj => obj.ActivityKey).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new TopOffersValidator();
        }
    }
}
