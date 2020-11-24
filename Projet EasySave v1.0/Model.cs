using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class Model
    {
        
        //Model Class constructor, initiate Save Work in an array
        //TODO: get save works value from the save work state json file
        public Model()
        {
            WorkList = new SaveWork[5];
            for (int i = 0; i < 5; i++)
            {
                WorkList[i] = new SaveWork("", "", "", SaveWorkType.unset);
            }
        }

        //Store all 5 (max) save works
        private SaveWork[] workList;

        public SaveWork[] WorkList
        {
            get { return workList; }
            set { workList = value; }
        }

        //Can create a save work from simple parameters
        public void CreateWork(int _nb, string _name, string _sourcePath, string _destinationPath, SaveWorkType _type)
        {
            SaveWork tempSave = new SaveWork(_name, _sourcePath, _destinationPath, _type);
            WorkList[_nb - 1] = tempSave;
        }

        //Modify value of save works objects stored in workList, if there is any null parameters the value attached isn't changed
        public void ChangeWork(int _nb, string _name, string _sourcePath, string _destinationPath, SaveWorkType _type)
        {
            workList[_nb - 1].Name = _name;
            workList[_nb - 1].SourcePath = _sourcePath;
            workList[_nb - 1].DestinationPath = _destinationPath;
            workList[_nb - 1].Type = _type;
        }

        //Can delete a save work (set to null)
        public void DeleteWork(int _nb)
        {
            workList[_nb - 1] = null;
        }

        //Can initiate a type of save from the numbers of the save work in workList.
        public void DoSave(int _nb)
        {
            CompleteSave(WorkList[_nb - 1]);
        }

        //Launch a complete save from a SaveWork type parameter
        private void CompleteSave(SaveWork _saveWork)
        {
            CompleteCopy(_saveWork.SourcePath, _saveWork.DestinationPath);
        }

        //Do a complete copy from a folder to another
        private  static void CompleteCopy(string _sourceDirectory, string _targetDirectory)
        {
            var diSource = new DirectoryInfo(_sourceDirectory);
            var diTarget = new DirectoryInfo(_targetDirectory);

            CompleteCopyAll(diSource, diTarget);
        }

        //Copy each file from a directory, and do the same for each subdirectory using recursion
        private static void CompleteCopyAll(DirectoryInfo _source, DirectoryInfo _target)
        {
            Directory.CreateDirectory(_target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in _source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", _target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(_target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in _source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    _target.CreateSubdirectory(diSourceSubDir.Name);
                CompleteCopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        //Launch a diffrencial save from a SaveWork parameter
        private void DifferencialSave(SaveWork _saveWork)
        {
            DifferencialCopy(_saveWork.SourcePath, _saveWork.DestinationPath);
        }

        //Do a différential copy from a folder to another
        private void DifferencialCopy(string _sourceDirectory, string _targetDirectory)
        {

        }

        //Copy each files (that has been modified since the last save) from a directory, and do the same for each subdirectory using recursion
        private void DifferencialCopyAll(DirectoryInfo _source, DirectoryInfo _target)
        {

        }





        public void createLogLine()
        {
            // The line to record in the log
            string jsonLogLine;
            // Save parameters
            SaveWork save = new SaveWork("Test2", "file1", "File2", SaveWorkType.complete);

            // Create the log line in a Json format
            jsonLogLine = JsonConvert.SerializeObject(save, Formatting.None);
            jsonLogLine = DateTime.Now + " >>> " + jsonLogLine; 
            Console.WriteLine(jsonLogLine);

            // call a fonction to write the line in a file
            addLogLine(jsonLogLine);
        }


        public void addLogLine(string jsonLogLine)
        {
            // the log file is "data.json"       
            using (StreamWriter logWriter = File.AppendText("log.json"))
            {
                // Write the log line
                logWriter.WriteLine(jsonLogLine);
                Console.WriteLine("Record log OK");
            }
            //}
        }

    }
}
