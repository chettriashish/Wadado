using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Model
{
    public class ActionModel
    {
        public string Action { get; set; }
        public string ReturnURL { get; set; }
        public string ActivityKey { get; set; }
        public string ActivityDate { get; set; }
        public string Time { get; set; }
        public string NumberOfAdults { get; set; }
        public string NumberOfChildren { get; set; }
    }
}
