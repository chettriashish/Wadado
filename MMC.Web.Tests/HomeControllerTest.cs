using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MMC.Client.Entities;
using Core.Common.Core;
using MMC.Web.Contracts;
using MMC.Client.Bootstrapper;
using MMC.Client.Contracts;
using Microsoft.Practices.Unity;

namespace MMC.Web.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = Bootstrapper.Initialise();
        }

        [TestMethod]
        public void LocationTest()
        {            
            IHomeDataService _homeDataService = ObjectBase.Container.Resolve<IHomeDataService>();
            IEnumerable<LocationsMaster> results = _homeDataService.GetAllLocations();             
        }
    }
}
