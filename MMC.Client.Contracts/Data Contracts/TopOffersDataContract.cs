using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts.DataContracts
{
    [DataContract(Namespace = "wadado.in")]
    public class TopOffersDataContract
    {
        [DataMember]
        public string TopOffersKey { get; set; }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public decimal Rating { get; set; }
        [DataMember]
        public string OfferType { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public decimal Discount { get; set; }
        [DataMember]
        public string ImageURL { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public DateTime OfferEndDate { get; set; }
        public DateTime OfferStartDate { get; set; }
    }
}
