using Newtonsoft.Json;
using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace EasySave_2._0
{
    class DifferencialSaveWork : ISaveWork, INotifyPropertyChanged
    {
        private int index;
        public int Index
        {
            get { return index; }
            set 
            { 
                index = value;
                OnPropertyChanged("Index");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string sourcePath;
        public string SourcePath
        {
            get { return sourcePath; }
            set 
            {
                sourcePath = value;
                OnPropertyChanged("SourcePath");
            }
        }

        private string destinationPath;
        public string DestinationPath
        {
            get { return destinationPath; }
            set 
            {
                destinationPath = value;
                OnPropertyChanged("DestinationPath");
            }
        }

        private List<Extension> extentionToEncryptList;

        public List<Extension> ExtentionToEncryptList
        {
            get { return extentionToEncryptList; }
            set 
            {
                extentionToEncryptList = value;
                OnPropertyChanged("ExtensionToEncryptList");
            }
        }

        private SaveWorkType type;
        public SaveWorkType Type
        {
            get { return type; }
        }

        private string creationTime;
        public string CreationTime
        {
            get { return creationTime; }
            set 
            { 
                creationTime = value;
                OnPropertyChanged("CreationTime");
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set 
            { 
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        private SaveProgress progress;

        public SaveProgress Progress
        {
            get { return progress; }
            set 
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public DifferencialSaveWork(string _name, string _source, string _target)
        {
            Name = _name;
            SourcePath = _source;
            DestinationPath = _target;
            type = SaveWorkType.differencial;
            CreationTime = DateTime.Now.ToString();
            //ExtentionToEncryptList = _extensionList;
            IsActive = false;
            Progress = null;
        }

        /// <summary>
        /// Create a new save progress object, that store information of the running save
        /// </summary>
        /// <param name="_totalFilesNumber">Number of files in the directory to save (total number)</param>
        /// <param name="_totalSize">Total size (in Bytes) of the directory</param>
        /// <param name="_filesRemaining">Number of file(s) remaining to save</param>
        /// <param name="_progressState">The progress percentage of the save (0-100)</param>
        /// <param name="_sizeRemaining">Size remaining (in Bytes) to save</param>
        public void CreateProgress(int _totalFilesNumber, long _totalSize, int _filesRemaining, int _progressState, long _sizeRemaining)
        {
            Progress = new SaveProgress(_totalFilesNumber, _totalSize, _filesRemaining, _progressState, _sizeRemaining);
        }

        /// <summary>
        /// Delete the SaveProgress object when the saving protocol stops
        /// </summary>
        public void DeleteProgress()
        {
            Progress = null;
        }

        public void Save()
        {
            //TODO: relation avec event !!!

            //CreateLogLine("Launching save work from work : " + work.Name + ", type : complete save");
            DifferencialCopy();
            //CreateLogLine(work.Name + " save DONE !");
        }

        private void DifferencialCopy()
        {
            //Search directory info from source and target path
            var diSource = new DirectoryInfo(SourcePath);
            var diTarget = new DirectoryInfo(DestinationPath);

            //Calculate the number of file in the source directory and the total size of it (of all )
            int nbFiles = EasySaveInfo.DifferencialGetFilesNumberInSourceDirectory(diSource, diTarget);
            long directorySize = EasySaveInfo.DifferencialGetSizeInSourceDirectory(diSource, diTarget);

            //If there is at least one file to save then initiate the differencial saving protocol
            if (nbFiles != 0)
            {
                //CreateLogLine(nbFiles + " files to save found from " + _sourceDirectory + ",Total size of the directory: " + directorySize + " Bytes");

                CreateProgress(nbFiles, directorySize, nbFiles, 0, directorySize);
                IsActive = true;

                Model.OnSaveWorkUpdate(Index);

                //initiate Copy from the source directory to the target directory (only the file / directory that has been modified or are new)
                //CreateLogLine("Saving file from " + _sourceDirectory + " to " + _targetDirectory + " ...");
                DifferencialCopyAll(diSource, diTarget);

                DeleteProgress();
                IsActive = false;
                Model.OnSaveWorkUpdate(Index);
            }
            //If there is no file to save then cancel the saving protocol
            else
            {
                //CreateLogLine("There is no file to save in the target directory");
            }

            //CreateLogLine("Closing differencial save work program ...");
        }

        /// <summary>
        /// Copy each files (that has been modified since the last save) from a directory, and do the same for each subdirectory using recursion
        /// </summary>
        /// <param name="_nb">Index of the save work</param>
        /// <param name="_source">source directory path</param>
        /// <param name="_target">target destination directory path</param>
        private void DifferencialCopyAll(DirectoryInfo _source, DirectoryInfo _target)
        {
            //CreateLogLine("Creating target directory ...");
            Directory.CreateDirectory(_target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in _source.GetFiles())
            {
                //Calculate the path of the future file we need to save
                string targetPath = Path.Combine(_target.FullName, fi.Name);

                //Check if the file already exist or not (new one), and verify if it has been modified or not
                if (!File.Exists(targetPath) || fi.LastWriteTime != File.GetLastWriteTime(targetPath))
                {
                    Progress.CurrentSourceFilePath = fi.FullName;
                    Progress.CurrentDestinationFilePath = Path.Combine(_target.FullName, fi.Name);
                    Model.OnSaveWorkUpdate(Index);
                    //CreateLogLine("Saving " + fi.FullName + " in " + WorkList[_nb - 1].SaveProgress.CurrentDestinationFilePath + ", size : " + fi.Length + " Bytes ...");

                    //Copy the file and measure execution time
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    fi.CopyTo(targetPath, true);
                    watch.Stop();

                    Progress.FilesRemaining--;
                    Progress.SizeRemaining -= fi.Length;
                    Model.OnSaveWorkUpdate(Index);
                    //CreateLogLine(fi.Name + " succesfully saved ! Time spend : " + watch.Elapsed.TotalSeconds.ToString());
                }


            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in _source.GetDirectories())
            {
                string targetDirectoryPath = Path.Combine(_target.FullName, diSourceSubDir.Name);
                //CreateLogLine("Entering subdirectory : " + diSourceSubDir.Name);

                //Check if the directory already exist to decide if it is required to create a new one or not
                if (!Directory.Exists(targetDirectoryPath))
                {
                    DirectoryInfo nextTargetSubDir = _target.CreateSubdirectory(diSourceSubDir.Name);
                    DifferencialCopyAll(diSourceSubDir, nextTargetSubDir);
                }
                else
                {
                    DirectoryInfo nextTargetSubDir = new DirectoryInfo(targetDirectoryPath);
                    DifferencialCopyAll(diSourceSubDir, nextTargetSubDir);
                }

                //CreateLogLine("Exiting subdirectory : " + diSourceSubDir.Name);

            }
        }



        public void EncryptFiles()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
