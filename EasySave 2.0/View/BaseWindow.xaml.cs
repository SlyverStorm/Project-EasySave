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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {

        #region Variables

        ViewModel VM;

        private List<UIElement> formElementList;
        /// <summary>
        /// List of Elements in the form.
        /// </summary>
        public List<UIElement> FormElementList { get => formElementList; set => formElementList = value; }

        private List<UIElement> optionButtonList;
        /// <summary>
        /// List of option Buttons (Launch, Modify...) + the SaveList ListView.
        /// </summary>
        public List<UIElement> OptionButtonList { get => optionButtonList; set => optionButtonList = value; }

        private List<UIElement> confirmButtonList;
        /// <summary>
        /// List of Buttons Confirm and Back.
        /// </summary>
        public List<UIElement> ConfirmButtonList { get => confirmButtonList; set => confirmButtonList = value; }

        private List<UIElement> selectionButtonList;
        /// <summary>
        /// List of Buttons Confirm and Back.
        /// </summary>
        public List<UIElement> SelectionButtonList { get => selectionButtonList; set => selectionButtonList = value; }

        private List<UIElement> checkBoxList;
        /// <summary>
        /// List of Buttons Confirm and Back.
        /// </summary>
        public List<UIElement> CheckBoxList { get => checkBoxList; set => checkBoxList = value; }

        private bool isAnItemSelected = false;
        /// <summary>
        /// Keeps track of the item selection status
        /// </summary>
        public bool IsAnItemSelected { get => isAnItemSelected; set => isAnItemSelected = value; }

        private bool firstTimeSelection = true;
        /// <summary>
        /// Prevents from the message box showing up on startup
        /// </summary>
        public bool FirstTimeSelection { get => firstTimeSelection; set => firstTimeSelection = value; }


        #endregion

        #region Constructor

        /// <summary>
        /// View Constructor
        /// </summary>
        public BaseWindow()
        {
            VM = new ViewModel();
            DataContext = VM;
            InitializeComponent();
            
            FormElementList = new List<UIElement>
            {
                SaveNameForm,
                SaveSourcePathForm,
                SaveDestinationPathForm,
                SaveTypeForm
            };

            OptionButtonList = new List<UIElement>
            {
                LaunchAllSave,
                Create,
                SaveList
            };

            ConfirmButtonList = new List<UIElement>
            {
                Confirm,
                Back
            };

            SelectionButtonList = new List<UIElement>
            {
                ModifySave,
                LaunchSave,
                DeleteSave
            };

            CheckBoxList = new List<UIElement>
            {
                Txt,
                Rar,
                Zip,
                Docx,
                Mp4,
                Pptx,
                Jpg,
                Png,
                Pdf,
                Exe,
                Iso
            };

            if (Settings.Default.languageCode == "en-US")
                LanguageSelection.SelectedIndex = 0;
            else
                LanguageSelection.SelectedIndex = 1;

        }

        #endregion

        #region i18n

        /// <summary>
        /// Informs the user of the language change on next startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstTimeSelection)
            {
                if (LanguageSelection.SelectedIndex == 0)
                {
                    Settings.Default.languageCode = "en-US";
                    MessageBox.Show("The change has been taken into account and will be effective on the next application startup.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    Settings.Default.languageCode = "fr-FR";
                    MessageBox.Show("Le changement a bien été pris en compte et sera effectif au prochain démarrage de l'application.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Properties.Settings.Default.Save();
            }
            else
            {
                firstTimeSelection = false;
            }
        }

        #endregion

    }
}
