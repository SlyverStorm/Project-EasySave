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
    /// <summary>
    /// Complete Save Work Class (implementing ISaveWork)
    /// </summary>
    class CompleteSaveWork : ISaveWork, INotifyPropertyChanged
    {

        private int index;
        /// <summary>
        /// Index of the save work
        /// </summary>
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
        /// <summary>
        /// Name of the save work
        /// </summary>
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
        /// <summary>
        /// Directory Source Path to save
        /// </summary>
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
        /// <summary>
        /// Target Directory to save files in
        /// </summary>
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
        /// <summary>
        /// Extension List to encrypt after the save is done
        /// </summary>
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
        /// <summary>
        /// Type of Save Work (complete or differential)
        /// </summary>
        public SaveWorkType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        private string creationTime;
        /// <summary>
        /// Creation time of the save work
        /// </summary>
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
        /// <summary>
        /// Boolean of the saving activity (true if the save is active, false if not)
        /// </summary>
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
        /// <summary>
        /// Progress object of the save work
        /// </summary>
        public SaveProgress Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        /// <summary>
        /// Complete Save Work constructor
        /// </summary>
        /// <param name="_name">Save Work Name</param>
        /// <param name="_source">Directory Source Path</param>
        /// <param name="_target">Directory Target Destination Path</param>
        /// <param name="_extension">Extension List to Encrypt</param>
        /// <param name="_type">Save Work Type</param>
        public CompleteSaveWork(string _name, string _source, string _target, List<Extension> _extension, SaveWorkType _type)
        {
            Name = _name;
            SourcePath = _source;
            DestinationPath = _target;
            type = _type;
            ExtentionToEncryptList = _extension;
            CreationTime = DateTime.Now.ToString();
            IsActive = false;
            //Progress = null;
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

        /// <summary>
        /// Launch the saving process
        /// </summary>
        /// <param name="obj">Thread friendly object</param>
        public void Save(object obj)
        {
            EditLog.StartSaveLogLine(this);
            EditLog.LaunchingSaveLogLine(Index);
            CompleteCopy();
        }

        /// <summary>
        /// Do a complete copy from a folder to another
        /// </summary>
        private void CompleteCopy()
        {
            if (Directory.Exists(SourcePath))
            {
                //The Source directory has succesfully been found

                //Search directory info from source and target path
                var diSource = new DirectoryInfo(SourcePath);
                var diTarget = new DirectoryInfo(DestinationPath);

                //Calculate the number of file in the source directory and the total size of it
                int nbFiles = EasySaveInfo.CompleteFilesNumber(diSource);
                long directorySize = EasySaveInfo.CompleteSize(diSource);

                EditLog.FileToSaveFound(this, nbFiles, diSource, directorySize);

                lock (Model.sync)
                {
                    CreateProgress(nbFiles, directorySize, nbFiles, 0, directorySize);
                    IsActive = true;
                }
                Model.OnSaveWorkUpdate();

                //initiate Copy from the source directory to the target directory
                EditLog.StartCopy(this);
                CompleteCopyAll(diSource, diTarget);


                lock (Model.sync)
                {
                    Progress.IsEncrypting = true;
                }
                Model.OnSaveWorkUpdate();

                //Start encryption file
                EditLog.StartEncryption(Index);
                EncryptFiles();
                EditLog.EndEncryption(Index);

                //Closing the complete save protocol
                lock (Model.sync)
                {
                    //DeleteProgress();
                    Progress.IsEncrypting = false;
                    IsActive = false;
                }
                Model.OnSaveWorkUpdate();

                EditLog.EndSaveProgram(Index);
            }
            else
            {
                //The Source Directory has not been found
                Model.OnUpdateModelError("directory");
            }
        }

        /// <summary>
        /// Copy each files from a directory and do the same for each subdirectory using recursion
        /// </summary>
        /// <param name="_nb">Index of the save work</param>
        /// <param name="_source">source directory path</param>
        /// <param name="_target">target destination directory path</param>
        private void CompleteCopyAll(DirectoryInfo _source, DirectoryInfo _target)
        {
            //Check of the different parameter that can stop or cancel the work (parameters stored in the Progress Update)
            if (Progress.Cancelled) return;
            bool softwareIsLaunched = false;
            while(Progress.IsPaused || EasySaveInfo.CheckIfSoftwareIsLaunched(Setting.softwareString))
            {
                if (Progress.Cancelled) return;
                if (EasySaveInfo.CheckIfSoftwareIsLaunched(Setting.softwareString) && !softwareIsLaunched)
                {
                    Model.OnUpdateModelError("software");
                    softwareIsLaunched = true;
                }
            }
            if (softwareIsLaunched) Model.OnUpdateModelError("resume");

            //First create the new target directory where all the files are saved later on
            EditLog.CreateDirectoryLogLine(this, _target);
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

                string elapsedTime = "";

                if(fi.Length >= Setting.maxTransferSize)
                {
                    lock (SaveProgress.taken)
                    {
                        EditLog.StartCopyFileLogLine(this, fi);

                        //Copy the file and measure execution time
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        fi.CopyTo(Path.Combine(_target.FullName, fi.Name), true);
                        watch.Stop();
                        elapsedTime = watch.Elapsed.TotalSeconds.ToString();
                    }
                }
                else
                {
                    EditLog.StartCopyFileLogLine(this, fi);

                    //Copy the file and measure execution time
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    fi.CopyTo(Path.Combine(_target.FullName, fi.Name), true);
                    watch.Stop();
                    elapsedTime = watch.Elapsed.TotalSeconds.ToString();
                }

                


                lock (Model.sync)
                {
                    Progress.FilesRemaining--;
                    Progress.SizeRemaining -= fi.Length;
                    Progress.UpdateProgressState();
                }
                
                Model.OnSaveWorkUpdate();
                EditLog.FinishCopyFileLogLine(this, fi, elapsedTime);

                //Check of the different parameter that can stop or cancel the work (parameters stored in the Progress Update)
                if (Progress.Cancelled) return;
                softwareIsLaunched = false;
                while (Progress.IsPaused || EasySaveInfo.CheckIfSoftwareIsLaunched(Setting.softwareString))
                {
                    if (Progress.Cancelled) return;
                    if (EasySaveInfo.CheckIfSoftwareIsLaunched(Setting.softwareString) && !softwareIsLaunched)
                    {
                        Model.OnUpdateModelError("software");
                        softwareIsLaunched = true;
                    }
                }
                if (softwareIsLaunched) Model.OnUpdateModelError("resume");
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in _source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    _target.CreateSubdirectory(diSourceSubDir.Name);
                EditLog.EnterSubdirectoryLogLine(this, diSourceSubDir);
                CompleteCopyAll(diSourceSubDir, nextTargetSubDir);
                EditLog.ExitSubdirectoryLogLine(this, diSourceSubDir);
            }
        }



        /// <summary>
        /// Encrypt selected files 
        /// </summary>
        public void EncryptFiles()
        {
            if (Progress.Cancelled) return;
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
                        if (Progress.Cancelled) return;
                        //Console.WriteLine(files);
                        // Encrypt File
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        CryptoSoft.CryptoSoftTools.CryptoSoftEncryption(files);
                        watch.Stop();

                        EditLog.EncryptedFile(this, files, watch.Elapsed.TotalSeconds.ToString());
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
                        if (Progress.Cancelled) return;
                        //Console.WriteLine(files);
                        // Encrypt File
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        CryptoSoft.CryptoSoftTools.CryptoSoftEncryption(files);
                        watch.Stop();

                        EditLog.EncryptedFile(this, files, watch.Elapsed.TotalSeconds.ToString());
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
