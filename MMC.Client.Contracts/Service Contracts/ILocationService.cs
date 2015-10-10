using Core.Common.Contracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Contracts
{
    [ServiceContract]
    public interface ILocationService:IServiceContract
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
