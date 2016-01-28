using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Entities
{
    [DataContract(Namespace = "wadado.in")]
    public class TopOffers : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public string TopOffersKey { get; set; }
        [DataMember]
        public string LocationKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public decimal Discount { get; set; }
        [DataMember]
        public DateTime OfferStartDate { get; set; }
        [DataMember]
        public DateTime OfferEndDate { get; set; }
        [DataMember]
        public bool ShowOnHomePage { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string EntityId
        {
            get
            {
                return TopOffersKey;
            }
            set
            {
                TopOffersKey = value;
            }
        }       
    }
}
