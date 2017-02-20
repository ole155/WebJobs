﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calculate;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;



namespace WebJob1
{
    public class Functions
    {


        public static void ProcessQueueMessage([ServiceBusTrigger("bus1")] BrokeredMessage message, TextWriter log)
        {
            CalculateJob calculatejob = new CalculateJob();
            calculatejob.DoJob(message);

        }


       

    }
}
