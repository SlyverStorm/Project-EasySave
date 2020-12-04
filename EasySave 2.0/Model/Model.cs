using Newtonsoft.Json;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    /// <summary>
    /// Program data model class
    /// </summary>
    class Model
    {

        /// <summary>
        /// Model Class constructor, initiate Save Work in a list (no parameters)
        /// </summary>
        public Model()
        {
            //If the state file has not been initialized then create 5 SaveWork object from nothing
            if (!File.Exists("stateFile.json"))
            {
                WorkList = new List<ISaveWork>();
                WorkList.Add(new CompleteSaveWork("Default", "", ""));
                //UpdateSaveFile(0);
            }
            //Then if the State file already exist, use the objects in it to create the WorkList
            else
            {
                string stateFile = File.ReadAllText("stateFile.json");
                var tempWorkList = JsonConvert.DeserializeObject<List<ISaveWork>>(stateFile);
                WorkList = tempWorkList;
                //UpdateSaveFile(0);
            }
            
        }

        //Store all 5 (max) save works
        private List<ISaveWork> workList;

        /// <summary>
        /// The work list in model
        /// </summary>
        public List<ISaveWork> WorkList
        {
            get { return workList; }
            set { workList = value; }
        }

        /// <summary>
        /// Can create a save work from simple parameters (private method)
        /// </summary>
        /// <param name="_name">Name of the work (must be different from existing ones)</param>
        /// <param name="_sourcePath">The Source path to save</param>
        /// <param name="_destinationPath">The Target destination to save files in</param>
        /// <param name="_type">Save work type (complete or differential)</param>
        /*private void CreateWork(string _name, string _sourcePath, string _destinationPath, SaveWorkType _type)
        {
            ISaveWork tempSave = new SaveWork(_name, _sourcePath, _destinationPath, _type);
            WorkList.Add(tempSave);
            UpdateSaveFile(WorkList.IndexOf(tempSave));
            CreateLogLine("Creation of a new save work, name : " + tempSave.Name + ", source path : " + tempSave.SourcePath + ", destination path : "+tempSave.DestinationPath+", type : "+tempSave.Type);
        }*/

        /// <summary>
        /// Create a save work (with a complete save algorithm)
        /// </summary>
        /// <param name="_name">Name of the work (must be different from existing ones)</param>
        /// <param name="_source">The Source path to save</param>
        /// <param name="_destination">The Target destination to save files in</param>
        public void CreateCompleteWork(string _name, string _source, string _destination)
        {
            WorkList.Add(new CompleteSaveWork(_name, _source, _destination));
        }

        /// <summary>
        /// Create a save work (with a differential save algorithm)
        /// </summary>
        /// <param name="_name">Name of the work (must be different from existing ones)</param>
        /// <param name="_source">The Source path to save</param>
        /// <param name="_destination">The Target destination to save files in</param>
        public void CreateDifferencialWork(string _name, string _source, string _destination)
        {
            WorkList.Add(new DifferencialSaveWork(_name, _source, _destination));
        }


        /// <summary>
        /// Modify value of save works objects stored in workList, if there is any null parameters the value attached isn't changed
        /// </summary>
        /// <param name="_nb">Index of the work you want to change in the list</param>
        /// <param name="_name">New name to apply to the work</param>
        /// <param name="_sourcePath">New source path to apply to the work</param>
        /// <param name="_destinationPath">New target destination path to apply to the work</param>
        /// <param name="_type">New type of save work to apply to the work</param>
        public void ChangeWork(int _nb, string _name, string _sourcePath, string _destinationPath, SaveWorkType _type)
        {
            if (_type != WorkList[_nb].Type && _type == SaveWorkType.complete)
            {
                WorkList[_nb] = new CompleteSaveWork(_name, _sourcePath, _destinationPath);
            }
            else if (_type != WorkList[_nb].Type && _type == SaveWorkType.differencial)
            {
                WorkList[_nb] = new DifferencialSaveWork(_name, _sourcePath, _destinationPath);
            }
            else
            {
                if (_name != "") { WorkList[_nb].Name = _name; }
                if (_sourcePath != "") { WorkList[_nb].SourcePath = _sourcePath; }
                if (_destinationPath != "") { WorkList[_nb].DestinationPath = _destinationPath; }
            }

            //UpdateSaveFile(_nb);
            //CreateLogLine("Modification of a existing save work in position " + _nb + ", current parameters : name : " + WorkList[_nb - 1].Name + ", source path : " + WorkList[_nb - 1].SourcePath + ", destination path : " + WorkList[_nb - 1].DestinationPath + ", type : " + WorkList[_nb - 1].Type);
        }

        /// <summary>
        /// Can delete a save work (set to null)
        /// </summary>
        /// <param name="_nb">Index of the work in the list to delete</param>
        public void DeleteWork(int _nb)
        {
            WorkList.RemoveAt(_nb);
            //TODO: IMPLEMENTER METHODE DE REASSIGNATION DES SAVEWORK, YA PEUT ËTRE DES BUG SANS
            //UpdateSaveFile(_nb);
            //CreateLogLine("Supression of save work in position"+_nb);
        }

        /// <summary>
        /// This method check if a work already exists in the WorkList, matching with their name
        /// </summary>
        /// <param name="_name">The name of the work you want to check</param>
        /// <returns></returns>
        public bool IfSaveWorkAlreadyExists(string _name)
        {
            foreach(ISaveWork work in WorkList)
            {
                if (work.Name == _name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get the index of a specified work in WorkList, matching with their name
        /// </summary>
        /// <param name="_name">The name of the work you want the index in the list</param>
        /// <returns></returns>
        public int GetWorkIndex(string _name)
        {
            foreach(ISaveWork work in WorkList)
            {
                if(work.Name == _name)
                {
                    return WorkList.IndexOf(work);
                }
            }
            return 0;
        }

        /// <summary>
        /// Can initiate a type of save from the numbers of the save work in workList.
        /// </summary>
        /// <param name="_nb">Index of the work in the list to execute the save process</param>
        /*public void DoSave(int _nb)
        {
            SaveWork work = WorkList[_nb - 1];

            if (Directory.Exists(WorkList[_nb - 1].SourcePath))
            {
                if (work.Type == SaveWorkType.complete)
                {
                    CompleteSave(work);
                }
                else if (work.Type == SaveWorkType.differencial)
                {
                    DifferencialSave(work);
                }
            }
        }*/

        /// <summary>
        /// Create the line to record in the log file
        /// </summary>
        /// <param name="_content">Content to write in the log</param>
        /*public void CreateLogLine(string _content)
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

        }*/

        /// <summary>
        /// Update the state file with the work list value
        /// </summary>
        /// <param name="_nb">Index of the save work process to update</param>
        /*public void UpdateSaveFile(int _nb)
        {
            if (_nb != 0)
            {
                //Check is a save protocol is active or not
                if (WorkList[_nb - 1].IsActive)
                {
                    long sizeDifference = WorkList[_nb - 1].SaveProgress.TotalSize - WorkList[_nb - 1].SaveProgress.SizeRemaining;

                    //Check if the difference in size is equal to 0, to avoid division by 0
                    if (sizeDifference != 0)
                    {
                        WorkList[_nb - 1].SaveProgress.ProgressState = ((WorkList[_nb - 1].SaveProgress.TotalSize - WorkList[_nb - 1].SaveProgress.SizeRemaining) / WorkList[_nb - 1].SaveProgress.TotalSize * 100);
                    }
                }
            }

            //Convert the work list to a json string then write it in a json file
            var convertedJson = JsonConvert.SerializeObject(WorkList, Formatting.Indented);
            File.WriteAllText("stateFile.json", convertedJson);
        }*/


        /// <summary>
        /// Check if the Sofware is launched
        /// </summary>
        /// <param name="_processName">The name of the process you want to check</param>
        /// <returns></returns>
        public bool CheckIfSoftwareIsLaunched(string _processName)
        {
            bool softwareIsLaunched;

            // Check if the Sofware (Calculator for testing purpose) is launched
            if (Process.GetProcessesByName(_processName).Length == 0)
            {
                // The software isn't launched
                softwareIsLaunched = false;
            } else
            {
                // The software is launched
                softwareIsLaunched = true;
            }
            return softwareIsLaunched;
        }

    }
}
