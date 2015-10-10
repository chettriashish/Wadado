using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts
{
    public class CustomerActivityRentalInfo
    {
        public IEnumerable<ActivitiesMaster> AllCustomerActivities { get; set; }
        ///TBD
        /// ADD CUSTOMER DETAILS
        ///ADD COMPANY DETAILS
        ///ADD ACTIVITY LOCATION DETAILS
        ///ADD HOTEL BOOKING DETAILS
    }
}
