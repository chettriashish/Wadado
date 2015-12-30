using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts.DataContracts
{
    [DataContract(Namespace = "wadado.in")]
    public class ActivitySummaryDataContract : DataContractBase
    {
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string ActivityName { get; set; }
        [DataMember]
        public bool IsSpecialOffer { get; set; }
        [DataMember]
        public bool IsTopTrending { get; set; }
        [DataMember]
        public string ThumbNailURL { get; set; }
        [DataMember]
        public string ActivityCategory { get; set; }
        [DataMember]
        public string Location { get; set; }
    }
}
