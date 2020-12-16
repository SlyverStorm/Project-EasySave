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
        /// Tells the Model to create a save procedure
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
        /// Tells the Model to modify a save procedure
        /// </summary>
        public void ModifySaveProcedure(int _id, string _name, string _sourcePath, string _destinationPath, SaveWorkType _saveType, List<Extension> _encryptionList)
        {
            Model.ChangeWork(_id, _name, _sourcePath, _destinationPath, _saveType, _encryptionList);
        }

        /// <summary>
        /// Tells the Model to delete a save procedure
        /// </summary>
        public void DeleteSaveProcedure(int _id)
        {
            Model.DeleteWork(_id);
        }

        /// <summary>
        /// Tells the Model to launch a save procedure
        /// </summary>
        public void LaunchSaveProcedure(int _id)
        {
            Model.DoSave(_id);
        }

        /// <summary>
        /// Tells the Model to launch all save procedures
        /// </summary>
        public void LaunchAllSaveProcedures()
        {
            Model.DoAllSave();
        }

        /// <summary>
        /// Tells the Model to pause the current save procedure(s)
        /// </summary>
        public void PauseSaveProcedure(int _index, bool _boolean)
        {
            if (_boolean)
                Model.PauseSave(_index);
            else
                Model.ResumeSave(_index);
        }

        /// <summary>
        /// Tells the Model to cancel the current save procedure(s)
        /// </summary>
        public void CancelSaveProcedure(int _index)
        {
            Model.CancelSave(_index);
        }

        /// <summary>
        /// Tells the Model to pause the current save procedure(s)
        /// </summary>
        public void PauseAllSaveProcedures(bool _boolean)
        {
            if (_boolean)
                Model.PauseAllSave();
            else
                Model.ResumeAllSave();
        }

        /// <summary>
        /// Tells the Model to cancel the current save procedure(s)
        /// </summary>
        public void CancelAllSaveProcedures()
        {
            Model.CancelAllSave();
        }

        #endregion

    }
}
