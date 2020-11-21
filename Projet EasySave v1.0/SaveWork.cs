using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class SaveWork
    {

        public SaveWork()
        {

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

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private SaveStateLog stateLog;

        public SaveStateLog StateLog
        {
            get { return stateLog; }
            set { stateLog = value; }
        }

        public void Save()
        {

        }



    }
}
