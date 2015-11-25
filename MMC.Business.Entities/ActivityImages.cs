﻿using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Entities
{
    [DataContract]
    public class ActivityImages : EntityBase, IIdentifiableEntity
    {

        #region Properties
        [DataMember]
        public string ActivityImageKey { get; set; }
        [DataMember]
        [ForeignKey("ActivitiesMaster")]
        public string ActivityKey { get; set; }
        [DataMember]
        public string ImageURL { get; set; }

        [DataMember]
        public bool IsDefault { get; set; } 
        #endregion
        public string EntityId
        {
            get
            {
                return ActivityImageKey;
            }
            set
            {
                ActivityImageKey = value;
            }
        }
    }
}