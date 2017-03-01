using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Calculate;
using Microsoft.Azure.WebJobs;

namespace WebJob3
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main(string[] args)
        {
            var config = new JobHostConfiguration();


            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            //var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously


            CalculateJob calculateJob = new CalculateJob();
            calculateJob.DoJob(args[0]);

            //host.RunAndBlock();
        }

        public static void DoJob(String Id)
        {
            CalculateJob calculateJob = new CalculateJob();
            calculateJob.DoJob(Id);
        }
    }
}
