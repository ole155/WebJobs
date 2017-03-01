using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculate;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace WebJob2
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([ServiceBusTrigger("bus2")] BrokeredMessage message, TextWriter log)
        {
            var calculatejob = new CalculateEngine();
            calculatejob.DoJob(message.MessageId);

        }

    }
}
