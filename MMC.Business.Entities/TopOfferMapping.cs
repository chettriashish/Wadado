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
    public class TopOfferMapping : EntityBase, IIdentifiableEntity
    {
        #region Properties
        [DataMember]
        public string TopOfferMappingKey { get; set; }
        [DataMember]
        public string TopOfferKey { get; set; }
        [DataMember]
        public string MappingKey { get; set; }
        [DataMember]
        public string MappingType { get; set; }
        [DataMember]
        public decimal Discount { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public string EntityId
        {
            get
            {
                return TopOfferMappingKey;
            }
            set
            {
                TopOfferMappingKey = value;
            }
        } 
        #endregion
    }
}
