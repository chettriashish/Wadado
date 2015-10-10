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
    public class CompanyMaster:EntityBase,IIdentifiableEntity
    {        
        [DataMember]
        #region Properties
        public string CompanyKey { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string TelephoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public decimal Rating { get; set; }
        [DataMember]
        public string ContactPerson { get; set; }
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
                return CompanyKey;
            }
            set
            {
                CompanyKey = value;
            }
        }
    }
}
