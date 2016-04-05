using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts.DataContracts
{
    [DataContract(Namespace = "wadado.in")]
    public class EmailDataContract
    {
        [DataMember]
        public string ActivityName { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DateTime BookingDate { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public int Participants { get; set; }
        [DataMember]
        public int ChildParticipants { get; set; }
        [DataMember]
        public string BookingNumber { get; set; }
        [DataMember]
        public decimal PaymentAmount { get; set; }
        [DataMember]
        public string PriceOption { get; set; }
        [DataMember]
        public string CancellationPolicy { get; set; }
        [DataMember]
        public string ContactNumber { get; set; }
        [DataMember]
        public string Restrictions { get; set; }
        [DataMember]
        public string ThingsToCarry { get; set; }
        [DataMember]
        public string Duration { get; set; }
    }
}
