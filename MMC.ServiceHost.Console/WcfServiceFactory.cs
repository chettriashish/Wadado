using Core.Common.Contracts;
using Microsoft.Practices.Unity;
using MMC.Business;
using MMC.Business.BusinessEngines;
using MMC.Business.Common;
using MMC.Business.Contracts;
using MMC.Business.Managers;
using MMC.Data;
using MMC.Data.Contracts.RepositoryInterfaces;
using MMC.Data.DataRepositories;
using System;
using Unity.Wcf;

namespace MMC.ServiceHost
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
        }

        protected override System.ServiceModel.ServiceHost CreateServiceHost(System.Type serviceType, System.Uri[] baseAddresses)
        {
            return base.CreateServiceHost(serviceType, baseAddresses);
        }

        public System.ServiceModel.ServiceHost CreateServiceHostWithType(Type serviceType)
        {
            ConfigureContainer(mContainter);
            return new UnityServiceHost(mContainter, serviceType);
        }
    }
}