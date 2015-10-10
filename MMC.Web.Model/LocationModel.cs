using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMC.Client.Entities;
namespace MMC.Web.Model
{
    public class LocationModel
    {
        public LocationsMaster SelectedLocation { get; set; }
        public IEnumerable<ActivityCategoryModel> AllActivities { get; set; }
    }
}
