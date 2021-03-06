﻿using MMC.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;


namespace MMC.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Bootstrapper.Initialise();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();           
            // Initialize WURFL
            WurflConfig.Initialize();
            // Added to make the site device-aware. (Disable this to get back to default behavior.)
            DisplayConfig.RegisterDisplayModes(DisplayModeProvider.Instance.Modes);
            //Removing the default JSON Factory and adding a custom factory to accomodate large JSON
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<System.Web.Mvc.JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new CustomJsonValueProviderFactory());
        }
    }
}