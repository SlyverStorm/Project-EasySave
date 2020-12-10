using Newtonsoft.Json;
using System;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{

    public delegate void SaveWorkUpdateDelegate(int _index);

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
                WorkList.Add(new CompleteSaveWork("Default", "", ""));
                UpdateSaveFile(-1);
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
                        CreateCompleteWork(work.Name, work.SourcePath, work.DestinationPath);
                    }
                    else if (work.Type == SaveWorkType.differencial)
                    {
                        CreateDifferencialWork(work.Name, work.SourcePath, work.DestinationPath);
                    }
                }
                UpdateSaveFile(-1);
            }
            
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
            SetWorkIndex();
            UpdateSaveFile(-1);
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
            SetWorkIndex();
            UpdateSaveFile(-1);
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
            SetWorkIndex();

            UpdateSaveFile(_nb);
            //CreateLogLine("Modification of a existing save work in position " + _nb + ", current parameters : name : " + WorkList[_nb - 1].Name + ", source path : " + WorkList[_nb - 1].SourcePath + ", destination path : " + WorkList[_nb - 1].DestinationPath + ", type : " + WorkList[_nb - 1].Type);
        }

        /// <summary>
        /// Can delete a save work (set to null)
        /// </summary>
        /// <param name="_nb">Index of the work in the list to delete</param>
        public void DeleteWork(int _nb)
        {
            WorkList.RemoveAt(_nb);
            SetWorkIndex();
            UpdateSaveFile(_nb);
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
        /// 
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
        /// Update the state file with the work list value
        /// </summary>
        /// <param name="_nb">Index of the save work process to update</param>
        public void UpdateSaveFile(int _nb)
        {
            if (_nb >= 0)
            {
                //Check is a save protocol is active or not
                if (WorkList[_nb - 1].IsActive)
                {
                    long sizeDifference = WorkList[_nb].Progress.TotalSize - WorkList[_nb].Progress.SizeRemaining;

                    //Check if the difference in size is equal to 0, to avoid division by 0
                    if (sizeDifference != 0)
                    {
                        WorkList[_nb].Progress.ProgressState = ((WorkList[_nb].Progress.TotalSize - WorkList[_nb].Progress.SizeRemaining) / WorkList[_nb - 1].Progress.TotalSize * 100);
                    }
                }
            }

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
