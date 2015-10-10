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
    public class ActivityDayScheduler:EntityBase,IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string ActivityDaySchedulerKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public bool IsSunday { get; set; }
        [DataMember]
        public bool IsMonday { get; set; }
        [DataMember]
        public bool IsTuesday { get; set; }
        [DataMember]
        public bool IsWednesday { get; set; }
        [DataMember]
        public bool IsThursday { get; set; }
        [DataMember]
        public bool IsFriday { get; set; }
        [DataMember]
        public bool IsSaturday { get; set; } 
        #endregion
        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityDaySchedulerKey;
            }
            set
            {
                ActivityDaySchedulerKey = value;
            }
        }
    }
}
