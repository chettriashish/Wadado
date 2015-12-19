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
    public class UserApplicationActivityDetails: EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public string SessionKey { get; set; }
        [DataMember]
        public DateTime LastUpdateSessionTime { get; set; }
        [DataMember]
        public bool UserLoggedIn { get; set; }
        [DataMember]
        public string LoginMethod { get; set; }
        [DataMember]
        public string EntityId
        {
            get
            {
                return SessionKey;
            }
            set
            {
                SessionKey = value;
            }
        }
    }
}
