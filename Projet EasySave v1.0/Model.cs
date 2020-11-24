using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class Model
    {

        public Model()
        {
            WorkList = new SaveWork[5];
            for (int i = 0; i < 5; i++)
            {
                WorkList[i] = new SaveWork("", "", "", SaveWorkType.unset);
            }
        }

        private SaveWork[] workList;

        public SaveWork[] WorkList
        {
            get { return workList; }
            set { workList = value; }
        }


        public void CreateWork(int nb, string name, string sourcePath, string destinationPath, SaveWorkType type)
        {
            SaveWork tempSave = new SaveWork(name, sourcePath, destinationPath, type);
            WorkList[nb - 1] = tempSave;
        }

        public void ChangeWork(int nb, string name, string sourcePath, string destinationPath, SaveWorkType type)
        {
            workList[nb - 1].Name = name;
            workList[nb - 1].SourcePath = sourcePath;
            workList[nb - 1].DestinationPath = destinationPath;
            workList[nb - 1].Type = type;
        }

        public void DeleteWork(int nb)
        {
            workList[nb - 1] = null;
        }

        public void DoSave(int nb)
        {
            CompleteSave(WorkList[nb - 1]);
        }

        private void CompleteSave(SaveWork saveWork)
        {
            CompleteCopy(saveWork.SourcePath, saveWork.DestinationPath);
        }

        private  static void CompleteCopy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CompleteCopyAll(diSource, diTarget);
        }

        private static void CompleteCopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CompleteCopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }





    }
}
