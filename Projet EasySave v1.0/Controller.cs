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

        public void start()
        {
            string a = view.read();
            view.write("ALLO :"+a);
        }




    }
}
