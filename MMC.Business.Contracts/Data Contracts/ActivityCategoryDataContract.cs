using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts.DataContracts
{
    [DataContract(Namespace = "wadado.in")]
    public class ActivityCategoryDataContract
    {
        [DataMember]
        public int ActivityCount { get; set; }
        [DataMember]
        public string ActivityName { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string ImageURL { get; set; }
    }
}
