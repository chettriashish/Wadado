using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Entities
{
    public class ActivityPriceMapping: EntityBase, IIdentifiableEntity
    {
        #region Properties
        public string ActivityPricingKey { get; set; }
        public string ActivityKey { get; set; }
        public string OptionDescription { get; set; }
        public decimal PriceForAdults { get; set; }
        public decimal PriceForChildren { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } 
        #endregion

        public string EntityId
        {
            get
            {
                return ActivityPricingKey;
            }
            set
            {
                ActivityPricingKey = value;
            }
        }
    }
}
