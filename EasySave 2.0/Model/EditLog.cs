using Newtonsoft.Json;
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
        private static void CreateLogLine(string _content)
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

        }


        /// <summary>
        /// Create a Log Line about: the initialisation of the Software with the date.
        /// </summary>
        public static void InitSoftwareLogLine()
        {
            CreateLogLine("Initialisation of the Sofware at: " + DateTime.Now);
        }

        /// <summary>
        /// Create a Log Line about: the creation of a new save.
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        public static void CreateWorkLogLine(int _nb, List<ISaveWork> WorkList)
        {
            CreateLogLine("Creation of a new save work, name : " + WorkList[_nb - 1].Name + ", source path : " + WorkList[_nb - 1].SourcePath + ", destination path : " + WorkList[_nb - 1].DestinationPath + ", type : " + WorkList[_nb - 1].Type);
        }

        /// <summary>
        /// Create a Log Line about: the modification of a save.
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        public static void ChangeWorkLogLine(int _nb, List<ISaveWork> WorkList)
        {
            CreateLogLine("Modification of a existing save work in position " + _nb + ", current parameters : name : " + WorkList[_nb - 1].Name + ", source path : " + WorkList[_nb - 1].SourcePath + ", destination path : " + WorkList[_nb - 1].DestinationPath + ", type : " + WorkList[_nb - 1].Type);
        }

        /// <summary>
        /// Create a Log Line about: The supression of a save.
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        public static void DeleteWorkLogLine(int _nb)
        {
            CreateLogLine("Supression of save work in position" + _nb);
        }

        /// <summary>
        /// Create a Log Line about: The research of an existing savework
        /// </summary>
        /// <param name="saveWorkName">Name of the save</param>
        public static void SaveWorkAlreadyExistsLogLine(string saveWorkName)
        {
            CreateLogLine("The search savework named : " + saveWorkName + " already exist");
        }

        /// <summary>
        /// Create a Log Line about: The launch of a savework 
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        public static void LaunchingSaveLogLine(int _nb)
        {
            CreateLogLine("Launching of the savework " + _nb);
        }

        /// <summary>
        /// Create a Log Line about: The current save 
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        /// <param name="fi">FileInfo of the file</param>
        public static void SavingInfoLogLine(int _nb, List<ISaveWork> WorkList, FileInfo fi)
        {
            CreateLogLine("Saving " + fi.FullName + " in " + WorkList[_nb - 1].Progress.CurrentDestinationFilePath + ", size : " + fi.Length + " Bytes");
        }

        /// <summary>
        /// Create a Log Line about: The start of a saving action
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        public static void StartSaveLogLine(int _nb, List<ISaveWork> WorkList)
        {
            CreateLogLine("Launching save work from work : " + WorkList[_nb - 1].Name + ", type : " + WorkList[_nb - 1].Type);
        }

        /// <summary>
        /// Create a Log Line about: The end of a saving action
        /// </summary>
        /// <param name="_nb">ID of the save</param>
        /// <param name="WorkList">WorkList of the save</param>
        /// <param name="timeSpend">time spend between the start and the end of the save</param>
        public static void FinishSaveLogLine(int _nb, List<ISaveWork> WorkList, string timeSpend)
        {
            CreateLogLine(WorkList[_nb - 1].Name + " succesfully saved ! Time spend : " + timeSpend);
        }

        /// <summary>
        /// Create a Log Line about: The creation of a new directory
        /// </summary>
        /// <param name="filePath">DirectoryInfo of the file</param>
        public static void CreateDirectoryLogLine(DirectoryInfo filePath)
        {
            CreateLogLine("Creating target directory in : " + filePath.FullName);
        }

        /// <summary>
        /// Create a Log Line about: Files found to save
        /// </summary>
        /// <param name="nbFiles">Number of files found</param>
        /// <param name="sourceDirectory">DirectoryInfo of the source directory</param>
        /// <param name="directorySize">Total size of the directory</param>
        public static void FileToSaveFound(int nbFiles, DirectoryInfo sourceDirectory, long directorySize)
        {
            CreateLogLine(nbFiles + " files to save found from " + sourceDirectory.Name + ",Total size of the directory: " + directorySize + " Bytes");
        }

        /// <summary>
        /// Create a Log Line about: Start copy a file
        /// </summary>
        /// <param name="fi">FileInfo of the file</param>
        public static void StartCopyFileLogLine(FileInfo fi)
        {
            CreateLogLine("Start copy" + fi.Name);

        }

        /// <summary>
        /// Create a Log Line about: End copy a file
        /// </summary>
        /// <param name="fi">FileInfo of the file</param>
        /// <param name="timeSpend"></param>
        public static void FinishCopyFileLogLine(FileInfo fi)
        {
            CreateLogLine(fi.Name + " succesfully copy !");

        }

        /// <summary>
        /// Create a Log Line about: Entering in a subdirectory
        /// </summary>
        /// <param name="subDir">DirectoryInfo of the subdirectory</param>
        public static void EnterSubdirectoryLogLine(DirectoryInfo subDir)
        {
            CreateLogLine("Entering subdirectory : " + subDir.Name);
        }

        /// <summary>
        /// Create a Log Line about: Exiting a subdirectory
        /// </summary>
        /// <param name="subDir">DirectoryInfo of the subdirectory</param>
        public static void ExitSubdirectoryLogLine(DirectoryInfo subDir)
        {
            CreateLogLine("Exiting subdirectory : " + subDir.Name);
        }

        /// <summary>
        /// Create a Log Line about: The end of the save work program
        /// </summary>
        public static void EndSaveProgram()
        {
            CreateLogLine("Ending save work program");
        }
    }
}

