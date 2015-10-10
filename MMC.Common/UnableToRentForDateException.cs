using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Common
{
    public class UnableToRentForDateException:ApplicationException
    {
        public UnableToRentForDateException(string message)
            : base(message)
        {
        }

        public UnableToRentForDateException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
