﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_2._0
{
    static class EditLog
    {

        /// <summary>
        /// Create the line to record in the log file
        /// </summary>
        /// <param name="_content">Content to write in the log</param>
        private static void CreateLogLine(int idSave, string _content)
        {
            lock (Model.sync)
            {
                //Check if file log.json doesn't exists, if so then create it and initialize it
                if (!File.Exists("log.json"))
                {
                    File.WriteAllText("log.json", "[]");
                }
                //New LogLine object with Time and content
                LogLine newLogLine = new LogLine(_content);

                //Create a raw string from the json log file
                string JsonLog = File.ReadAllText("log.json");

                //Convert the raw string into a LogLine object list
                var LogList = JsonConvert.DeserializeObject<List<LogLine>>(JsonLog);

                //Add the new object to the list
                LogList.Add(newLogLine);

                //Convert the LogLine object list into a json formated string
                var convertedJson = JsonConvert.SerializeObject(LogList, Formatting.Indented);

                //Write the new string into the json log file
                File.WriteAllText("log.json", convertedJson);

                SocketGestion.ToSendLog(idSave, _content); 

            }
        }


        /// <summary>
        /// Create a Log Line about: the initialisation of the Software with the date.
        /// </summary>
        public static void InitSoftwareLogLine()
        {
            int initSoftwareID = -1; 
            CreateLogLine(initSoftwareID, "Initialisation of the Sofware at: " + DateTime.Now);
        }

        /// <summary>
        /// Create a Log Line about: the creation of a new save.
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="_work">WorkList of the save</param>
        public static void CreateWorkLogLine(ISaveWork _work)
        {
            CreateLogLine(_work.Index, "Creation of a new save work, name : " + _work.Name + ", source path : " + _work.SourcePath + ", destination path : " + _work.DestinationPath + ", type : " + _work.Type);
        }

        /// <summary>
        /// Create a Log Line about: the modification of a save.
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        public static void ChangeWorkLogLine(ISaveWork _work)
        {
            CreateLogLine(_work.Index, "Modification of a existing save work in position " + _work.Index + ", current parameters : name : " + _work.Name + ", source path : " + _work.SourcePath + ", destination path : " + _work.DestinationPath + ", type : " + _work.Type);
        }

        /// <summary>
        /// Create a Log Line about: The supression of a save.
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        public static void DeleteWorkLogLine(int _nb)
        {
            CreateLogLine(_nb, "Supression of save work in position" + _nb);
        }

        /*
        /// <summary>
        /// Create a Log Line about: The research of an existing savework
        /// </summary>
        /// <param name="saveWorkName">Name of the save</param>
        public static void SaveWorkAlreadyExistsLogLine(string _saveWorkName)
        {
            CreateLogLine("The search savework named : " + _saveWorkName + " already exist");
        }
        */

        /// <summary>
        /// Create a Log Line about: The launch of a savework 
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        public static void LaunchingSaveLogLine(int _nb)
        {
            CreateLogLine(_nb, "Launching of the savework " + _nb);
        }

        /// <summary>
        /// Create a Log Line about: The current save 
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        /// <param name="fi">FileInfo of the file</param>
        public static void SavingInfoLogLine(ISaveWork _work, FileInfo _fi)
        {
            CreateLogLine(_work.Index, "Saving " + _fi.FullName + " in " + _work.Progress.CurrentDestinationFilePath + ", size : " + _fi.Length + " Bytes");
        }

        /// <summary>
        /// Create a Log Line about: The start of a saving action
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        public static void StartSaveLogLine(ISaveWork _work)
        {
            CreateLogLine(_work.Index, "Launching save work from work : " + _work.Name + ", type : " + _work.Type);
        }

        /// <summary>
        /// Create a Log Line about: The end of a saving action
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        /// <param name="timeSpend">time spend between the start and the end of the save</param>
        public static void FinishSaveLogLine(ISaveWork _work, string _timeSpend)
        {
            CreateLogLine(_work.Index, _work.Name + " succesfully saved ! Time spend : " + _timeSpend);
        }

        /// <summary>
        /// Create a Log Line about: The creation of a new directory
        /// </summary>
        /// <param name="filePath">DirectoryInfo of the file</param>
        public static void CreateDirectoryLogLine(ISaveWork _work, DirectoryInfo _filePath)
        {
            CreateLogLine(_work.Index, "Creating target directory in : " + _filePath.FullName);
        }

        /// <summary>
        /// Create a Log Line about: Files found to save
        /// </summary>
        /// <param name="nbFiles">Number of files found</param>
        /// <param name="sourceDirectory">DirectoryInfo of the source directory</param>
        /// <param name="directorySize">Total size of the directory</param>
        public static void FileToSaveFound(ISaveWork _work, int _nbFiles, DirectoryInfo _sourceDirectory, long _directorySize)
        {
            CreateLogLine(_work.Index, _nbFiles + " files to save found from " + _sourceDirectory.Name + ",Total size of the directory: " + _directorySize + " Bytes");
        }

        /// <summary>
        /// Create a Log Line about: Start copy a file
        /// </summary>
        /// <param name="fi">FileInfo of the file</param>
        public static void StartCopyFileLogLine(ISaveWork _work, FileInfo _fi)
        {
            CreateLogLine(_work.Index, "Start copy" + _fi.Name);

        }

        /// <summary>
        /// Create a Log Line about: End copy a file
        /// </summary>
        /// <param name="fi">FileInfo of the file</param>
        /// <param name="timeSpend"></param>
        public static void FinishCopyFileLogLine(ISaveWork _work, FileInfo _fi, string _timeSpend)
        {
            CreateLogLine(_work.Index, _fi.Name + " succesfully copy ! Time spend : " + _timeSpend + " second(s)");

        }

        /// <summary>
        /// Create a Log Line about: Entering in a subdirectory
        /// </summary>
        /// <param name="subDir">DirectoryInfo of the subdirectory</param>
        public static void EnterSubdirectoryLogLine(ISaveWork _work, DirectoryInfo _subDir)
        {
            CreateLogLine(_work.Index, "Entering subdirectory : " + _subDir.Name);
        }

        /// <summary>
        /// Create a Log Line about: Exiting a subdirectory
        /// </summary>
        /// <param name="subDir">DirectoryInfo of the subdirectory</param>
        public static void ExitSubdirectoryLogLine(ISaveWork _work, DirectoryInfo _subDir)
        {
            CreateLogLine(_work.Index, "Exiting subdirectory : " + _subDir.Name);
        }

        /// <summary>
        /// Create a Log Line about: The end of the save work program
        /// </summary>
        public static void EndSaveProgram(int _nb)
        {
            CreateLogLine(_nb, "SAVE DONE ! Ending save work program, Index : " + _nb);
        }

        /// <summary>
        /// Create a Log Line about: Startion encryption program in a specific save work
        /// </summary>
        public static void StartEncryption(int _nb)
        {
            CreateLogLine(_nb, "Starting files encryption, Index : " + _nb);
        }

        /// <summary>
        /// Create a Log Line about: The end of the encryption program of a specific save work
        /// </summary>
        public static void EndEncryption(int _nb)
        {
            CreateLogLine(_nb, "ENCRYPTION DONE ! Ending encyption program, Index : " + _nb);
        }

        /// <summary>
        /// Create a Log Line about: Starting the copy of a specific file
        /// </summary>
        /// <param name="_work">Save Work Object</param>
        public static void StartCopy(ISaveWork _work)
        {
            CreateLogLine(_work.Index, "Saving file from " + _work.SourcePath + " to " + _work.DestinationPath + " ...");
        }

        /// <summary>
        /// Create a Log Line about: When no files is found in source directory
        /// </summary>
        /// <param name="_nb">Index of the save</param>
        public static void NoFilesFound(int _nb)
        {
            CreateLogLine(_nb, "There is no file to save in the target directory, Save Index : " + _nb);
        }

        public static void EncryptedFile(ISaveWork _work, string _fi, string _time)
        {
            CreateLogLine(_work.Index, "Index : "+ _work.Index +" ,File Enctyption of " + _fi + " Done ! elapsed time : "+ _time);
        }

        /// <summary>
        /// Create a Log Line about: Pause a save
        /// </summary>
        /// <param name="_nb">Index of the work</param>
        public static void SavePaused(int _nb)
        {
            CreateLogLine(_nb, "Index : " + _nb + ", save paused !");
        }

        /// <summary>
        /// Create a Log Line about: Resuming a paused save
        /// </summary>
        /// <param name="_nb">Index of the save</param>
        public static void SaveResumed(int _nb)
        {
            CreateLogLine(_nb, "Index : " + _nb + ", save successfully resumed !");
        }

        /// <summary>
        /// Create a Log Line about: Cancel a running save
        /// </summary>
        /// <param name="_nb">Index of the save</param>
        public static void SaveCancelled(int _nb)
        {
            CreateLogLine(_nb, "Index : " + _nb + ", save cancelled");
        }
    }
}

