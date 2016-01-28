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
    public class GuestInformationMaster:EntityBase,IIdentifiableEntity
    {        
        #region Properties
        [DataMember]
        public string GuestKey { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime DOB { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string AccountKey { get; set; }
        #endregion
        [DataMember]
        public string EntityId
        {
            get
            {
                return GuestKey;
            }
            set
            {
                GuestKey = value;
            }
        }
    }
}
