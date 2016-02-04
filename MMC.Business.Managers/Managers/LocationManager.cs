using Core.Common.Contracts;
using MMC.Business.Contracts;
using MMC.Business.Entities;
using MMC.Common;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Managers
{
    /// <summary>
    /// Setting InstanceMode to per call so that the service proxy does not remain
    /// open for the application lifecycle. 
    /// Setting the Concurrency mode to Multiple so that multiple service calls are 
    /// handled concurrently and not one at a time.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class LocationManager : ManagerBase, ILocationService
    {
        public LocationManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }       
        public IEnumerable<LocationsMaster> GetAllLocations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ILocationsMasterRepository locationsMasterRepository
                        = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();

                IEnumerable<LocationsMaster> allLocations = locationsMasterRepository.Get();

                return allLocations;
            });
        }
        [OperationBehavior(TransactionScopeRequired = true)]        
        public void CreateNewLocation(LocationsMaster location)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ILocationsMasterRepository locationsMasterRepository
                                = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();

                locationsMasterRepository.Add(location);
            });
        }
    }
}
