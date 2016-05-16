using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Business.Bootstrapper;
using MMC.Data.Contracts.RepositoryInterfaces;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using MMC.Business.Entities;
using MMC.Data.DataRepositories;
using Core.Common.Core;
using Core.Common.Contracts;

namespace MMC.Data.Tests
{
    [TestClass]
    public class DataLayerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = Bootstrapper.Initialise();
        }
        [TestMethod]
        public void TestRepositoryUsage()
        {
            ///Creates a new instance every time
            RepositoryClassTest repositoryTest = ObjectBase.Container.Resolve<RepositoryClassTest>();
            IEnumerable<ActivitiesMaster> activities = repositoryTest.ActivityMasterRepository.Get();
            //ActivityDetailsDataContract activities = repositoryTest.GetActivities();
            Assert.IsTrue(activities != null);
        }

        [TestMethod]
        public void TestRepositoryFactoryUsage()
        {
            ///Creates a new instance every time
            RepositoryFactoryClassTest repositoryTest = ObjectBase.Container.Resolve<RepositoryFactoryClassTest>();

            //ActivityDetailsDataContract activities = repositoryTest.GetActivities();
            IEnumerable<LocationsMaster> locations = repositoryTest.GetLocations();
            //Assert.IsTrue(activities != null);
            Assert.IsTrue(locations != null);
        }
    }

    public class RepositoryClassTest
    {
        public RepositoryClassTest()
        {
        }
        public RepositoryClassTest(IActivitiesMasterRepository activitiesMasterRepository)
        {
            this.ActivityMasterRepository = activitiesMasterRepository;
        }

        public IActivitiesMasterRepository ActivityMasterRepository { get; set; }

        //public ActivityDetailsDataContract GetActivities()
        //{
        //    ActivityDetailsDataContract activities = ActivityMasterRepository.GetActivityByLocation("GANGTOK","HIGHFLY",null);
        //    return activities;
        //}
    }

    public class RepositoryFactoryClassTest
    {
        public RepositoryFactoryClassTest()
        {
        }
        public RepositoryFactoryClassTest(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        IDataRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<ActivitiesMaster> GetActivities()
        {
            ///Repository factory allows us to fetch a repository type on demand
            ///rather than loading all of the repository dependencies at once
            ///as per above "RepositoryClassTest"
            IActivitiesMasterRepository activityMasterRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
            IEnumerable<ActivitiesMaster> activities = activityMasterRepository.Get();
            return activities;
        }

        public IEnumerable<LocationsMaster> GetLocations()
        {
            ILocationsMasterRepository locationMasterRepository = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
            IEnumerable<LocationsMaster> locations = locationMasterRepository.Get();
            return locations;
        }
    }
}
