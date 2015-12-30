using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Business.Entities
{
    public class GuestFavorites : EntityBase, IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string GuestFavouritesKey { get; set; }
        [DataMember]
        public string ActivityKey { get; set; }
        [DataMember]
        public string GuestKey { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; } 
        #endregion
        public string EntityId
        {
            get
            {
                return GuestFavouritesKey;
            }
            set
            {
                GuestFavouritesKey = value;
            }
        }
    }
}
