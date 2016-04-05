using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts.RepositoryInterfaces
{
    public interface ITopOfferMappingRepository : IDataRepository<TopOfferMapping>
    {
        IEnumerable<TopOfferMapping> GetAllTopActivitiesOfferForSelectedLocation(string locationKey);
        TopOfferMapping CheckAndFetchSingleOfferExists(string mappingKey, string mappingType);

        bool AddAll(IEnumerable<TopOfferMapping> mappings);
    }
}
