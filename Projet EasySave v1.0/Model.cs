using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class Model
    {

        public Model()
        {
            
        }

        private List<SaveWork> workList;

        public List<SaveWork> WorkList
        {
            get { return workList; }
            set { workList = value; }
        }

        private SaveHistoryLog saveHistoryLog;

        public SaveHistoryLog SaveHistoryLog
        {
            get { return saveHistoryLog; }
            set { saveHistoryLog = value; }
        }

        public void CreateWork(int nb, string name, string sourcePath, string destinationPath, string type)
        {

        }

        public void ChangeWork(int nb, string name, string sourcePath, string destinationPath, string type)
        {

        }

        public void DeleteWork(int nb)
        {

        }

        public void DoSave(int nb)
        {

        }





    }
}
