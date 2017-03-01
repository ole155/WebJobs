using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Web.Http;
using Calculate;

namespace WebApplication2.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)};
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
            Work w = new Work();
            w.id = value;
            var threadDelegate = new ThreadStart(w.DoWork);
            var newThread = new Thread(threadDelegate);
            newThread.Start();
        }



        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }


        class Work
        {
         public string id;
            
            public void DoWork()
            {
                var calculator = new CalculateEngine();
                calculator.DoJob(id);

            }
        }
    }
}
