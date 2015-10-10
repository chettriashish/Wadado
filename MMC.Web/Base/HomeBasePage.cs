using Microsoft.Practices.Unity;
using MMC.Common.Contracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Base
{
    public class HomeBasePage:WebViewPage
    {
        [Dependency]
        public IHomeService HomeService { get; set; }
        public override void Execute()
        {
            
        }
    }
}