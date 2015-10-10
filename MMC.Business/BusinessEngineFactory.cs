using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace MMC.Business
{
    public class BusinessEngineFactory:IBusinessEngineFactory
    {
        public T GetBusinessEngine<T>() where T : IBusinessEngine
        {
            return ObjectBase.Container.Resolve<T>();
        }
    }
}
