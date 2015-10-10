using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace MMC.Client.Proxies
{
    public class ServiceFactory:IServiceFactory
    {
        public T CreateClient<T>() where T : IServiceContract
        {
            return ObjectBase.Container.Resolve<T>();
        }
    }
}
