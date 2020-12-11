using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class ViewModel
    {
        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public ViewModel()
        {
            Model = new Model();
        }
        
    
        #region Methodes

        /// <summary>
        /// 
        /// </summary>
        public void CreateSaveProcedure(string _name, string _sourcePath, string _destinationPath, SaveWorkType _saveType, List<Extension> _extensionList)
        {
            if(_saveType == SaveWorkType.complete)
            {
                Model.CreateCompleteWork(_name, _sourcePath, _destinationPath, _extensionList);
            }
            else
            {
                Model.CreateDifferencialWork(_name, _sourcePath, _destinationPath, _extensionList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ModifySaveProcedure(int _id, string _name, string _sourcePath, string _destinationPath, SaveWorkType _saveType, List<Extension> _encryptionList)
        {
            Model.ChangeWork(_id, _name, _sourcePath, _destinationPath, _saveType, _encryptionList);
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteSaveProcedure(int _id)
        {
            Model.DeleteWork(_id);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LaunchSaveProcedure(int _id)
        {
            Model.DoSave(_id);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LaunchAllSaveProcedures()
        {
            //TO DO
        }

        #endregion

    }
}
