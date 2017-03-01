using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebJob1;
using WebJob2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;



namespace WebJob1.Tests
{
    [TestClass()]
    public class FunctionsTests
    {
        [TestMethod()]
        public void ProcessQueueMessageTest1()
        {
            BrokeredMessage b = new BrokeredMessage();
            b.MessageId = "9661e6a0-5b89-48b8-bbee-b21762fcf839";
            WebJob1.Functions.ProcessQueueMessage(b, new StringWriter() );
        }

        [TestMethod()]
        public void ProcessQueueMessageTest2()
        {
            BrokeredMessage b = new BrokeredMessage();
            b.MessageId = "4733f3bf-a452-4364-92eb-dca1b30817f1";
            WebJob2.Functions.ProcessQueueMessage(b, new StringWriter());
        }


      
    }
}