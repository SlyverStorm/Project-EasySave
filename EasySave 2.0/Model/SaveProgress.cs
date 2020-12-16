using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave_2._0
{
    /// <summary>
    /// Class used to create object that store information during a save protocol (complete or differencial)
    /// </summary>
    class SaveProgress : INotifyPropertyChanged
    {
        /// <summary>
        /// Contructor, create the object from simple parameters
        /// </summary>
        /// <param name="_totalFilesNumber">Total file number to save</param>
        /// <param name="_totalSize">Total size to save</param>
        /// <param name="_filesRemaining">Total file remaining</param>
        /// <param name="_progressState">Percentage of progress of the save</param>
        /// <param name="_sizeRemaining">Total size remaining</param>
        public SaveProgress(int _totalFilesNumber, long _totalSize, int _filesRemaining, long _progressState, long _sizeRemaining)
        {
            //Enter the current time at the creation of the object
            LaunchTime = DateTime.Now.ToString();
            TotalFilesNumber = _totalFilesNumber;
            TotalSize = _totalSize;
            FilesRemaining = _filesRemaining;
            ProgressState = _progressState;
            SizeRemaining = _sizeRemaining;
            CurrentDestinationFilePath = null;
            CurrentSourceFilePath = null;
            IsPaused = false;
            Cancelled = false;
            IsEncrypting = false;
        }

        /// <summary>
        /// Thread friendly object for oversized files
        /// </summary>
        public static object taken = new object();

        private bool isPaused;
        /// <summary>
        /// Pause parameter
        /// </summary>
        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }

        private bool cancelled;
        /// <summary>
        /// Cancel parameter (if true : exit the save asap)
        /// </summary>
        public bool Cancelled
        {
            get { return cancelled; }
            set { cancelled = value; }
        }

        //Time at the launch of the save protocol
        private string launchTime;
        /// <summary>
        /// Save launch time
        /// </summary>
        public string LaunchTime
        {
            get { return launchTime; }
            set
            { 
                launchTime = value;
                OnPropertyChanged("LaunchTime");
            }
        }

        //Total file to save
        private int totalFilesNumber;
        /// <summary>
        /// Total file number to save
        /// </summary>
        public int TotalFilesNumber
        {
            get { return totalFilesNumber; }
            set 
            { 
                totalFilesNumber = value;
                OnPropertyChanged("TotalFilesNumber");
            }
        }

        //Total size to copy
        private long totalSize;
        /// <summary>
        /// Total file size to save
        /// </summary>
        public long TotalSize
        {
            get { return totalSize; }
            set 
            {
                totalSize = value;
                OnPropertyChanged("TotalSize");
            }
        }

        //Total file remaining to save
        private int filesRemaining;
        /// <summary>
        /// Files remaining to save
        /// </summary>
        public int FilesRemaining
        {
            get { return filesRemaining; }
            set
            { 
                filesRemaining = value;
                OnPropertyChanged("FilesRemaining");
            }
        }

        //Percent of progress in the save protocol
        private double progressState;
        /// <summary>
        /// Percentage of progress
        /// </summary>
        public double ProgressState
        {
            get { return progressState; }
            set
            {
                progressState = value;
                OnPropertyChanged("ProgressState");
            }
        }

        //Size remaining to save
        private long sizeRemaining;
        /// <summary>
        /// Size remaining to save
        /// </summary>
        public long SizeRemaining
        {
            get { return sizeRemaining; }
            set 
            { 
                sizeRemaining = value;
                OnPropertyChanged("SizeRemaining");
            }
        }

        //Source path of the current file we need to save
        private string currentSourceFilePath;
        /// <summary>
        /// Current file source path to saving
        /// </summary>
        public string CurrentSourceFilePath
        {
            get { return currentSourceFilePath; }
            set 
            { 
                currentSourceFilePath = value;
                OnPropertyChanged("CurrentSourceFilePath");
            }
        }

        //Target path of the current file we need to save
        private string currentDestinationFilePath;
        /// <summary>
        /// Current file destination path to saving
        /// </summary>
        public string CurrentDestinationFilePath
        {
            get { return currentDestinationFilePath; }
            set
            { 
                currentDestinationFilePath = value;
                OnPropertyChanged("CurrentDestinationFilePath");
            }
        }

        private bool isEncrypting;
        /// <summary>
        /// Encryption process parameter
        /// </summary>
        public bool IsEncrypting
        {
            get { return isEncrypting; }
            set 
            { 
                isEncrypting = value;
                OnPropertyChanged("IsEncrypting");
            }
        }

        /// <summary>
        /// Update the progress state
        /// </summary>
        public void UpdateProgressState()
        {
            double sizeDifference = TotalSize - SizeRemaining;

            //Check if the difference in size is equal to 0, to avoid division by 0
            if (sizeDifference != 0)
            {
                ProgressState = sizeDifference / TotalSize * 100;
            }
            Model.OnProgressUpdate();
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
