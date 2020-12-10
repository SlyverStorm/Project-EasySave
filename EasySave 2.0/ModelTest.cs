using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave_2._0
{
    class ModelTest : INotifyPropertyChanged
    {
        #region Variables
        private int _idSave;

        public int IdSave
        {
            get { return _idSave; }
            set
            {
                _idSave = value;
                OnPropertyChanged("IdSave");
            }
        }

        private string _saveName;

        public string SaveName
        {
            get { return _saveName; }
            set

            {
                _saveName = value;
                OnPropertyChanged("SaveName");
            }
        }

        private string _sourcePath;

        public string SourcePath
        {
            get { return _sourcePath; }
            set
            {
                _sourcePath = value;
                OnPropertyChanged("SourcePath");
            }
        }

        private string _destinationPath;

        public string DestinationPath
        {
            get { return _destinationPath; }
            set
            {
                _destinationPath = value;
                OnPropertyChanged("DestinationPath");
            }
        }

        private ModelTestType _saveType;

        public ModelTestType SaveType
        {
            get { return _saveType; }
            set
            {
                _saveType = value;
                OnPropertyChanged("SaveType");
            }
        }

        private string _encryption;

        public string Encryption
        {
            get { return _encryption; }
            set
            {
                _encryption = value;
                OnPropertyChanged("Encryption");
            }
        }
        #endregion

        public ModelTest(int _idSave, string _saveName, string _sourcePath, string _destinationPath, ModelTestType _saveType, string _encryption)
        {
            IdSave = _idSave;
            SaveName = _saveName;
            SourcePath = _sourcePath;
            DestinationPath = _destinationPath;
            SaveType = _saveType;
            Encryption = _encryption;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public enum ModelTestType
        {
            complete,
            differencial
        }

        public enum Extension
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
    }
}
