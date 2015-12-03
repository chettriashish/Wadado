using Core.Common.Contracts;
using Microsoft.Practices.Unity;
using MMC.Business.BusinessEngines;
using MMC.Business.Common;
using MMC.Business.Contracts;
using MMC.Data;
using MMC.Data.Contracts.RepositoryInterfaces;
using MMC.Data.DataRepositories;
using Unity.Wcf;

namespace MMC.Business.Managers
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IActivitiesService, ActivitiesManager>();
            container.RegisterType<IActivitiesMasterRepository, ActivitiesMasterRepository>();
            container.RegisterType<IDataRepositoryFactory, DataRepositoryFactory>();
            container.RegisterType<IBusinessEngineFactory, BusinessEngineFactory>();
            container.RegisterType<IActivitiesBookingEngine, ActivitiesBookingEngine>();         
        }
    }    
}