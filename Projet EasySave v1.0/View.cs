using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class View
    {

        public View(Model _model)
        {
            Model = _model;
        }

        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }


        public void Start()
        {
            Console.WriteLine("Welcome to EasySave !\nEasySave v.1.0\n\n");
            ShowMainMenu();
        }

        public string ShowMainMenu()
        {
            Console.WriteLine("Please select an option :\n" + //Mettre tout ça dans un fichier ressource
                "1. Select one or more save to launch.\n" +
                "2. Create a save procedure.\n" +
                "3. Modify a save procedure.\n" +
                "4. Delete a save procedure.\n" +
                "9. Close application.\n");

            switch (Console.ReadLine()) //Pour test en attendant le controller
            {
                case "1":
                    LaunchSaveMenu();
                    break;

                case "2":
                    CreateSaveMenu();
                    break;

                case "3":
                    ModifySaveMenu();
                    break;

                case "4":
                    DeleteSaveMenu();
                    break;

                default:
                    Back();
                    break;
            }

            return null;
            //return Console.ReadLine();
        }

        public List<int> LaunchSaveMenu()
        {
            return null;
        }
        
        public List<string> CreateSaveMenu()
        {
            List<string> choice = new List<string>();

            // Ask for name
            Console.WriteLine("Choose a name for your save procedure:");
            choice.Add(Console.ReadLine());

            // Ask for source path
            Console.WriteLine("\nChoose a source path to save :");
            choice.Add(Console.ReadLine());


            // Ask for destination path
            Console.WriteLine("\nChoose a destination path to export the save :");
            choice.Add(Console.ReadLine());


            // Ask for backup type
            Console.WriteLine("\nChoose a save type :\n" +
                "1. Complete save\n" +
                "2. Differential save");

            string saveTypeChoice = Console.ReadLine();
            
            while (saveTypeChoice != "1" && saveTypeChoice != "2")
            {
                Console.WriteLine("\nPlease enter a correct value :\n" +
                "1. Complete save\n" +
                "2. Differential save");
                saveTypeChoice = Console.ReadLine();
            }
            choice.Add(saveTypeChoice);

            return choice; // retour au menu depuis le controller
        }

        public string ModifySaveMenu()
        {
            return null;
        }

        public List<string> DeleteSaveMenu(List<SaveWork> _saveList)
        {
            List<string> choice = new List<string>();

            Console.WriteLine("\nSelect a save procedure to delete or return to main menu :\n");
            int i = 1;
            foreach (SaveWork saveWork in _saveList) // affichage des saves procedures into sélection des saves à delete
            {
                Console.WriteLine(i + ". " + saveWork.Name + "\n");
            }
            int valueEntered = int.TryParse(Console.ReadLine(), out null);

            if (valueEntered == 0)
            {
                //C'est de la merde recommence
            } 
            else if (valueEntered < _saveList.Count)
            {
                //On annule tout
            }

            
            choice.Add();
            Console.WriteLine("\n Select another save to delete or return to main menu :"); // choix pour quitter / retour : 9

            return choice;
        }

        private void Back()
        {

        }

        private void Confirm()
        {

        }
    }
}
