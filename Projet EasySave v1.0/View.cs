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
            ShowMainMenu(); // TEMPORAIRE // A enlever, role du controller
        }

        public string ShowMainMenu()
        {
            Console.WriteLine("\n\nPlease select an option :\n" +
                "1. Launch a save procedure.\n" +
                "2. Create a save procedure.\n" +
                "3. Modify a save procedure.\n" +
                "4. Delete a save procedure.\n" +
                "5. Launch all save procedures sequentially.\n" +
                "9. Close application.\n"); // TEMPORAIRE // Ajouter une confirmation

            return Console.ReadLine();
        }

        public string[] CreateSaveProcedure()
        {
            string[] choice = new string[4];
            
            for (int i = 0; i < 4; i++)
            {
                // Ask for name.
                Console.WriteLine("\nChoose a name for your save procedure:");
                string enteredName = Console.ReadLine();
                while (!Regex.IsMatch(enteredName, @"^[a-zA-Z0-9 _]+$"))  //Regex only allowing alphanumeric chars, spaces or underscores.
                {
                    Console.WriteLine("\nPlease only make use of alphanumeric characters, spaces or underscores.\n");
                    enteredName = Console.ReadLine();
                }
                choice[i] = enteredName;
                i++;

                // Ask for source path.
                Console.WriteLine("\nChoose a source path to save :");
                string enteredSource = Console.ReadLine();
                while (!Regex.IsMatch(enteredSource, @"^[a-zA-Z]:(?:[\\\/]+[a-zA-Z0-9 _]+)*[\\\/]*$"))  //Regex for valid windows folder path.
                {
                    Console.WriteLine("\nPlease enter a valid absolute path.\n");
                    enteredSource = Console.ReadLine();
                }
                choice[i] = enteredSource;
                i++;

                // Ask for destination path.
                string enteredDestination = Console.ReadLine();
                while (!Regex.IsMatch(enteredDestination, @"^[a-zA-Z]:(?:[\\\/]+[a-zA-Z0-9 _]+)*[\\\/]*$"))  //Regex for valid windows folder path.
                {
                    Console.WriteLine("\nPlease enter a valid absolute path.\n");
                    enteredDestination = Console.ReadLine();
                }
                Console.WriteLine("\nChoose a destination path to export the save :");
                choice[i] = enteredDestination;
                i++;


                // Ask for backup type.
                Console.WriteLine("\nChoose a save type :\n" +
                    "1. Complete save\n" +
                    "2. Differential save");

                string saveTypeChoice = Console.ReadLine();

                // Check if input is correct.
                while (saveTypeChoice != "1" && saveTypeChoice != "2")
                {
                    Console.WriteLine("\nPlease enter a correct value to proceed.");
                    saveTypeChoice = Console.ReadLine();
                }
                choice[i] = saveTypeChoice;
            }
            Console.WriteLine("\nSuccess ! Save procedure has been created.");
            return choice; // TEMPORAIRE //  retour au menu depuis le controller
        }

        public SaveWork ModifySaveProcedure(SaveWork _save)
        {
            SaveWork modifiedSave = _save;
            string choice = "";

            while (choice != "5" && choice != "9") //While loop to allow the user to modify multiple values.
            {
                Console.WriteLine("\n\nPlease select a parameter to modify :\n" +
                    "1. Name : " + modifiedSave.Name +
                    "\n2. Source Path : " + modifiedSave.SourcePath +
                    "\n3. Destination Path : " + modifiedSave.DestinationPath +
                    "\n4. Save Type : " + modifiedSave.Type +
                    "\n5. Confirm" +
                    "\n9. Cancel.\n");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nPlease enter a new name\n");
                        string enteredName = Console.ReadLine();
                        while (!Regex.IsMatch(enteredName, @"^[a-zA-Z0-9 _]+$"))  //Regex only allowing alphanumeric chars, spaces or underscores.
                        {
                            Console.WriteLine("\nPlease only make use of alphanumeric characters, spaces or underscores.\n");
                            enteredName = Console.ReadLine();
                        }
                        modifiedSave.Name = enteredName;
                        break;

                    case "2":
                        Console.WriteLine("Please enter a new source path to save (absolute) :\n");
                        string enteredSource = Console.ReadLine();
                        while (!Regex.IsMatch(enteredSource, @"^[a-zA-Z]:(?:[\\\/]+[a-zA-Z0-9 _]+)*[\\\/]*$"))  //Regex for valid windows folder path.
                        {
                            Console.WriteLine("\nPlease enter a valid absolute path.\n");
                            enteredSource = Console.ReadLine();
                        }
                        modifiedSave.SourcePath = enteredSource;
                        break;

                    case "3":
                        Console.WriteLine("Please enter a new destination path to export the save (absolute) :\n");
                        string enteredDestination = Console.ReadLine();
                        while (!Regex.IsMatch(enteredDestination, @"^[a-zA-Z]:(?:[\\\/]+[a-zA-Z0-9 _]+)*[\\\/]*$"))  //Regex for valid windows folder path.
                        {
                            Console.WriteLine("\nPlease enter a valid absolute path.\n");
                            enteredDestination = Console.ReadLine();
                        }
                        modifiedSave.SourcePath = enteredDestination;
                        break;

                    case "4":
                        Console.WriteLine("Please choose a new save type :\n" +
                            "1. Complete\n" +
                            "2. Differencial\n");

                        string enteredValue = Console.ReadLine();

                        while (enteredValue != "1" && enteredValue != "2")  //Check for valid value entered by the user (1 or 2).
                        {
                            Console.WriteLine("\nPlease enter a correct value to proceed.\n");
                            enteredValue = Console.ReadLine();
                        }

                        modifiedSave.Type = enteredValue == "1" ? SaveWorkType.complete : SaveWorkType.differencial;
                        break;

                    case "5":
                        break;

                    case "9":
                        break;
                    default:
                        Console.WriteLine("\nPlease enter a correct value to proceed.\n");
                        break;
                }

            }
            return choice == "5" ? modifiedSave : null;
        }

        //Shows a different message depending on selection.
        public void TerminalMessage(string _type)
        {
            switch (_type)
            {
                case "Launch":
                    Console.WriteLine("\nSelect a save procedure to launch or return to the main menu :\n");
                    break;
                case "Modify":
                    Console.WriteLine("\nSelect a save procedure to modify or return to the main menu :\n");
                    break;
                case "Delete":
                    Console.WriteLine("\nSelect a save procedure to delete or return to the main menu :\n");
                    break;
                default:
                    break;
            }
        }

        //Shows the menu from which you can select a save procedure to delete. It receives all the procedures as a parameter.
        public int SelectSaveProcedure(SaveWork[] _saveList)
        {
            if (_saveList == null)
            {
                Console.WriteLine("\nNo save procedures created yet.");
                return 9;
            }

            int increment = 0; //Helps to count foreach interations and show index in terminal.
            string regexNumbers = "9"; //Later, we'll check if the value entered by the user is in this regex string, meaning it corresponds to a save procedure or the cancel option. Can be considered as a char list.


            //Write the name of every save procedure in the terminal as a list and add the procedure index in the string regexNumbers.
            foreach (SaveWork saveWork in _saveList)
            {
                increment++;
                regexNumbers += increment;
                Console.WriteLine(increment + ". " + saveWork.Name + "\n");
            }
            Console.WriteLine("9. Cancel\n");


            string enteredValue = Console.ReadLine(); //The user has to choose from the list above.


            //If the value entered by the user doesn't correspond to a single index of a save procedure, will ask to enter a correct value.
            while (!Regex.IsMatch(enteredValue, @"^[" + regexNumbers + "]$"))
            {
                Console.WriteLine("\nPlease enter a correct value to proceed.\n");
                enteredValue = Console.ReadLine();
            }

            //Will return the index of the save procedure or 9 if "9" is the value entered.
            return enteredValue != "9" ? int.Parse(enteredValue) - 1 : 9;
        }

        // Confirms critical user interaction (Save procedure suppression, modifications, exiting app...)
        public bool Confirm()
        {
            Console.WriteLine("Are you sure you want to do this ? y/n");

            string choice = Console.ReadLine();

            while (choice != "y" && choice != "n")
            {
                Console.WriteLine("\nPlease enter a correct value to proceed.\n");
                choice = Console.ReadLine();
            }

            if (choice == "y")
            {
                return true;
            }
            else
            {
                return false; // TEMPORAIRE // controller fait revenir au menu principal
            }
        }
    }
}
