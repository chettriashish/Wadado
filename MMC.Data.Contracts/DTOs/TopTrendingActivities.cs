using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts
{
    public class TopTrendingActivities
    {
        public ActivityImages DefaultActivityImage { get; set; }
        public ActivitiesMaster Activity { get; set; }
    }
}
