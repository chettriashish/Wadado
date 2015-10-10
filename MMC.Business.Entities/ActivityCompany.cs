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
    public class ActivityCompany:EntityBase,IIdentifiableEntity
    {   
        #region Properties
        [DataMember]
        public string ActivityCompanyKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string CompanyKey { get; set; }
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
                return ActivityCompanyKey;
            }
            set
            {
                ActivityCompanyKey = value;
            }
        }
    }
}
