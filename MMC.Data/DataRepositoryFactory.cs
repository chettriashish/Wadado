using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
namespace MMC.Data
{
    public class DataRepositoryFactory:IDataRepositoryFactory
    {
        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return ObjectBase.Container.Resolve<T>();
        }
    }
}
