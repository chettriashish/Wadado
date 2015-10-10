using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WURFL;

namespace MMC.Common.Extensions
{
    public static class DeviceExtensions
    {
        /// <summary>
        /// Indicates whether the browser behind the current request is wireless
        /// </summary>
        /// <param name="device">WURFL device</param>
        /// <returns>True if it's a mobile device, false otherwise</returns>
        public static Boolean IsWireless(this IDevice device)
        {
            return device.GetCapability("is_wireless_device").ToBool();
        }

        /// <summary>
        /// Indicates whether the requesting device is a tablet
        /// </summary>
        /// <param name="device">WURFL device</param>
        /// <returns>True if it's a tablet device, false otherwise</returns>
        public static Boolean IsTablet(this IDevice device)
        {
            return device.GetCapability("is_tablet").ToBool();
        }

        /// <summary>
        /// Indicates whether the browser behind the current request has touch capabilities
        /// </summary>
        /// <param name="device">WURFL device</param>
        /// <returns>True if it's a mobile device, false otherwise</returns>
        public static Boolean IsTouch(this IDevice device)
        {
            return device.GetCapability("pointing_method").Equals("touchscreen");
        }

        /// <summary>
        /// Gets width of the device in pixels
        /// </summary>
        /// <param name="device">WURFL device</param>
        /// <returns>True if it's a mobile device, false otherwise</returns>
        public static Int32 Width(this IDevice device)
        {
            return device.GetCapability("resolution_width").ToInt();
        }

        /// <summary>
        /// Gets the OS of the device (if any)
        /// </summary>
        /// <param name="device">WURFL device</param>
        /// <returns>Name of the OS, if any</returns>
        public static String Os(this IDevice device)
        {
            var deviceOs = device.GetCapability("device_os");
            var deviceOsVersion = device.GetCapability("device_os_version");
            return String.Format("{0} {1}", deviceOs, deviceOsVersion).Trim();
        }

        /// <summary>
        /// Indicates whether the device runs the specified OS (version)   
        /// </summary>
        /// <param name="device">WURFL device</param>
        /// <param name="os">Name of the OS</param>
        /// <returns>True if it's a mobile device, false otherwise</returns>
        public static Boolean HasOs(this IDevice device, String os)
        {
            return HasOs(device, os, new Version(0, 0));
        }
        public static Boolean HasOs(this IDevice device, String os, Version version)
        {
            // Check OS
            var deviceOs = device.GetCapability("device_os");
            if (!deviceOs.Equals(os, StringComparison.InvariantCultureIgnoreCase))
                return false;

            // Check OS version
            var deviceOsVersion = device.GetCapability("device_os_version");
            if (!deviceOsVersion.Contains("."))
                deviceOsVersion = String.Format("{0}.0", deviceOsVersion);

            Version detectedVersion;
            var success = Version.TryParse(deviceOsVersion, out detectedVersion);
            if (!success)
                return false;
            return detectedVersion.CompareTo(version) >= 0;
        }
        public static Boolean HasOs(this IDevice device, String os, String version)
        {
            // Check OS
            var deviceOs = device.GetCapability("device_os");
            if (!deviceOs.Equals(os, StringComparison.InvariantCultureIgnoreCase))
                return false;

            // Check OS version
            var deviceOsVersion = device.GetCapability("device_os_version");
            return deviceOsVersion.Equals(version, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
