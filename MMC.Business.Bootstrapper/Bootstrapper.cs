using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using MMC.Data.Contracts.RepositoryInterfaces;
using MMC.Data.DataRepositories;
using Core.Common.Contracts;
using MMC.Data;

namespace MMC.Business.Bootstrapper
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(AllClasses.FromLoadedAssemblies(), WithMappings.FromMatchingInterface, WithName.Default);
            container.RegisterType<IActivitiesMasterRepository, ActivitiesMasterRepository>();
            container.RegisterType<IDataRepositoryFactory, DataRepositoryFactory>();
            container.RegisterType<IBusinessEngineFactory, BusinessEngineFactory>();
            ///Singleton
            //container.RegisterType<IActivitiesMasterRepository, ActivitiesMasterRepository>(new ContainerControlledLifetimeManager());
        }
    }
}