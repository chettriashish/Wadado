using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts.DataContracts
{
    [DataContract]
    public class CustomerActivitiesBookingData:DataContractBase
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string ActivityName { get; set; }
        [DataMember]
        public DateTime BookingDate { get; set; }
        [DataMember]
        public string BookingTime { get; set; }
        [DataMember]
        public decimal BookingCost { get; set; }
        [DataMember]
        public int NumberOfAdults { get; set; }
        [DataMember]
        public int NumberOfChildren { get; set; }
        [DataMember]
        public string ActivityCompanyName { get; set; }
        [DataMember]
        public string ActivityLocation { get; set; }        
    }
}
