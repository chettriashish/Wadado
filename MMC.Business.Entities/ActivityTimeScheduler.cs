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
    public class ActivityTimeScheduler:EntityBase,IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string ActivityTimeSchedulerKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string ActivityTime { get; set; } 
        #endregion
        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityTimeSchedulerKey;
            }
            set
            {
                ActivityTimeSchedulerKey = value;
            }
        }
    }
}
