using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMC.Client.Entities;
using MMC.Client.Contracts.DataContracts;

namespace MMC.Web.Model
{
    public class LocationModel
    {
        public LocationDetailsDataContract LocationDetails { get; set; }
        public IEnumerable<ActivityCategoryModel> AllActivities { get; set; }
    }
}
