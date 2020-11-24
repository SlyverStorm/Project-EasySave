using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class Model
    {

        public Model()
        {
            WorkList = new SaveWork[5];
            for (int i = 0; i < 5; i++)
            {
                WorkList[i] = new SaveWork("", "", "", SaveWorkType.unset);
            }
        }

        private SaveWork[] workList;

        public SaveWork[] WorkList
        {
            get { return workList; }
            set { workList = value; }
        }      

    }
}
