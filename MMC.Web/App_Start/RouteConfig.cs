using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MMC.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // /Getting details of selected location
            routes.MapRoute("GetSelectedLocation",
                "location/GetSelectedLocation",
                new { controller = "Location", action = "GetSelectedLocation", selectedLocation = UrlParameter.Optional });

            routes.MapRoute("GetAllOtherLocations",
               "location/GetAllOtherLocations",
               new { controller = "Location", action = "GetAllOtherLocations", selectedLocation = UrlParameter.Optional });

            // /Location/locationkey
            routes.MapRoute("Location",
                "location/{locationName}",
                new { controller = "Location", action = "Index", selectedLocation = UrlParameter.Optional });

            // /Getting details of selected location
            routes.MapRoute("GetLocationsForActivities",
                "activities/GetAllOtherLocations",
                new { controller = "Activities", action = "GetAllOtherLocations", locationName = UrlParameter.Optional, activityType = UrlParameter.Optional });

            // /Getting all locations for activitiesListing
            routes.MapRoute("GetSelectedActivity",
                "activities/GetSelectedActivityType",
                new { controller = "Activities", action = "GetSelectedActivityType", locationName = UrlParameter.Optional, activityType = UrlParameter.Optional });

            // /Location/locationkey/ActivityCategory
            routes.MapRoute("Activity",
              "activities/{locationName}/{activityType}",
              new { controller = "Activities", action = "Index", locationName = UrlParameter.Optional, activityType = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}