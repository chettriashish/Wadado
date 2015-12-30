using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ActivitiesService.ActivitiesServiceClient _client = new ActivitiesService.ActivitiesServiceClient();
            _client.GetAllActivities("GANGTOK", "HIGHFLY", "smartphone");
            Console.ReadLine();
        }
    }
}
