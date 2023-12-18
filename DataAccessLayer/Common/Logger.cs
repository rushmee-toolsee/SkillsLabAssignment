using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DataAccessLayer.Common
{
    public class Logger : ILogger
    {
        private readonly string logFilename = "LogFile.txt";
        private readonly string logFilePath;

        public Logger()
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFilename);
        }

        public void Log(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--------------------------------------------------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}
