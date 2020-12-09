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

namespace EasySave_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        ViewModel VM;

        #endregion

        #region Constructor
        /// <summary>
        /// View Constructor
        /// </summary>
        public MainWindow()
        {
            VM = new ViewModel();
            DataContext = VM;
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Fill in the form with Save object info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifySave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveList.SelectedItem != null)
            {
                DisableUIElements();
                ModelTest selectedItem = (ModelTest)SaveList.SelectedItem;
                SaveNameForm.Text = selectedItem.SaveName;
                SaveSourcePathForm.Text = selectedItem.SourcePath;
                SaveDestinationPathForm.Text = selectedItem.DestinationPath;

                if (selectedItem.SaveType == ModelTest.ModelTestType.complete)
                {
                    SaveTypeForm.SelectedIndex = 0;
                }
                else
                {
                    SaveTypeForm.SelectedIndex = 1;
                }

                // TODO : Create checkbox with available extensions

                EnableConfirmBackBtn();
            }
        }

        /// <summary>
        /// Creation of a new save / Visual duties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            DisableUIElements();
            ClearForm();
        }

        /// <summary>
        /// Confirm changes and save them to the Save object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            // Sauvegarde et envoie de l'objet Save modifié vers le Model
            EnableUIElements();
            ClearForm();
        }

        /// <summary>
        /// Cancel any changes and go back to inital state of the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            EnableUIElements();
        }

        /// <summary>
        /// Disable interaction with Save Objects.
        /// Permits Confirm or Back.
        /// </summary>
        private void DisableUIElements()
        {
            Create.IsEnabled = false;
            LaunchAllSave.IsEnabled = false;
            LaunchSave.IsEnabled = false;
            ModifySave.IsEnabled = false;
            DeleteSave.IsEnabled = false;
            SaveList.IsEnabled = false;
            EnableConfirmBackBtn();
        }

        /// <summary>
        /// Enable interaction with Save Objects.
        /// Deny Confirm or Back.
        /// </summary>
        private void EnableUIElements()
        {
            Create.IsEnabled = true;
            LaunchAllSave.IsEnabled = true;
            LaunchSave.IsEnabled = true;
            ModifySave.IsEnabled = true;
            DeleteSave.IsEnabled = true;
            SaveList.IsEnabled = true;
            DisableConfirmBackBtn();
        }

        /// <summary>
        /// Disable Confirm and Back button.
        /// </summary>
        private void DisableConfirmBackBtn()
        {
            Confirm.IsEnabled = false;
            Back.IsEnabled = false;
        }

        /// <summary>
        /// Enable Confirm and Back button.
        /// </summary>
        private void EnableConfirmBackBtn()
        {
            Confirm.IsEnabled = true;
            Back.IsEnabled = true;
        }

        /// <summary>
        /// Clear Form
        /// </summary>
        private void ClearForm()
        {
            SaveNameForm.Text = "";
            SaveSourcePathForm.Text = "";
            SaveDestinationPathForm.Text = "";
            SaveTypeForm.SelectedIndex = -1;
        }

        #endregion

    }
}
