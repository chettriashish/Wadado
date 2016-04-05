using Core.Common.Contracts;
using MMC.Client.Contracts.DataContracts;
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
        /// so that the db always goes from on consistent state to another consistent state
        /// </summary>
        /// <param name="location"></param>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CreateNewLocation(LocationDetailsDataContract locationDetails);

        [OperationContract]
        IEnumerable<LocationDetailsDataContract> GetSelectedLocationDetails(string locationKey);
        [OperationContract]
        IEnumerable<LocationDetailsDataContract> GetSelectedLocationDetailsForClientApplication(string locationKey, string userAgent);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateLocationDetails(LocationDetailsDataContract locationDetails);        
    }
}
