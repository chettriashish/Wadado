using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts
{
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        IEnumerable<LocationsMaster> GetAllLocations();

        /// <summary>
        /// All non-fetch operations should be decorated with the transactionflow
        /// so that the db always goes from on consisten state to another consistent state
        /// </summary>
        /// <param name="location"></param>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CreateNewLocation(LocationsMaster location);
    }
}
