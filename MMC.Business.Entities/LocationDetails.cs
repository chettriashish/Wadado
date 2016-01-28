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
    public class LocationDetails : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public string LocationDetailsKey { get; set; }
        [DataMember]
        public string LocationKey { get; set; }
        [DataMember]
        public string ActivityHeader { get; set; }
        [DataMember]
        public string ActivityDescription { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }

        public string EntityId
        {
            get
            {
                return LocationDetailsKey;
            }
            set
            {
                LocationDetailsKey = value;
            }
        }
    }
}
