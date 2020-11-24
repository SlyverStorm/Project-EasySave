using System;
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
            SourcePath = _sourcePath;
            DestinationPath = _destinationPath;
            Type = _type;
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


    }

    public enum SaveWorkType
    {
        complete,
        differencial,
        unset
    }
}
