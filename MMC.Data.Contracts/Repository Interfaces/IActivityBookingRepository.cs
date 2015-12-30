using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts.RepositoryInterfaces
{
    public interface IActivityBookingRepository : IDataRepository<ActivityBooking>
    {
        IEnumerable<ActivityBooking> GetBookedActivitiesBySession(string sessionKey);
        IEnumerable<ActivityBooking> GetBookedActivitiesByUserKey(string userKey);
    }
}
