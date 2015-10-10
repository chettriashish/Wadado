using WURFL;
using WURFL.Aspnet.Extensions.Config;

namespace MMC.Web
{
    public class WurflConfig
    {
        public static void Initialize()
        {
            WURFLManagerBuilder.Build(new ApplicationConfigurer());
        }
    }
}