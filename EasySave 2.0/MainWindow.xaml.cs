using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySave_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Model model = new Model();
        }

        /// <summary>
        /// On click, launch one or more save procedures.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launchSaveProcedure_Click(object sender, RoutedEventArgs e)
        {
            // faire en sorte de charger une nouvelle frame --> fichier create save par exemple
            MessageBox.Show($"Launch save procedure");
        }

        /// <summary>
        /// On click, create a save procedure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createSaveProcedure_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// On click, modify a save procedure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modifySaveProcedure_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// On click, delete a save procedure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteSaveProcedure_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
