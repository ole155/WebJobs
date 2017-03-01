using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace Calculate
{

    public class CalculateEngine
    {

        public void DoJob(string messageId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["App1DBConnectionString"].ToString();
            using (var connection = new SqlConnection(connectionString))
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);

                sqlConnection1.Open();

                using (var cmd = new SqlCommand("",connection))
                {
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("Id", messageId);
                    cmd.CommandText = "UPDATE [dbo].[TEST] SET [DTProcessStarted] = @dtstart, [DTProcessEnded] = Null, [TotalProcessTime] = Null  Where Id = @id";
                    cmd.Parameters.AddWithValue("dtstart", DateTime.UtcNow);
                    cmd.ExecuteNonQuery();

                    // Get jobsize
                    cmd.CommandText = "Select [Jobsize] From [dbo].[TEST] Where Id = @id";
                    var jobsize = (int)cmd.ExecuteScalar();



                    var tt = DateTime.Now;
                    Console.WriteLine("Starting Job: " + messageId + " Size: " + jobsize );

                    // Simulate job execution
                    bool result = Execute(jobsize);

                    Console.WriteLine("Finished Job: " + messageId + " in " + (int)(DateTime.Now - tt).TotalMilliseconds+ " miliseconds ");


                    if (!result)
                    {
                        throw new Exception("Simuleret fejl");
                    }

                    // Update TotalProcesstime
                    // Update DTProcessEnded
                    if (result)
                    {
                        cmd.CommandText = "SELECT DATEDIFF(MILLISECOND, 0,@dtend - [DTReceived]) as TotalProcesstime FROM [dbo].[TEST] Where Id = @id";
                        cmd.Parameters.AddWithValue("dtend", DateTime.UtcNow);
                        var totalprocesstime = (int)cmd.ExecuteScalar()-jobsize*1000;

                        cmd.CommandText = "UPDATE [dbo].[TEST] SET [DTProcessEnded] = @dtend, [TotalProcessTime] = @total Where Id = @id";
                        cmd.Parameters.AddWithValue("total", totalprocesstime);
                        cmd.ExecuteNonQuery();
                    }




                }
            }
        }
        

        private bool Execute(int jobsize)
        {
            try
            {
                Thread.Sleep(Convert.ToInt16(jobsize) * 1000);

                // Simulate ERROR !!!!
                if (DateTime.Now.Second == 59)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                // Log: Job not found
                return false;
            }

            return true;
        }

    }
}
