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


        /// <summary>
        /// Create a Log Line about: the initialisation of the Software with the date.
        /// </summary>
        public static void InitSoftwareLogLine()
        {
            CreateLogLine("Initialisation of the Sofware at: " + DateTime.Now); 
        }

        /// <summary>
        /// Create a Log Line about: the creation of a new save.
        /// </summary>
        /// <param name="_nb"></param>
        /// <param name="WorkList"></param>
        public static void CreateWorkLogLine(int _nb, List<ISaveWork> WorkList)
        {
            CreateLogLine("Creation of a new save work, name : " + WorkList[_nb - 1].Name + ", source path : " + WorkList[_nb - 1].SourcePath + ", destination path : " + WorkList[_nb - 1].DestinationPath + ", type : " + WorkList[_nb - 1].Type);
        }

        /// <summary>
        /// Create a Log Line about: the modification of a save.
        /// </summary>
        /// <param name="_nb"></param>
        /// <param name="WorkList"></param>
        public static void ChangeWorkLogLine(int _nb, List<ISaveWork> WorkList)
        {
            CreateLogLine("Modification of a existing save work in position " + _nb + ", current parameters : name : " + WorkList[_nb - 1].Name + ", source path : " + WorkList[_nb - 1].SourcePath + ", destination path : " + WorkList[_nb - 1].DestinationPath + ", type : " + WorkList[_nb - 1].Type);
        }

        /// <summary>
        /// Create a Log Line about: The supression of a save.
        /// </summary>
        /// <param name="_nb"></param>
        public static void DeleteWorkLogLine(int _nb)
        {
            CreateLogLine("Supression of save work in position" + _nb);
        }

        /// <summary>
        /// Create a Log Line about: The research of an existing savework
        /// </summary>
        /// <param name="saveWorkName"></param>
        public static void SaveWorkAlreadyExistsLogLine(string saveWorkName)
        {
            CreateLogLine("The search savework named : " + saveWorkName + " already exist");
        }

        /// <summary>
        /// Create a Log Line about: The launch of a savework 
        /// </summary>
        /// <param name="_nb"></param>
        public static void LaunchingSaveLogLine(int _nb)
        {
            CreateLogLine("Launching of the savework " + _nb);
        }

    }
}
