using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            Console.WriteLine("Welcome to EasySave !\nEasySave v.1.0");
            ShowMainMenu();
        }

        public string ShowMainMenu()
        {
            Console.WriteLine("\n\nPlease select an option :\n" + //Mettre tout ça dans un fichier ressource
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
                    DeleteSaveMenu(null);
                    break;

                default:
                    break;
            }

            return null;
        }

        public int[] LaunchSaveMenu()
        {
            return null;
        }
        
        public string[] CreateSaveMenu()
        {
            string[] choice = new string[4];

            for (int i = 0; i < 4; i++)
            {
                // Ask for name
                Console.WriteLine("\nChoose a name for your save procedure:");
                choice[i] = Console.ReadLine();
                i++;

                // Ask for source path
                Console.WriteLine("\nChoose a source path to save :");
                choice[i] = Console.ReadLine();
                i++;

                // Ask for destination path
                Console.WriteLine("\nChoose a destination path to export the save :");
                choice[i] = Console.ReadLine();
                i++;


                // Ask for backup type
                Console.WriteLine("\nChoose a save type :\n" +
                    "1. Complete save\n" +
                    "2. Differential save");

                string saveTypeChoice = Console.ReadLine();

                while (saveTypeChoice != "1" && saveTypeChoice != "2")
                {
                    Console.WriteLine("\nPlease enter a correct value to proceed.");
                    saveTypeChoice = Console.ReadLine();
                }
                choice[i] = saveTypeChoice;
                i++;
            }
            Console.WriteLine("Success ! Save procedure has been created.");
            return choice; // retour au menu depuis le controller
        }

        public string ModifySaveMenu()
        {
            return null;
        }


        //Shows the menu from which you can select a save procedure to delete. It receives all the procedures as a parameter.
        public SaveWork DeleteSaveMenu(SaveWork[] _saveList)
        {
            if (_saveList == null) 
            { 
                Console.WriteLine("\no save procedures created yet.");
                return null;
            }

            Console.WriteLine("\nSelect a save procedure to delete or return to main menu :\n");

            //"increment" variable helps to count foreach interations and 
            int increment = 0;
            string regexNumbers = "9";


            foreach(SaveWork saveWork in _saveList)  // affichage des saves procedures into sélection des saves à delete
            {
                increment++;
                regexNumbers += increment;
                Console.WriteLine(increment + ". " + saveWork.Name + "\n");
            }
            Console.WriteLine("9. Cancel\n");


            string enteredValue = Console.ReadLine();

            while (!Regex.IsMatch(enteredValue, @"^\d{" + regexNumbers + "}$"))
            {
                Console.WriteLine("\nPlease enter a correct value to proceed.\n");
                enteredValue = Console.ReadLine();
            }

            if (enteredValue == "9")
            {
                return null;
            }
            else
            {
                return _saveList[int.Parse(enteredValue) - 1];
            }
        }


        public bool Confirm()
        {
            Console.WriteLine("Are you sure you want to do this ? y/n");

            string choice = Console.ReadLine();

            while (choice != "y" && choice != "n")
            {
                Console.WriteLine("\nPlease enter a correct value to proceed.\n");
                choice = Console.ReadLine();
            }

            if(choice == "y")
            {
                return true;
            } else {
                return false; // controller fait revenir au menu principal
            }
        }
    }
}