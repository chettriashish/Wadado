using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Entities
{
    public class UserCompanyMapping : EntityBase, IIdentifiableEntity
    {
        #region Properties
        public string UserCompanyKey { get; set; }
        public string UserKey { get; set; }
        public string CompanyKey { get; set; }
        public bool IsDeleted { get; set; } 
        #endregion
        public string EntityId
        {
            get
            {
                return UserCompanyKey;
            }
            set
            {
                UserCompanyKey = value;
            }
        }
    }
}
