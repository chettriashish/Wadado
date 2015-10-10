using System.Collections.Generic;
using System.Web.WebPages;
using MMC.Common.Extensions;

namespace MMC.Web
{
    public class DisplayConfig
    {
        public static void RegisterDisplayModes(IList<IDisplayMode> displayModes)
        {
            var modeDesktop = new DefaultDisplayMode("")
            {
                ContextCondition = (c => c.Request.IsDesktop())
            };
            var modeSmartphone = new DefaultDisplayMode("smartphone")
            {
                ContextCondition = (c => c.Request.IsSmartphone())
            };
            var modeTablet = new DefaultDisplayMode("tablet")
            {
                ContextCondition = (c => c.Request.IsTablet())
            };
            var modeLegacy = new DefaultDisplayMode("legacy")
            {
                ContextCondition = (c => c.Request.IsLegacy())
            };

            displayModes.Clear();
            displayModes.Add(modeSmartphone);
            displayModes.Add(modeTablet);
            displayModes.Add(modeLegacy);
            displayModes.Add(modeDesktop);
        }
    }
}