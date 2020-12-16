using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Projet_EasySave_v1._0
{
    class View
    {
        private bool isEnglish = true; // false -> français

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

        //Ask user to select language 
        public void SelectLanguage()
        {
            Console.WriteLine("\n\nPlease select your language : / Veuillez choisir une langue :\n" +
                "1. English\n" +
                "2. Français\n");

            string selectedLanguage = Console.ReadLine();
            while (!Regex.IsMatch(selectedLanguage, @"^[12]$"))  //Regex only allowing 1 or 2.
            {
                Console.WriteLine("\nPlease only choose 1 or 2 :\n");
                selectedLanguage = Console.ReadLine();
            }

            if (selectedLanguage == "1")
            {
                isEnglish = true;
            }
            else
            {
                isEnglish = false;
            }
        }

        //Returns the user choice as a string to the controller.
        public string ShowMainMenu()
        {
            if (isEnglish)
            {
                Console.WriteLine("\n\nPlease select an option :\n" +
                "1. Launch a save procedure.\n" +
                "2. Create a save procedure.\n" +
                "3. Modify a save procedure.\n" +
                "4. Delete a save procedure.\n" +
                "5. Launch all save procedures sequentially.\n" +
                "9. Close application.\n");
            }
            else
            {
                Console.WriteLine("\n\nVeuillez choisir une option :\n" +
                "1. Lancer une procédure de sauvegarde.\n" +
                "2. Créer  une procédure de sauvegarde.\n" +
                "3. Modifier une procédure de sauvegarde.\n" +
                "4. Supprimer une procédure de sauvegarde.\n" +
                "5. Lancer toutes les procédures de sauvegarde.\n" +
                "9. Fermer l'application.\n");
            }
            

            return Console.ReadLine();
        }

        //Allows the user to create a new save procedure by giving it's name, source path, destination path and type.
        public string[] CreateSaveProcedure()
        {
            string[] choice = new string[5];
            
            for (int i = 0; i < 4; i++)
            {
                // Ask for name.
                if (isEnglish)
                    Console.WriteLine("\nChoose a name for your save procedure :");
                else
                    Console.WriteLine("\nChoisissez un nom pour votre procédure de sauvegarde :");

                string enteredName = Console.ReadLine();
                while (!Regex.IsMatch(enteredName, @"^[a-zA-Z0-9 _]+$"))  //Regex only allowing alphanumeric chars, spaces or underscores.
                {
                    if (isEnglish)
                       Console.WriteLine("\nPlease only make use of alphanumeric characters, spaces or underscores.\n");
                    else
                       Console.WriteLine("\nVeuillez ne choisir que des caractères alphanumériques, des espaces ou des underscores.\n");

                    enteredName = Console.ReadLine();
                }
                choice[i] = enteredName;
                i++;

                // Ask for source path.
                if (isEnglish)
                    Console.WriteLine("\nChoose a source path to save :");
                else
                    Console.WriteLine("\nChoisissez un chemin source pour votre sauvegarde :");

                string enteredSource = Console.ReadLine();
                while (!Regex.IsMatch(enteredSource, @"^[a-zA-Z]:(?:\/[a-zA-Z0-9 _]+)*$"))  //Regex for valid windows folder path.
                {
                    if (isEnglish)
                        Console.WriteLine("\nPlease enter a valid absolute path.\n");
                    else
                        Console.WriteLine("\nVeuillez rentrer un chemin absolu valide.\n");

                    enteredSource = Console.ReadLine();
                }
                choice[i] = enteredSource;
                i++;

                // Ask for destination path.
                if (isEnglish)
                    Console.WriteLine("\nChoose a destination path to export the save :");
                else
                    Console.WriteLine("\nChoisissez un chemin de destination pour votre sauvegarde :");

                string enteredDestination = Console.ReadLine();
                while (!Regex.IsMatch(enteredDestination, @"^[a-zA-Z]:(?:\/[a-zA-Z0-9 _]+)*$"))  //Regex for valid windows folder path.
                {
                    if (isEnglish)
                        Console.WriteLine("\nPlease enter a valid absolute path.\n");
                    else
                        Console.WriteLine("\nVeuillez rentrer un chemin absolu valide.\n");

                    enteredDestination = Console.ReadLine();
                }
                choice[i] = enteredDestination;
                i++;


                // Ask for backup type.
                if (isEnglish)
                    Console.WriteLine("\nChoose a save type :\n" +
                        "1. Complete save\n" +
                        "2. Differential save");
                else
                    Console.WriteLine("\nChoisissez un type de sauvegarde :\n" +
                        "1. Sauvegarde complète\n" +
                        "2. Sauvegarde différentielle");

                string saveTypeChoice = Console.ReadLine();

                // Check if input is correct.
                while (saveTypeChoice != "1" && saveTypeChoice != "2")
                {
                    if (isEnglish)
                        Console.WriteLine("\nPlease enter a correct value to proceed.");
                    else
                        Console.WriteLine("\nVeuillez rentrer une valeur correcte pour continuer.");

                    saveTypeChoice = Console.ReadLine();
                }
                choice[i] = saveTypeChoice;
                i++;


                //Ask for save location in array.
                if (isEnglish)
                    Console.WriteLine("\nChoose the save procedure position number. (from 1 to 5)\n");
                else
                    Console.WriteLine("\nChoisissez le numéro de la procédure de sauvegarde. (de 1 à 5)\n");

                string savePos = Console.ReadLine();

                // Check if input is correct.
                while (!Regex.IsMatch(savePos, @"^[12345]$"))
                {
                    if (isEnglish)
                        Console.WriteLine("\nPlease enter a correct value to proceed.");
                    else
                        Console.WriteLine("\nVeuillez rentrer une valeur correcte pour continuer.");
                    savePos = Console.ReadLine();
                }
                choice[i] = savePos;
            }
            return choice;
        }

        //Allows the user to modifiy an existing save procedure name, source path, destination path and/or type.
        public SaveWork ModifySaveProcedure(SaveWork _save)
        {
            if (_save.Type != SaveWorkType.unset)
            {
                SaveWork modifiedSave = _save;
                string choice = "";

                while (choice != "5" && choice != "9") //While loop to allow the user to modify multiple values.
                {
                    if (isEnglish)
                        Console.WriteLine("\n\nPlease select a parameter to modify :\n" +
                            "1. Name : " + modifiedSave.Name +
                            "\n2. Source Path : " + modifiedSave.SourcePath +
                            "\n3. Destination Path : " + modifiedSave.DestinationPath +
                            "\n4. Save Type : " + modifiedSave.Type +
                            "\n5. Confirm" +
                            "\n9. Cancel.\n");
                    else
                        Console.WriteLine("\n\nVeuillez choisir un paramètre à modifier :\n" +
                            "1. Nom : " + modifiedSave.Name +
                            "\n2. Chemin Source : " + modifiedSave.SourcePath +
                            "\n3. Chemin de Destination: " + modifiedSave.DestinationPath +
                            "\n4. Type de Sauvegarde: " + modifiedSave.Type +
                            "\n5. Valider" +
                            "\n9. Annuler.\n");

                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            if (isEnglish)
                                Console.WriteLine("\nPlease enter a new name\n");
                            else
                                Console.WriteLine("\nVeuillez rentrer un nouveau nom");
                            string enteredName = Console.ReadLine();
                            while (!Regex.IsMatch(enteredName, @"^[a-zA-Z0-9 _]+$"))  //Regex only allowing alphanumeric chars, spaces or underscores.
                            {
                                if (isEnglish)
                                    Console.WriteLine("\nPlease only make use of alphanumeric characters, spaces or underscores.\n");
                                else
                                    Console.WriteLine("\nVeuillez ne choisir que des caractères alphanumériques, des espaces ou des underscores.\n");
                                enteredName = Console.ReadLine();
                            }
                            modifiedSave.Name = enteredName;
                            break;

                        case "2":
                            if (isEnglish)
                                Console.WriteLine("Please enter a new source path to save (absolute) :\n");
                            else
                                Console.WriteLine("Veuillez rentrer un nouveau chemin source absolu à sauvegarder :\n");

                            string enteredSource = Console.ReadLine();
                            while (!Regex.IsMatch(enteredSource, @"^[a-zA-Z]:(?:\/[a-zA-Z0-9 _]+)*$"))  //Regex for valid windows folder path.
                            {
                                if (isEnglish)
                                    Console.WriteLine("\nPlease enter a valid absolute path.\n");
                                else
                                    Console.WriteLine("\nVeuillez rentrer un chemin absolu valide.\n");
                                enteredSource = Console.ReadLine();
                            }
                            modifiedSave.SourcePath = enteredSource;
                            break;

                        case "3":
                            if (isEnglish)
                                Console.WriteLine("Please enter a new destination path to export the save (absolute) :\n");
                            else
                                Console.WriteLine("Veuillez rentrer un nouveau chemin de destination absolu où sauvegarder :\n");

                            string enteredDestination = Console.ReadLine();
                            while (!Regex.IsMatch(enteredDestination, @"^[a-zA-Z]:(?:\/[a-zA-Z0-9 _]+)*$"))  //Regex for valid windows folder path.
                            {
                                if (isEnglish)
                                    Console.WriteLine("\nPlease enter a valid absolute path.\n");
                                else
                                    Console.WriteLine("\nVeuillez rentrer un chemin absolu valide.\n");
                                enteredDestination = Console.ReadLine();
                            }
                            modifiedSave.SourcePath = enteredDestination;
                            break;

                        case "4":
                            if (isEnglish)
                                Console.WriteLine("Please choose a save type :\n" +
                                    "1. Complete\n" +
                                    "2. Differencial\n");
                            else
                                Console.WriteLine("Veuillez choisir un type de sauvegarde :\n" +
                                    "1. Complète\n" +
                                    "2. Différentielle\n");

                            string enteredValue = Console.ReadLine();

                            //Check for valid value entered by the user (1 or 2).
                            while (enteredValue != "1" && enteredValue != "2")
                            {
                                if (isEnglish)
                                    Console.WriteLine("\nPlease enter a correct value to proceed.");
                                else
                                    Console.WriteLine("\nVeuillez rentrer une valeur correcte pour continuer.");
                                enteredValue = Console.ReadLine();
                            }

                            modifiedSave.Type = enteredValue == "1" ? SaveWorkType.complete : SaveWorkType.differencial;
                            break;

                        case "5":
                            break;

                        case "9":
                            break;
                        default:
                            if (isEnglish)
                                Console.WriteLine("\nPlease enter a correct value to proceed.");
                            else
                                Console.WriteLine("\nVeuillez rentrer une valeur correcte pour continuer.");
                            break;
                    }

                }
                return choice == "5" ? modifiedSave : null;
            }
            else
            {
                if (isEnglish)
                    Console.WriteLine("The Specified save work is currently empty, cannot modify it");
                else
                    Console.WriteLine("La procédure de sauvegarde actuellement selectionnée est vide, elle ne peut être modifiée.");

                return null;
            }
        }
        
        //Shows the menu from which you can select a save procedure to delete. It receives all the procedures as a parameter.
        public int SelectSaveProcedure(SaveWork[] _saveList)
        {
            if (_saveList == null)
            {
                if (isEnglish)
                    Console.WriteLine("\nNo save procedures created yet.");
                else
                    Console.WriteLine("\nAucune procédure de sauvegarde n'a été créée.");

                return 9;
            }

            int increment = 0;
            string regexNumbers = "9"; //Later, we'll check if the value entered by the user is in this regex string, meaning it corresponds to a save procedure or the cancel option. Can be considered as an int list.

            
            //Write the name of every save procedure in the terminal as a list and add the procedure index in the string regexNumbers.
            foreach (SaveWork saveWork in _saveList)
            {
                increment++;
                if (saveWork.Type !=SaveWorkType.unset)
                {
                    regexNumbers += increment;
                    Console.WriteLine(increment + ". " + saveWork.Name + "\n");
                }
            }
            if (isEnglish)
                Console.WriteLine("9. Cancel\n");
            else
                Console.WriteLine("9. Annuler\n");


            string enteredValue = Console.ReadLine();


            //Check for valid value entered by the user.
            while (!Regex.IsMatch(enteredValue, @"^[" + regexNumbers + "]$"))
            {
                if (isEnglish)
                    Console.WriteLine("\nPlease enter a correct value to proceed.");
                else
                    Console.WriteLine("\nVeuillez rentrer une valeur correcte pour continuer.");
                enteredValue = Console.ReadLine();
            }

            //Will return the index of the save procedure or 9 if "9" is the value entered.
            return enteredValue != "9" ? int.Parse(enteredValue) : 9;
        }

        //The user has to confirm critical interactions.
        public bool Confirm()
        {
            if (isEnglish)
                Console.WriteLine("\nAre you sure you want to do this ? y/n");
            else
                Console.WriteLine("\nÊtes-vous sûr de vouloir faire ceci ? o/n");

            string choice = Console.ReadLine();

            while (choice != "y" && choice != "n")
            {
                if (isEnglish)
                    Console.WriteLine("\nPlease enter a correct value to proceed.");
                else
                    Console.WriteLine("\nVeuillez rentrer une valeur correcte pour continuer.");
                choice = Console.ReadLine();
            }

            if (choice == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /* Simple Void Console Write methods */


        public void Start()
        {
            Console.WriteLine("Welcome to EasySave ! / Bienvenue sur EasySave !\nEasySave v.1.1");
            SelectLanguage();
        }

        //Shows a different message depending on save type.
        public void SaveInProgressMessage(SaveWork _save)
        {
            if (_save.Type == SaveWorkType.differencial)
            {
                if (isEnglish)
                    Console.WriteLine("\nDifferential save " + _save.Name + " in progress...");
                else
                    Console.WriteLine("\nSauvegarde différentielle " + _save.Name + " en cours...");

            }
            else if (_save.Type == SaveWorkType.complete)
            {
                if (isEnglish)
                    Console.WriteLine("\nComplete save " + _save.Name + " in progress...");
                else
                    Console.WriteLine("\nSauvegarde complète" + _save.Name + " en cours...");
            }
        }

        public void OperationDoneMessage()
        {
            if (isEnglish)
                Console.WriteLine("\nDone.");
            else
                Console.WriteLine("\n Terminé.");
        }

        public void SaveIsDoneMessage(SaveWork _save) //Done method is different for the launch option as we don't want to show unset save procedures.
        {
            if (_save.Type != SaveWorkType.unset)
            {
                if (isEnglish)
                    Console.WriteLine("\nDone.");
                else
                    Console.WriteLine("\n Terminé.");
            }
        }

        //Shows a different message depending on selection.
        public void TerminalMessage(string _type)
        {
            if (isEnglish)
                Console.WriteLine("\nSelect a save procedure to " + _type + " or return to the main menu :\n");
            else
                Console.WriteLine("\nSélectionner une procédure de sauvegarde " + _type + " ou retourner au menu principal :\n");
        }

    }
}
