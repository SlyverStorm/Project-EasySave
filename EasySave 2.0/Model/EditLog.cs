using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_2._0
{
    static class EditLog
    {

        /// <summary>
        /// Create the line to record in the log file
        /// </summary>
        /// <param name="_content">Content to write in the log</param>
        private static void CreateLogLine(string _content)
        {
            //Check if file log.json doesn't exists, if so then create it and initialize it
            if (!File.Exists("log.json"))
            {
                File.WriteAllText("log.json", "[]");
            }
            //New LogLine object with Time and content
            LogLine newLogLine = new LogLine(_content);

            //Create a raw string from the json log file
            string JsonLog = File.ReadAllText("log.json");

            //Convert the raw string into a LogLine object list
            var LogList = JsonConvert.DeserializeObject<List<LogLine>>(JsonLog);

            //Add the new object to the list
            LogList.Add(newLogLine);

            //Convert the LogLine object list into a json formated string
            var convertedJson = JsonConvert.SerializeObject(LogList, Formatting.Indented);

            //Write the new string into the json log file
            File.WriteAllText("log.json", convertedJson);

        }

    }
}
