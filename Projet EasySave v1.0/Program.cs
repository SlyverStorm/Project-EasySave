using System;

namespace Projet_EasySave_v1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            //View view = new View(model);
            //Controller ctrl = new Controller(model, view);

            string sourcePath = "D:/save/source";
            string destinationPath = "D:/save/destination";

            model.CreateWork(1, "saveTest",sourcePath, destinationPath, SaveWorkType.differencial);
           /* Console.WriteLine(model.WorkList[0].Name);
            Console.WriteLine(model.WorkList[0].SourcePath);
            Console.WriteLine(model.WorkList[0].DestinationPath);
            Console.WriteLine(model.WorkList[0].Type);
           
            model.DoSave(1);
           */
            model.createLogLine(); 
        }
    }
}
