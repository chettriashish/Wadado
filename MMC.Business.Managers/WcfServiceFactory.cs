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
        private static IUnityContainer mContainter;
        public WcfServiceFactory()
        {
            mContainter = new UnityContainer();
        }

        protected override void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IActivitiesService, ActivitiesManager>();
            container.RegisterType<IUsersService, UsersManager>();
            container.RegisterType<IActivitiesMasterRepository, ActivitiesMasterRepository>();
            container.RegisterType<IDataRepositoryFactory, DataRepositoryFactory>();
            container.RegisterType<IBusinessEngineFactory, BusinessEngineFactory>();
            container.RegisterType<IActivitiesBookingEngine, ActivitiesBookingEngine>();
            container.RegisterType<IUserDetailsBusinessEngine, UserDetailsBusinessEngine>(); 
        }
        protected override System.ServiceModel.ServiceHost CreateServiceHost(System.Type serviceType, System.Uri[] baseAddresses)
        {
            ConfigureContainer(mContainter);
            return new UnityServiceHost(mContainter, serviceType);
        }
    }    
}