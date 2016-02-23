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
    public class LocationsMaster : EntityBase, IIdentifiableEntity
    {        
        #region Properties
        [DataMember]
        public string LocationKey { get; set; }
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string LocationImage { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string BestTimeToVisit { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string LatLng { get; set; }
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
