using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Model
{
    public class TopOffersModel
    {
        public TopOffers Offer { get; set; }        
        public ActivitiesMaster Activity { get; set; }
        public string ImageURL { get; set; }

        public decimal DiscountedPrice { get; set; }
    }
}
