using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts.DataContracts
{
    [DataContract(Namespace = "wadado.in")]
    public class LocationDetailsDataContract
    {
        #region Properties
        [DataMember]
        public IEnumerable<LocationsMaster> AllLocations { get; set; }
        [DataMember]
        public IEnumerable<LocationDetails> SelectedLoctionDetails { get; set; }
        [DataMember]
        public IEnumerable<ActivitySummaryDataContract> AllActivitiesForLocation { get; set; }
        [DataMember]
        public IEnumerable<TopOffers> TopOffersForLocation { get; set; } 
        #endregion
    }
}
