using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace Calculate
{
    public class CalculateJob
    {
        public CalculateJob()
        {


        }

        public void DoJob(BrokeredMessage message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Detected new message: " + message.MessageId);

            var connectionString = ConfigurationManager.ConnectionStrings["App1DBConnectionString"].ToString();
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.CommandText = "UPDATE [dbo].[TEST] SET [DTProcessStarted] = @dtstart Where Id = @id";
            cmd.Parameters.AddWithValue("Id", message.MessageId);
            cmd.Parameters.AddWithValue("dtstart", DateTime.Now.ToUniversalTime().ToLocalTime());
            cmd.ExecuteNonQuery();


            bool result = Execute(message.MessageId);

            if (result)
            {
                cmd.CommandText = "UPDATE [dbo].[TEST] SET [DTProcessEnded] = @dtend Where Id = @id";
                cmd.Parameters.AddWithValue("dtend", DateTime.Now.ToLocalTime());
                cmd.ExecuteNonQuery();
            }

            sqlConnection1.Close();
        }

        private static bool Execute(string messageMessageId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["App1DBConnectionString"].ToString();
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.CommandText = "Select [Jobsize] From [dbo].[TEST] Where Id = @id";
            cmd.Parameters.AddWithValue("Id", messageMessageId);

            try
            {
                var jobsize = (Int32)cmd.ExecuteScalar();
                Thread.Sleep(Convert.ToInt16(jobsize) * 1000);
                return true;
            }
            catch (Exception)
            {
                // Log: Job not found
                return false;
            }



        }

    }
}
