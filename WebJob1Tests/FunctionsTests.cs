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
            b.MessageId = "03C4A41E-985D-43C3-A5AA-FE8E32B9130B";
            WebJob1.Functions.ProcessQueueMessage(b, new StringWriter() );
        }

        [TestMethod()]
        public void ProcessQueueMessageTest2()
        {
            BrokeredMessage b = new BrokeredMessage();
            b.MessageId = "7A0BE2E4-10BB-4989-A74E-DC2AB996CD1C";
            WebJob2.Functions.ProcessQueueMessage(b, new StringWriter());
        }
    }
}