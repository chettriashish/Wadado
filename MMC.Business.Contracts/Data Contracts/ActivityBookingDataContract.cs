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
    public class ActivityBookingDataContract : DataContractBase
    {
        #region Properties
        [DataMember]
        public string ActivityBookingKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string ActivityName { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string SessionKey { get; set; }
        [DataMember]
        public string GuestKey { get; set; }
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
        public bool IsDeleted { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public bool IsConfirmed { get; set; }
        [DataMember]
        public bool IsPaymentComplete { get; set; }
        [DataMember]
        public DateTime ConfirmationDate { get; set; }
        [DataMember]
        public string ConfirmedBy { get; set; }
        [DataMember]
        public decimal PaymentAmount { get; set; }
        [DataMember]
        public bool IsCancelled { get; set; }
        [DataMember]
        public decimal RefundAmount { get; set; }
        [DataMember]
        public string BookingNumber { get; set; }
        [DataMember]
        public string ThumbnailImage { get; set; }
        #endregion
    }
}
