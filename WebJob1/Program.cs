using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace WebJob1
{
    class Program
    {
  
        static void Main()
        {
            var config = new JobHostConfiguration();
            ServiceBusConfiguration serviceBusConfig = new ServiceBusConfiguration();
            serviceBusConfig.MessageOptions.MaxConcurrentCalls = 64;
            serviceBusConfig.MessageOptions.AutoRenewTimeout = new TimeSpan(14,0,0,0); 
            config.UseServiceBus(serviceBusConfig);

            JobHost host = new JobHost(config);
            host.RunAndBlock();
         
        }
    }
}
