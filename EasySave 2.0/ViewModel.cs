using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class ViewModel
    {
        
        private List<ModelTest> _workList;

        public List<ModelTest> WorkList
        {
            get { return _workList; }
            set { _workList = value; }
        }

        private ModelTest modelTest;

        public ModelTest ModelTest
        {
            get { return modelTest; }
            set { modelTest = value; }
        }

        public ViewModel()
        {
            ModelTest = new ModelTest();
        }
        
    
        #region Methodes

        /// <summary>
        /// 
        /// </summary>
        public void CreateSaveProcedure(string _name, string _sourcePath, string _destinationPath, SaveWorkTest.SaveWorkTestType _saveType, List<SaveWorkTest.SaveWorkTestExtension> _extensionList)
        {
            if(_saveType == SaveWorkTest.SaveWorkTestType.complete)
            {
                ModelTest.CreateCompleteWork(_name, _sourcePath, _destinationPath, _extensionList);
            }
            else
            {
                ModelTest.CreateDifferencialWork(_name, _sourcePath, _destinationPath, _extensionList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ModifySaveProcedure(int _id, string _name, string _sourcePath, string _destinationPath, SaveWorkTest.SaveWorkTestType _saveType, List<SaveWorkTest.SaveWorkTestExtension> _encryptionList)
        {
            //ChangeWork(_id, _name, _sourcePath, _destinationPath, _type, _encryptionList);
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
