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
        public string LocationName { get; set; }
        [DataMember]
        public string ImageURL { get; set; }
        [DataMember]
        public string LocationKey { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string MustEat { get; set; }
        [DataMember]
        public string MustDrink { get; set; }
        [DataMember]
        public string GettingAround { get; set; }           
        [DataMember]
        public IEnumerable<ActivityCategoryDataContract> AllActivities { get; set; }
        [DataMember]
        public string DefaultActivityCategoryKey { get; set; }
        [DataMember]
        public string LatLong { get; set; }
        [DataMember]
        public string MapIconURL { get; set; }
        [DataMember]
        public IEnumerable<TopOffersDataContract> TopOffersForLocation { get; set; }
        #endregion
    }
}
