using Newtonsoft.Json;
using System;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EasySave_2._0
{
    class CompleteSaveWork : ISaveWork, INotifyPropertyChanged
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

        public CompleteSaveWork(string _name, string _source, string _target, List<Extension> _extension)
        {
            Name = _name;
            SourcePath = _source;
            DestinationPath = _target;
            type = SaveWorkType.complete;
            ExtentionToEncryptList = _extension;
            CreationTime = DateTime.Now.ToString();
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

        public void Save(object obj)
        {
            EditLog.StartSaveLogLine(this);
            EditLog.LaunchingSaveLogLine(Index);
            CompleteCopy();
            EditLog.EndSaveProgram(Index);

            EditLog.StartEncryption(Index);
            EncryptFiles();
            EditLog.EndEncryption(Index);
        }

        /// <summary>
        /// Do a complete copy from a folder to another
        /// </summary>
        private void CompleteCopy()
        {
            //Search directory info from source and target path
            var diSource = new DirectoryInfo(SourcePath);
            var diTarget = new DirectoryInfo(DestinationPath);

            //Calculate the number of file in the source directory and the total size of it
            int nbFiles = EasySaveInfo.CompleteFilesNumber(diSource);
            long directorySize = EasySaveInfo.CompleteSize(diSource);

            EditLog.FileToSaveFound(nbFiles, diSource, directorySize);

            lock (Model.sync)
            {
                CreateProgress(nbFiles, directorySize, nbFiles, 0, directorySize);
                IsActive = true;
            }
            Model.OnSaveWorkUpdate();

            //initiate Copy from the source directory to the target directory
            EditLog.StartCopy(this);
            CompleteCopyAll(diSource, diTarget);

            //Closing the complete save protocol
            lock (Model.sync)
            {
                //DeleteProgress();
                IsActive = false;
            }
            Model.OnSaveWorkUpdate();
        }

        /// <summary>
        /// Copy each file from a directory, and do the same for each subdirectory using recursion
        /// </summary>
        private void CompleteCopyAll(DirectoryInfo _source, DirectoryInfo _target)
        {
            if (Progress.Cancelled) return;
            while(Progress.IsPaused)
            {
                if (Progress.Cancelled) return;
            }

            //First create the new target directory where all the files are saved later on
            EditLog.CreateDirectoryLogLine(_target);
            Directory.CreateDirectory(_target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in _source.GetFiles())
            {
                lock (Model.sync)
                {
                    Progress.CurrentSourceFilePath = fi.FullName;
                    Progress.CurrentDestinationFilePath = Path.Combine(_target.FullName, fi.Name);
                }
                Model.OnSaveWorkUpdate();

                EditLog.StartCopyFileLogLine(fi);

                //Copy the file and measure execution time
                Stopwatch watch = new Stopwatch();
                watch.Start();
                fi.CopyTo(Path.Combine(_target.FullName, fi.Name), true);
                watch.Stop();


                lock (Model.sync)
                {
                    Progress.FilesRemaining--;
                    Progress.SizeRemaining -= fi.Length;
                    Progress.UpdateProgressState();
                }
                
                Model.OnSaveWorkUpdate();
                EditLog.FinishCopyFileLogLine(fi, watch.Elapsed.TotalSeconds.ToString());

                if (Progress.Cancelled) return;
                while (Progress.IsPaused)
                {
                    if (Progress.Cancelled) return;
                }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in _source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    _target.CreateSubdirectory(diSourceSubDir.Name);
                EditLog.EnterSubdirectoryLogLine(diSourceSubDir);
                CompleteCopyAll(diSourceSubDir, nextTargetSubDir);
                EditLog.ExitSubdirectoryLogLine(diSourceSubDir);
            }
        }



        /// <summary>
        /// Encrypt selected files 
        /// </summary>
        public void EncryptFiles()
        {
            if (ExtentionToEncryptList != null && Directory.Exists(DestinationPath))
            {
                // If we encrypt all files
                if (extentionToEncryptList.Contains(Extension.ALL))
                {
                    // Find all files
                    string[] filesPathToEncrypt = Directory.GetFiles(destinationPath, "*.*", SearchOption.AllDirectories);
                    // For each files
                    foreach (string files in filesPathToEncrypt)
                    {
                        Console.WriteLine(files);
                        // Encrypt File
                        CryptoSoft.CryptoSoftTools.CryptoSoftEncryption(files);
                    }
                }

                // for each exntensions in the list
                foreach (Extension extension in extentionToEncryptList)
                {
                    // Adjusts the format of the extension
                    string extensionReformated = "*." + extension.ToString() + "*";
                    // Find all files in directory with aimed extensions
                    string[] filesPathToEncrypt = Directory.GetFiles(destinationPath, extensionReformated, SearchOption.AllDirectories);
                    // For each files with aimed extensions
                    foreach (string files in filesPathToEncrypt)
                    {
                        Console.WriteLine(files);
                        // Encrypt File
                        CryptoSoft.CryptoSoftTools.CryptoSoftEncryption(files);
                    }
                }
            }
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
