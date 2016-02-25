using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts.DataContracts
{
    [DataContract(Namespace="wadado.in")]
    public class ActivityDetailsDataContract : DataContractBase
    {
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string LocationCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Duration { get; set; }
        [DataMember]
        public int NumAdults { get; set; }
        [DataMember]
        public int NumChildren { get; set; }
        [DataMember]
        public int MinPeople { get; set; }
        [DataMember]
        public int MaxPeople { get; set; }
        [DataMember]
        public string DefaultImageURL { get; set; }
        [DataMember]
        public string ImageURL { get; set; }
        [DataMember]
        public List<string> ActivityImages { get; set; }
        [DataMember]
        public List<string> ActivityImagesURL { get; set; }
        [DataMember]
        public bool PermitRequired { get; set; }
        [DataMember]
        public string ActivityCategory { get; set; }
        [DataMember]
        public string ActivitySubCategory { get; set; }
        [DataMember]
        public decimal UserRating { get; set; }
        [DataMember]
        public decimal DifficultyRating { get; set; }
        [DataMember]
        public string  DifficultyLevel { get; set; }
        [DataMember]
        public string CancellationPolicy { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public decimal CostForChild { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string LocationLatLong { get; set; }

        [DataMember]
        public string LatLong { get; set; }
        [DataMember]
        public string DistanceFromNearestCity { get; set; }
        [DataMember]
        public List<string> AllActivityTimes { get; set; }
        [DataMember]
        public List<int> AllActivityDates { get; set; }
        [DataMember]
        public DateTime NextAvaiableDate { get; set; }
        [DataMember]
        public IDictionary<string, string> Reviews { get; set; }
        [DataMember]
        public List<ActivitySummaryDataContract> SimilarActivities { get; set; }
        [DataMember]
        public string ActivityStartTime { get; set; }
        [DataMember]
        public string ActivityEndTime { get; set; }
    }
}
