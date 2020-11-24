using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_EasySave_v1._0
{
    class View
    {

        public View(Model model)
        {
            Model = model;
        }

        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public void write(string x)
        {
            Console.WriteLine(x);
        }

        public string read()
        {
            return Console.ReadLine();
        }


    }
}
