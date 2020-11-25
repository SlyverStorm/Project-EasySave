using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class Controller
    {

        public Controller(Model model, View view)
        {

            Model = model;
            View = view;

        }

        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        private View view;

        public View View
        {
            get { return view; }
            set { view = value; }
        }

        public void Start()
        {
            View.Start();
            ShowMenu();
        }

        private void LaunchSave()
        {
            View.TerminalMessage("Launch");
            int saveProcedureIndex = View.SelectSaveProcedure(/*To Implement*/);
            if (View.Confirm())
            {
                //To Implement (sauvegarde en cours blablabla)
            }
            else
            {
                ShowMenu();
                return;
            }
        }

        private void CreateSave()
        {
            string[] saveProcedure = View.CreateSaveProcedure();

            //To Implement

            ShowMenu();
            return;
        }

        private void ModifySave()
        {
            View.TerminalMessage("Modify");
            SaveWork saveProcedure = View.ModifySaveProcedure(/*To Implement*/);
            if (View.Confirm())
            {
                //To Implement
            }
            else
            {
                ShowMenu();
                return;
            }

        }

        private void DeleteSave()
        {
            View.TerminalMessage("Delete");
            int saveProcedureIndex = View.SelectSaveProcedure(/*To Implement*/);
            if (View.Confirm())
            {
                //To Implement
            }
            else
            {
                ShowMenu();
                return;
            }

        }

        private void LaunchAllSavesSequentially()
        {
            if (View.Confirm())
            {
                //To Implement (sauvegarde en cours blablabla)
            }
            else
            {
                ShowMenu();
                return;
            }
        }

        private void ShowMenu()
        {
            switch (View.ShowMainMenu())
            {
                case 1:
                    LaunchSave();
                    break;
                case 2:
                    CreateSave();
                    break;
                case 3:
                    ModifySave();
                    break;
                case 4:
                    DeleteSave();
                    break;
                case 5:
                    LaunchAllSavesSequentially();
                    break;
                default:
                    break;
            }
            
        }
    }
}
