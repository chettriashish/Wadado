using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts.DataContracts
{
    [DataContract(Namespace = "wadado.in")]
    public class UserSessionDataContract
    {
        [DataMember]
        public string SessionKey { get; set; }
        [DataMember]
        public bool IsUserLoggedIn { get; set; }
        [DataMember]
        public string LoginMethod { get; set; }
        [DataMember]
        public string GuestKey { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime DOB { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }        
    }
}
