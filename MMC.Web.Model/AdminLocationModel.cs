using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Model
{
    public class AdminLocationModel
    {
        public string LocationName { get; set; }
        public string LocationKey { get; set; }
        public bool IsDeleted { get; set; }
    }
}
