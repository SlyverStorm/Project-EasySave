using System;

namespace Projet_EasySave_v1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model(1);
            View view = new View(2, model);
            Controller ctrl = new Controller(model, view);
        }
    }
}
