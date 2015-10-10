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
    public class Account:EntityBase,IIdentifiableEntity, IAccountOwnedEntity
    {
        #region Properties
        [DataMember]
        public string AccountKey { get; set; }
        [DataMember]
        public string SessionKey { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string AccountTypeKey { get; set; } 
        #endregion

        [DataMember]
        public string EntityId
        {
            get
            {
                return AccountKey;
            }
            set
            {
                AccountKey = value;
            }
        }
        public string OwnerAccountId
        {
            get { return Email; }
        }
    }
}
