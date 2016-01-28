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
    public class ActivityHolidays:EntityBase,IIdentifiableEntity
    {        
        #region Properties
        [DataMember]
        public string ActivityHolidayKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public int NumberOfDays { get; set; }
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
                return ActivityHolidayKey;
            }
            set
            {
                ActivityHolidayKey = value;
            }
        }
    }
}
