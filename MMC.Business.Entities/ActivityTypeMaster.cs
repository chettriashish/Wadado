using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Entities
{    
    public class ActivityTypeMaster:EntityBase,IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string ActivityTypeKey { get; set; }
        [DataMember]
        public string ActivityType { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string CreatedDate { get; set; } 
        #endregion

        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityTypeKey;
            }
            set
            {
                ActivityTypeKey = value;
            }
        }
    }
}
