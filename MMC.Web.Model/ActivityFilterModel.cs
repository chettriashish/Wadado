using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Model
{
    public class ActivityFilterModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ActivityTypeFilter> ActivityTypes { get; set; }
    }

    public class ActivityTypeFilter
    {
        public string type { get; set; }
        public bool selected { get; set; }
    }
}
