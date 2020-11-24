using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class Model
    {
        private string jsonLogLine;

        public Model()
        {
            
        }

        private SaveWork[] workList;

        public SaveWork[] WorkList
        {
            get { return workList; }
            set { workList = value; }
        }


        public void CreateWork(int nb, string name, string sourcePath, string destinationPath, SaveWorkType type)
        {
            workList[nb] = new SaveWork(name, sourcePath, destinationPath, type);
        }

        public void ChangeWork(int nb, string name, string sourcePath, string destinationPath, SaveWorkType type)
        {
            workList[nb].Name = name;
            workList[nb].SourcePath = sourcePath;
            workList[nb].DestinationPath = destinationPath;
            workList[nb].Type = type;
        }

        public void DeleteWork(int nb)
        {
            workList[nb] = null;
        }

        public void DoSave(int nb)
        {

        }



        public void createLogLine()
        {

            SaveWork save = new SaveWork("Test1", "file1", "File2", SaveWorkType.complete);

            // Translate the log line in a Json format
            jsonLogLine = JsonConvert.SerializeObject(save);
            Console.WriteLine(jsonLogLine);

            // call a fonction to write the line in a file
            addLogLine(); 
        }


        public void addLogLine()
        {
           JsonSerializer serializer = new JsonSerializer();


            // the log file is "data.json"       
            using (var logFile = new StreamWriter(("log.json")))
            {
                using (var jsonWriter = new JsonTextWriter(logFile))
                {
                    // just a single line
                    jsonWriter.Formatting = Formatting.None;
                    // Write the log line
                    serializer.Serialize(jsonWriter, JsonConvert.DeserializeObject(jsonLogLine));
                    Console.WriteLine("Record log OK");
                }
            }
        }

    }
}
