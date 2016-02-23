using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMC.Business.Bootstrapper;
using MMC.Business.Entities;
using MMC.Business.Managers;
using Core.Common.Core;
using SM = System.ServiceModel;
namespace MMC.ServiceHost
{
    class Program
    {       
        static void Main(string[] args)
        {
            WcfServiceFactory hostFactory = new WcfServiceFactory();
            System.Console.WriteLine("Starting up services...");
            System.Console.WriteLine("");
            ///Using WCF unity service factory to create service hosts and configure services
            SM.ServiceHost host = hostFactory.CreateServiceHostWithType(typeof(ActivitiesManager));
            StartService(host, "Activities Manager");

            SM.ServiceHost host1 = hostFactory.CreateServiceHostWithType(typeof(UsersManager));

            StartService(host1, "User Manager");

            SM.ServiceHost host2 = hostFactory.CreateServiceHostWithType(typeof(LocationManager));

            StartService(host2, "Location Manager");

            System.Console.WriteLine("");
            System.Console.WriteLine("Press [Enter] to exit.");
            StopService(host,"Activities Manager");
            System.Console.ReadLine();
        }

        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            System.Console.WriteLine("Service {0} started.", serviceDescription);
            foreach(var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Listening on endpoint."));
                Console.WriteLine(string.Format("Address: {0}", endpoint.Address.Uri));
                Console.WriteLine(string.Format("Binding: {0}", endpoint.Binding.Namespace));
                Console.WriteLine(string.Format("Contract: {0}", endpoint.Contract.ConfigurationName));
            }
            Console.ReadLine();
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();
            Console.WriteLine("Service {0} stopped", serviceDescription);
        }
    }
}
