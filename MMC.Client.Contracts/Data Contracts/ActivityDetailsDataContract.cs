using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts.Data_Contracts
{
    [DataContract]
    public class ActivityDetailsDataContract : DataContractBase
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string LocationCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int NumAdults { get; set; }
        [DataMember]
        public int NumChildren { get; set; }
        [DataMember]
        public string DefaultImageURL { get; set; }
        [DataMember]
        public string ImageURL { get; set; }
        [DataMember]
        public List<string> ActivityImages { get; set; }
        [DataMember]
        public bool PermitRequired { get; set; }
        [DataMember]
        public string ActivityCategory { get; set; }
        [DataMember]
        public string ActivitySubCategory { get; set; }
        [DataMember]
        public decimal UserRating { get; set; }
        [DataMember]
        public decimal DifficultyRating { get; set; }
        [DataMember]
        public string CancellationPolicy { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public List<string> Reviews { get; set; }
    }
}
