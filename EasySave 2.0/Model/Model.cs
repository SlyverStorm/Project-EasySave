using Newtonsoft.Json;
using System;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{

    public delegate void SaveWorkUpdateDelegate();

    /// <summary>
    /// Program data model class
    /// </summary>
    class Model : INotifyPropertyChanged
    {

        /// <summary>
        /// Model Class constructor, initiate Save Work in a list (no parameters)
        /// </summary>
        public Model()
        {
            OnSaveWorkUpdate = UpdateSaveFile;
            WorkList = new List<ISaveWork>();
            //If the state file has not been initialized then create 5 SaveWork object from nothing
            if (!File.Exists("stateFile.json"))
            {
                WorkList.Add(new CompleteSaveWork("Default", "", "", null));
                UpdateSaveFile();
            }
            //Then if the State file already exist, use the objects in it to create the WorkList
            else
            {
                string stateFile = File.ReadAllText("stateFile.json");
                var tempWorkList = JsonConvert.DeserializeObject<List<CompleteSaveWork>>(stateFile);

                foreach (CompleteSaveWork work in tempWorkList)
                {
                    if (work.Type == SaveWorkType.complete)
                    {
                        CreateCompleteWork(work.Name, work.SourcePath, work.DestinationPath, work.ExtentionToEncryptList);
                    }
                    else if (work.Type == SaveWorkType.differencial)
                    {
                        CreateDifferencialWork(work.Name, work.SourcePath, work.DestinationPath, work.ExtentionToEncryptList);
                    }
                }
                UpdateSaveFile();
            }

            EditLog.InitSoftwareLogLine();
            
        }

        public static SaveWorkUpdateDelegate OnSaveWorkUpdate;

        //Store all 5 (max) save works
        private List<ISaveWork> workList;

        /// <summary>
        /// The work list in model
        /// </summary>
        public List<ISaveWork> WorkList
        {
            get { return workList; }
            set 
            { 
                workList = value;
                OnPropertyChanged("WorkList");
            }
        }

        /// <summary>
        /// Create a save work (with a complete save algorithm)
        /// </summary>
        /// <param name="_name">Name of the work (must be different from existing ones)</param>
        /// <param name="_source">The Source path to save</param>
        /// <param name="_destination">The Target destination to save files in</param>
        public void CreateCompleteWork(string _name, string _source, string _destination, List<Extension> _extension)
        {
            if (!IfSaveWorkAlreadyExists(_name))
            {
                WorkList.Add(new CompleteSaveWork(_name, _source, _destination, _extension));
                SetWorkIndex();
                UpdateSaveFile();
                EditLog.CreateWorkLogLine(GetWorkIndex(_name), WorkList);
            }
            //TODO: Event pour préveneir la vue MDRRRRRRR !
        }

        /// <summary>
        /// Create a save work (with a differential save algorithm)
        /// </summary>
        /// <param name="_name">Name of the work (must be different from existing ones)</param>
        /// <param name="_source">The Source path to save</param>
        /// <param name="_destination">The Target destination to save files in</param>
        public void CreateDifferencialWork(string _name, string _source, string _destination, List<Extension> _extension)
        {
            if (!IfSaveWorkAlreadyExists(_name))
            {
                WorkList.Add(new DifferencialSaveWork(_name, _source, _destination, _extension));
                SetWorkIndex();
                UpdateSaveFile();
                EditLog.CreateWorkLogLine(GetWorkIndex(_name), WorkList);
            }
            //TODO: Event pour préveneir la vue MDRRRRRRR !
        }


        /// <summary>
        /// Modify value of save works objects stored in workList, if there is any null parameters the value attached isn't changed
        /// </summary>
        /// <param name="_nb">Index of the work you want to change in the list</param>
        /// <param name="_name">New name to apply to the work</param>
        /// <param name="_sourcePath">New source path to apply to the work</param>
        /// <param name="_destinationPath">New target destination path to apply to the work</param>
        /// <param name="_type">New type of save work to apply to the work</param>
        public void ChangeWork(int _nb, string _name, string _sourcePath, string _destinationPath, SaveWorkType _type, List<Extension> _extension)
        {
            if (!IfSaveWorkAlreadyExists(_name))
            {
                if (_type != WorkList[_nb].Type && _type == SaveWorkType.complete)
                {
                    WorkList[_nb] = new CompleteSaveWork(_name, _sourcePath, _destinationPath, _extension);
                }
                else if (_type != WorkList[_nb].Type && _type == SaveWorkType.differencial)
                {
                    WorkList[_nb] = new DifferencialSaveWork(_name, _sourcePath, _destinationPath, _extension);
                }
                else
                {
                    if (_name != "") { WorkList[_nb].Name = _name; }
                    if (_sourcePath != "") { WorkList[_nb].SourcePath = _sourcePath; }
                    if (_destinationPath != "") { WorkList[_nb].DestinationPath = _destinationPath; }
                    WorkList[_nb].ExtentionToEncryptList = _extension;
                }
                SetWorkIndex();

                UpdateSaveFile();
                EditLog.ChangeWorkLogLine(_nb, WorkList);
            }
        }

        /// <summary>
        /// Can delete a save work (set to null)
        /// </summary>
        /// <param name="_nb">Index of the work in the list to delete</param>
        public void DeleteWork(int _nb)
        {
            WorkList.RemoveAt(_nb);
            SetWorkIndex();
            UpdateSaveFile();
            EditLog.DeleteWorkLogLine(_nb);
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
            EditLog.SaveWorkAlreadyExistsLogLine(_name);
            return false;
        }

        /// <summary>
        /// Asign index to work in worklist
        /// </summary>
        public void SetWorkIndex()
        {
            foreach (ISaveWork li in WorkList)
            {
                li.Index = WorkList.IndexOf(li);
            }
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
            return -1;
        }

        /// <summary>
        /// Can initiate a type of save from the numbers of the save work in workList.
        /// </summary>
        /// <param name="_nb">Index of the work in the list to execute the save process</param>
        public void DoSave(int _nb)
        {
            WorkList[_nb].Save();
        }

        /// <summary>
        /// Can initiate a type of save from the name of the save work in workList.
        /// </summary>
        /// <param name="_name"></param>
        public void DoSave(string _name)
        {
            WorkList[GetWorkIndex(_name)].Save();
        }


        /// <summary>
        /// Update the state file with the work list value
        /// </summary>
        /// <param name="_nb">Index of the save work process to update</param>
        public void UpdateSaveFile()
        {
            //Convert the work list to a json string then write it in a json file
            var convertedJson = JsonConvert.SerializeObject(WorkList, Formatting.Indented);
            File.WriteAllText("stateFile.json", convertedJson);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
