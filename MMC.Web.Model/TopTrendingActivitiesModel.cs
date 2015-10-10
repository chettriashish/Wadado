using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMC.Web.Model
{
    public class TopTrendingActivitiesModel
    {
        public ActivityImages DefaultActivityImage { get; set; }
        public ActivitiesMaster Activity { get; set; }
        public string ImageURL { get; set; }
    }
}