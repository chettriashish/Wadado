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
    public class LocationsMaster : EntityBase, IIdentifiableEntity
    {
        public ExtensionDataObject ExtensionData { get; set; }
        #region Properties
        [DataMember]
        public string LocationKey { get; set; }
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string LocationImage { get; set; }
        public string Country { get; set; }
        public string Season1Start { get; set; }
        public string Season1End { get; set; }
        public string Season2Start { get; set; }
        public string Season2End { get; set; }
        public string Season3Start { get; set; }
        public string Season3End { get; set; }

        #endregion

        [DataMember]
        public string EntityId
        {
            get
            {
                return LocationKey;
            }
            set
            {
                LocationKey = value;
            }
        }
    }
}
