using System;

namespace Projet_EasySave_v1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            View view = new View(model);
            Controller ctrl = new Controller(model, view);
            view.Start();                                       //temporary
            view.ShowMainMenu();                                //temporary
        }
    }
}
