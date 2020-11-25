﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class SaveWork
    {

        //SaveWork class contructor from parameters given by the user
        public SaveWork(string _name, string _sourcePath, string _destinationPath, SaveWorkType _type)
        {
            Name = _name;
            CreationTime = DateTime.Now.ToString();
            SourcePath = _sourcePath;
            DestinationPath = _destinationPath;
            Type = _type;
            IsActive = false;
            SaveProgress = null;
        }

        //The actual name of the save work given by the user
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //The source path of the directory to save
        private string sourcePath;

        public string SourcePath
        {
            get { return sourcePath; }
            set { sourcePath = value; }
        }

        //The destion path to store the save
        private string destinationPath;

        public string DestinationPath
        {
            get { return destinationPath; }
            set { destinationPath = value; }
        }

        //The type of save work (complete, differencial or unset)
        private SaveWorkType type;

        public SaveWorkType Type
        {
            get { return type; }
            set { type = value; }
        }

        //Date of the creation of the object
        private string creationTime;

        public string CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }

        //Tell if a saving protocol is active or not to the current SaveWork object
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        //Object defining the save progress when a saving protocol is active
        private SaveProgress saveProgress;

        public SaveProgress SaveProgress
        {
            get { return saveProgress; }
            set { saveProgress = value; }
        }

        //Create a SaveProgress object when a saving protocol starts
        public void CreateSaveProgress(int _totalFilesNumber, long _totalSize, int _filesRemaining, int _progressState, long _sizeRemaining)
        {
            SaveProgress = new SaveProgress(_totalFilesNumber, _totalSize, _filesRemaining, _progressState, _sizeRemaining);
        }

        //Delete the SaveProgress object when the saving protocol stops
        public void DeleteSaveProgress()
        {
            SaveProgress = null;
        }



    }

    //Define all the type of save protocols we can have (unset when no protocol is associate with the save work)
    public enum SaveWorkType
    {
        complete,
        differencial,
        unset
    }
}
