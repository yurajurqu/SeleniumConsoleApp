using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SeleniumCM.Helpers
{
    public class LoggerHelper
    {

        public static void LogWrite(string logText)
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            currentDirectory += Path.DirectorySeparatorChar + "TimeTasks.csv";

            if (File.Exists(currentDirectory))
            {
                File.AppendAllText(currentDirectory, logText);
            }
            else
            {
                File.WriteAllText(currentDirectory, logText);
            }
            
        }
      
    }
}
