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
    public class ActivityCategoryMaster : EntityBase, IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string ActivityCategoryKey { get; set; }
        [DataMember]
        public string ActivityCategory { get; set; }
        
        #endregion
        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityCategoryKey;
            }
            set
            {
                ActivityCategoryKey = value;
            }
        }
    }
}
