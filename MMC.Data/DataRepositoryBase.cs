using Core.Common.Contracts;
using Core.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data
{
    public abstract class DataRepositoryBase<T>:DataRepositoryBase<T,MyMonkeyCapContext>
        where T:class,IIdentifiableEntity, new()
        ///the new() allows us to instantiate the dbcontext class
    {
    }
}
