using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM_Student_Record
{
    internal class dbconnector
    {
        public static string GetConnectionString()
        {
            // Accessing the connection string
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Extract the scrambled connection string from the configuration
            string scrambledConn = config.GetConnectionString("RemoteDB");
                
            // Decrypting the connection string
            if (!string.IsNullOrEmpty(scrambledConn))
            {
                byte[] data = Convert.FromBase64String(scrambledConn);
                return Encoding.UTF8.GetString(data);
            }
            return null; // Return null if the connection string is not found or empty
        }
    }
}
