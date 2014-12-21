using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(Service1));
            //serviceHost.AddServiceEndpoint(typeof(IService1), new NetTcpBinding(), "net.tcp://localhost:8875/");
            serviceHost.Open();
            Console.WriteLine("Service is running...");
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
            serviceHost.Close();
            Console.WriteLine("Service was stopped");
        }
    }
}
