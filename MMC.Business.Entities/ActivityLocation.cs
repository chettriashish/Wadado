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
    public class ActivityLocation:EntityBase,IIdentifiableEntity
    {        
        #region Properties        
        [DataMember]
        public string ActivityLocationKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string LocationKey { get; set; }
        [DataMember]
        public DateTime CreatedOn { get; set; }
        [DataMember]
        public string CreatedBy { get; set; } 
        #endregion

        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityLocationKey;
            }
            set
            {
                ActivityLocationKey = value;
            }
        }
    }
}
