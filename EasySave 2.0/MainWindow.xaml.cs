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

namespace EasySave_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        }

        #endregion

        #region Methods

        #region Option Buttons

        /// <summary>
        /// Shows the settings menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Launch the selected save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaunchSave_Click(object sender, RoutedEventArgs e)
        {
            ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;
            VM.LaunchSaveProcedure(selectedItem.Index);
        }

        /// <summary>
        /// Deletion of the selected save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSave_Click(object sender, RoutedEventArgs e)
        {
            ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;
            VM.DeleteSaveProcedure(selectedItem.Index);

            ChangeUIElementEnableState(SelectionButtonList, false);

            SaveList.Items.Refresh();
            IsAnItemSelected = false;
        }

        /// <summary>
        /// Fill in the form with Save object info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifySave_Click(object sender, RoutedEventArgs e)
        {
            ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;
            SaveNameForm.Text = selectedItem.Name;
            SaveSourcePathForm.Text = selectedItem.SourcePath;
            SaveDestinationPathForm.Text = selectedItem.DestinationPath;

            if (selectedItem.Type == SaveWorkType.complete)
            {
                SaveTypeForm.SelectedIndex = 0;
            }
            else
            {
                SaveTypeForm.SelectedIndex = 1;
            }

            if (selectedItem.ExtentionToEncryptList != null)
            {
                foreach (Extension _extension in selectedItem.ExtentionToEncryptList)
                {
                    CheckBox _checkBox = FindName(_extension.ToString().First().ToString().ToUpper() + _extension.ToString().Substring(1)) as CheckBox;
                    _checkBox.IsChecked = true;
                }
            }

            ChangeUIElementEnableState(FormElementList, true);
            ChangeUIElementEnableState(OptionButtonList, false);
            ChangeUIElementEnableState(SelectionButtonList, false);
            ChangeUIElementEnableState(CheckBoxList, true);
            ALL.IsEnabled = true;
            ChangeUIElementVisibilityState(ConfirmButtonList, Visibility.Visible);

            Confirm.Click -= ConfirmModifyClick;
            Confirm.Click -= ConfirmCreateClick;
            Confirm.Click += ConfirmModifyClick;
            
        }

        /// <summary>
        /// Creation of a new save / Visual duties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ChangeUIElementEnableState(FormElementList, true);
            ChangeUIElementEnableState(OptionButtonList, false);
            ChangeUIElementEnableState(SelectionButtonList, false);
            ChangeUIElementEnableState(CheckBoxList, true);
            ALL.IsEnabled = true;
            ChangeUIElementVisibilityState(ConfirmButtonList, Visibility.Visible);
            ClearForm();
            Confirm.Click -= ConfirmModifyClick;
            Confirm.Click -= ConfirmCreateClick;
            Confirm.Click += ConfirmCreateClick;
        }

        #endregion

        #region Confirm Buttons

        /// <summary>
        /// Confirm changes and save them to the Save object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmCreateClick(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(SaveNameForm.Text, @"^[a-zA-Z0-9 _]{3,50}$") || !Regex.IsMatch(SaveSourcePathForm.Text, @"^[a-zA-Z]:(?:[\/\\][a-zA-Z0-9 _#]+)*$") || !Regex.IsMatch(SaveDestinationPathForm.Text, @"^[a-zA-Z]:(?:[\/\\][a-zA-Z0-9 _#]+)*$") || SaveTypeForm.SelectedItem == null)
            {
                MessageBox.Show("Error: Please fill every field before confirming.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                ChangeUIElementEnableState(FormElementList, false);
                ChangeUIElementEnableState(OptionButtonList, true);
                ChangeUIElementEnableState(SelectionButtonList, true);
                ChangeUIElementEnableState(CheckBoxList, false);
                ALL.IsEnabled = false;
                ChangeUIElementVisibilityState(confirmButtonList, Visibility.Hidden);

                ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;

                List<Extension> extensionList = new List<Extension>();
                if (ALL.IsChecked == false)
                {
                    foreach (CheckBox _checkBox in CheckBoxList)
                    {
                        if (_checkBox.IsChecked == true)
                        {
                            Enum.TryParse(_checkBox.Name.ToLower(), out Extension _extension);
                            extensionList.Add(_extension);
                        }
                    }
                }
                else
                {
                    extensionList.Add(Extension.ALL);
                }

                SaveWorkType saveType = SaveWorkType.complete;
                if (((ComboBoxItem)SaveTypeForm.SelectedItem).Content.ToString() != "Complete") { saveType = SaveWorkType.differencial; }

                VM.CreateSaveProcedure(SaveNameForm.Text, SaveSourcePathForm.Text.Replace("\\", "/"), SaveDestinationPathForm.Text.Replace("\\", "/"), saveType, extensionList);

                ClearForm();

                SaveList.Items.Refresh();
            }
        }

        /// <summary>
        /// Confirm changes and save them to the existing object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmModifyClick(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(SaveNameForm.Text, @"^[a-zA-Z0-9 _]{3,50}$") || !Regex.IsMatch(SaveSourcePathForm.Text, @"^[a-zA-Z]:(?:[\/\\][a-zA-Z0-9 _#]+)*$") || !Regex.IsMatch(SaveDestinationPathForm.Text, @"^[a-zA-Z]:(?:[\/\\][a-zA-Z0-9 _#]+)*$") || SaveTypeForm.SelectedItem == null)
            {
                MessageBox.Show("Error: Please fill every field before confirming.\n\n -Name should contain between 3 and 50 alphanumeric, space or underscore characters.\n\n -Paths must follow this structure : DRIVE_LETTER:/folder1/folder2...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                ChangeUIElementEnableState(FormElementList, false);
                ChangeUIElementEnableState(OptionButtonList, true);
                ChangeUIElementEnableState(SelectionButtonList, true);
                ChangeUIElementEnableState(CheckBoxList, false);
                ALL.IsEnabled = false;
                ChangeUIElementVisibilityState(confirmButtonList, Visibility.Hidden);

                ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;

                List<Extension> extensionList = new List<Extension>();
                if (ALL.IsChecked == false)
                {
                    foreach (CheckBox _checkBox in CheckBoxList)
                    {
                        if (_checkBox.IsChecked == true)
                        {
                            Enum.TryParse(_checkBox.Name.ToLower(), out Extension _extension);
                            extensionList.Add(_extension);
                        }
                    }
                }
                else
                {
                    extensionList.Add(Extension.ALL);
                }

                SaveWorkType saveType = SaveWorkType.complete;
                if (((ComboBoxItem)SaveTypeForm.SelectedItem).Content.ToString() != "Complete") { saveType = SaveWorkType.differencial; }

                VM.ModifySaveProcedure(selectedItem.Index, SaveNameForm.Text, SaveSourcePathForm.Text.Replace("\\", "/"), SaveDestinationPathForm.Text.Replace("\\", "/"), saveType, extensionList);

                ClearForm();

                SaveList.Items.Refresh();
            }
        }

        /// <summary>
        /// Cancel any changes and go back to inital state of the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ChangeUIElementEnableState(FormElementList, false);
            ChangeUIElementEnableState(optionButtonList, true);
            ChangeUIElementEnableState(selectionButtonList, true);
            ChangeUIElementEnableState(checkBoxList, false);
            ALL.IsEnabled = false;
            ChangeUIElementVisibilityState(confirmButtonList, Visibility.Hidden);
            ClearForm();
        }

        #endregion

        #region Change UIElement state

        /// <summary>
        /// Controls the first time an item is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ItemSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!IsAnItemSelected)
            {
                IsAnItemSelected = true;
                ChangeUIElementEnableState(SelectionButtonList, true);
            }
        }

        /// <summary>
        /// Empty all of the form fields
        /// </summary>
        private void ClearForm()
        {
            SaveNameForm.Text = "";
            SaveSourcePathForm.Text = "";
            SaveDestinationPathForm.Text = "";
            SaveTypeForm.SelectedIndex = -1;
            foreach (CheckBox _checkBox in CheckBoxList)
            {
                _checkBox.IsChecked = false;
            }
        }

        /// <summary>
        /// Enable or disable elements inside an UIElement Collection.
        /// </summary>
        /// <param name="_elementCollection">The UIELement Collection you want to enable/disable.</param>
        /// <param name="_choice">Choose if you want to enable (true) the UIElements or not (false).</param>
        private void ChangeUIElementEnableState(List<UIElement> _elementList, bool _choice)
        {
            if (!(_elementList == SelectionButtonList && IsAnItemSelected == false))
            {
                foreach (UIElement element in _elementList)
                {
                    element.IsEnabled = _choice;
                }

            }
        }

        /// <summary>
        /// Set the visibility of elements inside an UIElement Collection
        /// </summary>
        /// <param name="_elementList">The UIELement Collection you want to show/hide.</param>
        /// <param name="_choice">Choose the visibility state Visible, Hidden or Collapsed.</param>
        private void ChangeUIElementVisibilityState(List<UIElement> _elementList, Visibility _choice)
        {
            foreach (UIElement element in _elementList)
            {
                element.Visibility = _choice;
            }
        }

        /// <summary>
        /// Disable checkboxes when All is ticked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALL_Checked(object sender, RoutedEventArgs e)
        {
            ChangeUIElementEnableState(CheckBoxList, false);
        }

        /// <summary>
        /// Enable checkboxes when All is unticked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALL_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeUIElementEnableState(CheckBoxList, true);
        }

        #endregion

        #endregion

        #region Setting Pannel

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
