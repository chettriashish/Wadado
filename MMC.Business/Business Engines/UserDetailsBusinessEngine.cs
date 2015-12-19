using Core.Common.Contracts;
using MMC.Business.Common;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.BusinessEngines
{
    public class UserDetailsBusinessEngine:IUserDetailsBusinessEngine
    {
        IDataRepositoryFactory _DataRepositoryFactory;
        public UserDetailsBusinessEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        public GuestInformationMaster AddGuestInformation(string userName, string userKey, string email)
        {
            IGuestInformationMasterRepository guestInformationRepository = _DataRepositoryFactory.GetDataRepository<IGuestInformationMasterRepository>();
            GuestInformationMaster result = guestInformationRepository.Get(userKey);
            if (result == null )
            {
                result = new GuestInformationMaster() { GuestKey = userKey, Name = userName, DOB = DateTime.Now };
                guestInformationRepository.Add(result);
            }
            return result;            
        }


        public bool UpdateGuestInformation(GuestInformationMaster guestInformation, string sessionKey)
        {
            IGuestInformationMasterRepository guestInformationRepository = _DataRepositoryFactory.GetDataRepository<IGuestInformationMasterRepository>();                       
            guestInformationRepository.Update(guestInformation);
            return true;
        }

        public GuestInformationMaster GetUserInformation(string guestKey)
        {
            IGuestInformationMasterRepository guestInformationRepository = _DataRepositoryFactory.GetDataRepository<IGuestInformationMasterRepository>();
            return guestInformationRepository.Get(guestKey);
        }
    }
}
