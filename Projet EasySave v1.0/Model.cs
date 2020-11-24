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

        private SaveWork[] workList;

        public SaveWork[] WorkList
        {
            get { return workList; }
            set { workList = value; }
        }


        public void CreateWork(int nb, string name, string sourcePath, string destinationPath, SaveWorkType type)
        {
            workList[nb] = new SaveWork(name, sourcePath, destinationPath, type);
        }

        public void ChangeWork(int nb, string name, string sourcePath, string destinationPath, SaveWorkType type)
        {
            workList[nb].Name = name;
            workList[nb].SourcePath = sourcePath;
            workList[nb].DestinationPath = destinationPath;
            workList[nb].Type = type;
        }

        public void DeleteWork(int nb)
        {
            workList[nb] = null;
        }

        public void DoSave(int nb)
        {

        }





    }
}
