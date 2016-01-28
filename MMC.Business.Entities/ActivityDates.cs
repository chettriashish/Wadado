using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Business.Entities
{
    [DataContract(Namespace = "wadado.in")]
    public class ActivityDates : EntityBase, IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string ActivityDatesKey { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string Time { get; set; } 
        #endregion
        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityDatesKey;
            }
            set
            {
                ActivityDatesKey = value;
            }
        }
    }
}
