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

            routes.MapMvcAttributeRoutes();

            // /Getting details of selected location
            routes.MapRoute("GetSelectedLocation",
                "location/GetSelectedLocation",
                new { controller = "Location", action = "GetSelectedLocation", selectedLocation = UrlParameter.Optional });

            // /Location/locationkey
            routes.MapRoute("Location",
                "location/{locationName}",
                new { controller = "Location", action = "Index", selectedLocation = UrlParameter.Optional });            

            // /Getting all locations for activitiesListing
            routes.MapRoute("GetSelectedActivity",
                "activities/GetSelectedActivityType",
                new { controller = "Activities", action = "GetSelectedActivityType", locationName = UrlParameter.Optional, activityType = UrlParameter.Optional });

            // /Activity/locationName/ActivityCategory
            routes.MapRoute("Activity",
              "activities/{locationName}/{activityType}",
              new { controller = "Activities", action = "Index", locationName = UrlParameter.Optional, activityType = UrlParameter.Optional });

            // /Getting all details for selected activity
            routes.MapRoute("GetSelectedActivityDetails",
                "ActivityDetails/GetSelectedActivityDetails",
                new { controller = "ActivityDetails", action = "GetSelectedActivityDetails", locationName = UrlParameter.Optional, activityType = UrlParameter.Optional });

            // /ActivityDetails/LocationName/ActivityCode
            routes.MapRoute("ActivityDetails",
              "ActivityDetails/{selectedLocation}/{activityKey}",
              new { controller = "ActivityDetails", action = "Index", locationName = UrlParameter.Optional, activityCode = UrlParameter.Optional });           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}