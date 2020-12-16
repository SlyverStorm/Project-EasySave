using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave_2._0
{

    //Class used to create object that store information during a save protocol (complete or differencial)
    class SaveProgress : INotifyPropertyChanged
    {
        //Contructor, create the object from simple parameters
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
        }

        public static object taken = new object();

        private bool isPaused;

        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }

        private bool cancelled;

        public bool Cancelled
        {
            get { return cancelled; }
            set { cancelled = value; }
        }



        //Time at the launch of the save protocol
        private string launchTime;

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

        public string CurrentDestinationFilePath
        {
            get { return currentDestinationFilePath; }
            set
            { 
                currentDestinationFilePath = value;
                OnPropertyChanged("CurrentDestinationFilePath");
            }
        }


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
