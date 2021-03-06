﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EasySave_2._0
{

    public delegate void SaveWorkUpdateDelegate();

    public delegate void UpdateGlobalProgress();

    public delegate void UpdateModelError(string _errorString);

    public delegate void ResumeSaveDelegate(int _nb);

    public delegate void PauseSaveDelegate(int _nb);

    public delegate void CancelSaveDelegate(int _nb);


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
            //Asign all delegate
            OnSaveWorkUpdate = UpdateSaveFile;
            OnProgressUpdate = UpdateAllSaveProgress;
            OnUpdateModelError = SetModelError;
            OnSocketResumeSave = ResumeSave;
            OnSocketPauseSave = PauseSave;
            OnSocketCancelSave = CancelSave;
            GlobalProgress = 0;
            //Init new settings
            ModelSettings = new Setting();
            //Check if a setting file already exists, if false then create a new one, if true get the different value from it and apply it to the setting object (ModelSettings).
            if (!File.Exists("settings.json"))
            {
                ModelSettings.MaxTransferSize = 10000;
                ModelSettings.SoftwareString = "";
                ModelSettings.PriorityExtension = new List<Extension>();
                UpdateSettingsFile();
            }
            else
            {
                string settingsFile = File.ReadAllText("settings.json");
                var tempWorkList = JsonConvert.DeserializeObject<Setting>(settingsFile);
                ModelSettings = tempWorkList;
                UpdateSettingsFile();
            }

            WorkList = new List<ISaveWork>();
            //If the state file has not been initialized then create 5 SaveWork object from nothing
            if (!File.Exists("stateFile.json"))
            {
                WorkList.Add(new CompleteSaveWork("Default", "test", "test", null, SaveWorkType.complete));
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

        //Thread friendly / safe object for locking
        public static object sync = new object();

        public static SaveWorkUpdateDelegate OnSaveWorkUpdate;

        public static UpdateGlobalProgress OnProgressUpdate;

        public static UpdateModelError OnUpdateModelError;

        //Store all save works
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

        private Setting modelSettings;
        /// <summary>
        /// Setting object
        /// </summary>
        public Setting ModelSettings
        {
            get { return modelSettings; }
            set 
            {
                modelSettings = value;
                OnPropertyChanged("ModelSettings");
            }
        }

        private string modelError;
        /// <summary>
        /// Model Error handle string
        /// </summary>
        public string ModelError
        {
            get { return modelError; }
            set 
            {
                modelError = value;
                OnPropertyChanged("ModelError");
            }
        }

        /// <summary>
        /// Model Error String update method
        /// </summary>
        /// <param name="_errorString"></param>
        public void SetModelError(string _errorString)
        {
            ModelError = _errorString;
        }

        private double globalProgress;
        /// <summary>
        /// Global Progress State for a all save process
        /// </summary>
        public double GlobalProgress
        {
            get { return globalProgress; }
            set 
            { 
                globalProgress = value;
                OnPropertyChanged("GlobalProgress");
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
            CompleteSaveWork work = new CompleteSaveWork(_name, _source, _destination, _extension, SaveWorkType.complete);
            WorkList.Add(work);
            SetWorkIndex();
            UpdateSaveFile();
            EditLog.CreateWorkLogLine(work);
        }

        /// <summary>
        /// Create a save work (with a differential save algorithm)
        /// </summary>
        /// <param name="_name">Name of the work (must be different from existing ones)</param>
        /// <param name="_source">The Source path to save</param>
        /// <param name="_destination">The Target destination to save files in</param>
        public void CreateDifferencialWork(string _name, string _source, string _destination, List<Extension> _extension)
        {
            DifferencialSaveWork work = new DifferencialSaveWork(_name, _source, _destination, _extension, SaveWorkType.differencial);
            WorkList.Add(work);
            SetWorkIndex();
            UpdateSaveFile();
            EditLog.CreateWorkLogLine(work);
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
            if (_type != WorkList[_nb].Type && _type == SaveWorkType.complete)
            {
                WorkList[_nb] = new CompleteSaveWork(_name, _sourcePath, _destinationPath, _extension, SaveWorkType.complete);
            }
            else if (_type != WorkList[_nb].Type && _type == SaveWorkType.differencial)
            {
                WorkList[_nb] = new DifferencialSaveWork(_name, _sourcePath, _destinationPath, _extension, SaveWorkType.differencial);
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
            EditLog.ChangeWorkLogLine(WorkList[_nb]);
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
        /// Can initiate a type of save from the numbers of the save work in workList.
        /// </summary>
        /// <param name="_nb">Index of the work in the list to execute the save process</param>
        public void DoSave(int _nb)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorkList[_nb].Save));
        }

        public static PauseSaveDelegate OnSocketPauseSave;

        /// <summary>
        /// Pause a specific save
        /// </summary>
        /// <param name="_nb">Index of the work in the list</param>
        public void PauseSave(int _nb)
        {
            lock (sync)
            {
                if (WorkList[_nb].Progress != null && WorkList[_nb].Progress.IsPaused != true)
                {
                    WorkList[_nb].Progress.IsPaused = true;
                    EditLog.SavePaused(_nb);
                }
            }
        }

        public static ResumeSaveDelegate OnSocketResumeSave;

        /// <summary>
        /// Resume a specific save
        /// </summary>
        /// <param name="_nb">Index of the work in the list</param>
        public void ResumeSave(int _nb)
        {
            lock (sync)
            {
                if (WorkList[_nb].Progress != null && WorkList[_nb].Progress.IsPaused != false)
                {
                    WorkList[_nb].Progress.IsPaused = false;
                    EditLog.SaveResumed(_nb);
                }
            }
        }

        public static CancelSaveDelegate OnSocketCancelSave;

        /// <summary>
        /// Cancel a specific save
        /// </summary>
        /// <param name="_nb">Index of the work in the list</param>
        public void CancelSave(int _nb)
        {
            lock (sync)
            {
                if (WorkList[_nb].Progress != null)
                {
                    WorkList[_nb].Progress.Cancelled = true;
                    EditLog.SaveCancelled(_nb);
                }
            }
        }

        /// <summary>
        /// Initiate all save in work list
        /// </summary>
        public void DoAllSave()
        {
            foreach (ISaveWork work in WorkList)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(work.Save));
            }
        }

        /// <summary>
        /// Get the global save progress percentage
        /// </summary>
        /// <returns></returns>
        public void UpdateAllSaveProgress()
        {
            double progressCount = 0;
            foreach (ISaveWork work in WorkList)
            {
                if (work.Progress != null)
                    progressCount += work.Progress.ProgressState;
            }
            GlobalProgress =  progressCount / WorkList.Count;
        }

        /// <summary>
        /// Pause a specific save
        /// </summary>
        public void PauseAllSave()
        {
            lock (sync)
            {
                foreach (ISaveWork work in WorkList)
                {
                    if (work.Progress != null && work.Progress.IsPaused != true) work.Progress.IsPaused = true;
                } 
            }
        }

        /// <summary>
        /// Resume a specific save
        /// </summary>
        public void ResumeAllSave()
        {
            lock (sync)
            {
                foreach (ISaveWork work in WorkList)
                {
                    if (work.Progress != null && work.Progress.IsPaused != false) work.Progress.IsPaused = false;
                }
            }
        }

        /// <summary>
        /// Cancel a specific save
        /// </summary>
        public void CancelAllSave()
        {
            lock (sync)
            {
                foreach (ISaveWork work in WorkList)
                {
                    if (work.Progress != null) work.Progress.Cancelled = true;
                }
            }
        }

        /// <summary>
        /// Update the state file with the work list value
        /// </summary>
        /// <param name="_nb">Index of the save work process to update</param>
        public void UpdateSaveFile()
        {
            lock (sync)
            {
                //Convert the work list to a json string then write it in a json file
                var convertedJson = JsonConvert.SerializeObject(WorkList, Formatting.Indented);
                File.WriteAllText("stateFile.json", convertedJson);
            }
        }

        /// <summary>
        /// Update Setting file method (write the setting object in a json file)
        /// </summary>
        public void UpdateSettingsFile()
        {
            lock (sync)
            {
                //Convert the settings to a json string then write it in a json file
                var convertedJson = JsonConvert.SerializeObject(ModelSettings, Formatting.Indented);
                File.WriteAllText("settings.json", convertedJson);
            }
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
