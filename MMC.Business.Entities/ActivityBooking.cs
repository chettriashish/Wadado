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
    [DataContract]
    public class ActivityBooking : EntityBase, IIdentifiableEntity
    {       

        #region Properties
        [DataMember]
        public string ActivityBookingKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string SessionKey { get; set; }
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
        #endregion

        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityBookingKey;
            }
            set
            {
                ActivityBookingKey = value;
            }
        }
    }
}
