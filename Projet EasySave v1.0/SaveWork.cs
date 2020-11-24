using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class SaveWork
    {

        public SaveWork(string name, string sourcePath, string destinationPath, SaveWorkType type)
        {
            Name = name;
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Type = type;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string sourcePath;

        public string SourcePath
        {
            get { return sourcePath; }
            set { sourcePath = value; }
        }

        private string destinationPath;

        public string DestinationPath
        {
            get { return destinationPath; }
            set { destinationPath = value; }
        }

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
        differencial
    }
}
