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
            // Translate the log line in a Json format
            jsonLogLine = JsonConvert.SerializeObject("test");
            Console.WriteLine(jsonLogLine);

            // call a fonction to write the line in a file
            addLogLine(); 
        }


        public void addLogLine()
        {
           JsonSerializer serializer = new JsonSerializer();


            // Write the log line in the log file
            using (var streamWriter = new StreamWriter("data.json"))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    jsonWriter.Formatting = Formatting.None;
                    serializer.Serialize(jsonWriter, JsonConvert.DeserializeObject(jsonLogLine));
                    Console.WriteLine("Record log OK");
                }
            }
        }

    }
}
