using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class ViewModel
    {
        //public ModelTest SaveExample { get; set; }

        private List<ModelTest> _workList;

        public List<ModelTest> WorkList
        {
            get { return _workList; }
            set { _workList = value; }
        }

        public ViewModel()
        {
            WorkList = new List<ModelTest>();
            WorkList.Add(new ModelTest(1, "Test Save", "C:/balek", "D:/balek*2", "complete", ".gabrex"));
        }
        
    
        #region Methodes

        /// <summary>
        /// 
        /// </summary>
        public void CreateSaveProcedure()
        {

        }
        
        /// <summary>
        /// 
        /// </summary>
        public void ModifySaveProcedure()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteSaveProcedure()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void LaunchSaveProcedure()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void LaunchAllSaveProcedures()
        {

        }

        #endregion

    }
}
