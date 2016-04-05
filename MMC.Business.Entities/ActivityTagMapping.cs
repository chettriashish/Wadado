using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Entities
{
    public class ActivityTagMapping : EntityBase, IIdentifiableEntity
    {
        #region Properties
        public string ActivityTagKey { get; set; }
        public string ActivityKey { get; set; }
        public string Tag { get; set; }
        #endregion
        public string EntityId
        {
            get
            {
                return ActivityTagKey;
            }
            set
            {
                ActivityTagKey = value;
            }
        }
    }
}
