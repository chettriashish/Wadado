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
    [DataContract]
    public class ActivityTypeCategory : EntityBase, IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string ActivityTypeCategoryKey { get; set; }
        [DataMember]
        public string ActivityTypeKey { get; set; }
        [DataMember]
        public string ActivityCategoryKey { get; set; }
        public bool IsPrimary { get; set; }
        #endregion

        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivityTypeCategoryKey;
            }
            set
            {
                ActivityTypeCategoryKey = value;
            }
        }
    }
}
