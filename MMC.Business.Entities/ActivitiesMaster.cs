﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Business.Entities
{    
    public partial class ActivitiesMaster : EntityBase, IIdentifiableEntity
    {   

        #region Properties
        [DataMember]
        public string ActivitesKey { get; set; }

        [DataMember]        
        public string ActivityTypeKey { get; set; }

        [DataMember]
        public string LocationKey { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public decimal CostForChild { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string AdditionalFeatures { get; set; }
        [DataMember]
        public string Pickup { get; set; }
        [DataMember]        
        public int MaxAdults { get; set; }
        [DataMember]
        public int MinAdults { get; set; }
        public int NumAdults { get; set; }
        public int NumChildren { get; set; }
        [DataMember]
        public int MaxChildren { get; set; }
        [DataMember]
        public int MinChildren { get; set; }
        [DataMember]
        public string ThingsToCarry { get; set; }
        [DataMember]
        public string Included { get; set; }
        [DataMember]
        public bool IsPermitRequired { get; set; }
        [DataMember]
        public decimal DifficultyRating { get; set; }
        [DataMember]
        public string Advice { get; set; }
        [DataMember]
        public string CancellationPolicy { get; set; }
        [DataMember]
        public string OurReview { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public bool IsValidated { get; set; }
        [DataMember]
        public string ActivityLocation { get; set; }
        [DataMember]
        public string DistanceFromNearestCity { get; set; }
        [DataMember]
        public decimal AverageUserRating { get; set; }
        [DataMember]
        public string ActivityStartTime { get; set; }
        [DataMember]
        public string ActivityEndTime { get; set; }
        [DataMember]
        public decimal? Comission { get; set; }
        [DataMember]
        public int? MaxUnits { get; set; }
        [DataMember]
        public bool? AllowInstantBooking { get; set; }
        #endregion
        [DataMember]
        public string EntityId
        {
            get
            {
                return ActivitesKey;
            }
            set
            {
                ActivitesKey = value;
            }
        }
    }
}
