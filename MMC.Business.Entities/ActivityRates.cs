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
    public class ActivityRates : EntityBase, IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string ActivityRatesKey { get; set; }

        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public decimal AdultOffSeasonRate { get; set; }
        [DataMember]
        public decimal ChildOffSeasonRate { get; set; }
        [DataMember]
        public decimal AdultSeasonRate { get; set; }
        [DataMember]
        public decimal ChildSeasonRate { get; set; }
        
        #endregion
        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityRatesKey;
            }
            set
            {
                ActivityRatesKey = value;
            }
        }
    }
}
