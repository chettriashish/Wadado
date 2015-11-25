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
        public string DefaultLocationImageURL { get; set; }
        public List<ActivityCategoryModel> AllActivities { get; set; }
        public List<string> BestMonthsToVisit { get; set; }
    }
}
