using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class SaveWorkTest
    {
        #region Variables

        private int saveID;

        public int SaveID
        {
            get { return saveID; }
            set { saveID = value; }
        }

        private string saveName;

        public string SaveName
        {
            get { return saveName; }
            set { saveName = value; }
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

        private SaveWorkTestType saveType;

        public SaveWorkTestType SaveType
        {
            get { return saveType; }
            set { saveType = value; }
        }

        private List<SaveWorkTestExtension> extensionList;

        public List<SaveWorkTestExtension> ExtensionList
        {
            get { return extensionList; }
            set { extensionList = value; }
        }

        #endregion

        #region Constructor

        public SaveWorkTest(int _saveID, string _saveName, string _sourcePath, string _destinationPath, SaveWorkTestType _saveType, List<SaveWorkTestExtension> _extensionList)
        {
            SaveID = _saveID;
            SaveName = _saveName;
            SourcePath = _sourcePath;
            DestinationPath = _destinationPath;
            SaveType = _saveType;
            ExtensionList = _extensionList;
        }

        #endregion

        #region Enums

        public enum SaveWorkTestType
        {
            complete,
            differencial
        }

        public enum SaveWorkTestExtension
        {
            txt,
            rar,
            zip,
            docx,
            mpp,
            pptx,
            jpg,
            png,
            pdf,
            exe,
            iso,
            gif,
            mp3,
            mp4,
            ALL
        }

        #endregion
    }
}
