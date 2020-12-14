using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Text.RegularExpressions;
using System.Globalization;
using EasySave_2._0.Properties;

namespace EasySave_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml Settings frame
    /// </summary>
    public partial class BaseWindow : Window
    {
       #region Methods                

        /// <summary>
        /// Only permits numbers to be entered in the text field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ConfirmSettings_Click(object sender, RoutedEventArgs e)
        {
            //To-do : implement settings saving
            GlobalSettings.Visibility = Visibility.Collapsed;
        }

        private void BackSettings_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
