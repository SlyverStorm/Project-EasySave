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
                SaveTypeForm,
                PickEncryptionForm
            };

            OptionButtonList = new List<UIElement>
            {
                ModifySave,
                LaunchSave,
                LaunchAllSave,
                Create,
                DeleteSave,
                SaveList
            };

            ConfirmButtonList = new List<UIElement>
            {
                Confirm,
                Back
            };

            ChangeUIElementEnableState(FormElementList, false);
            ChangeUIElementVisibilityState(ConfirmButtonList, Visibility.Hidden);

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
                ChangeUIElementEnableState(FormElementList, true);
                ChangeUIElementEnableState(optionButtonList, false);
                ChangeUIElementVisibilityState(ConfirmButtonList, Visibility.Visible);
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

                ChangeUIElementVisibilityState(confirmButtonList, Visibility.Visible);
            }
        }

        /// <summary>
        /// Creation of a new save / Visual duties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ChangeUIElementEnableState(FormElementList, true);
            ChangeUIElementEnableState(optionButtonList, false);
            ChangeUIElementVisibilityState(ConfirmButtonList, Visibility.Visible);
            ClearForm();
        }

        /// <summary>
        /// Confirm changes and save them to the Save object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ChangeUIElementEnableState(FormElementList, false);
            ChangeUIElementEnableState(optionButtonList, true);
            ChangeUIElementVisibilityState(confirmButtonList, Visibility.Hidden);
            // Sauvegarde et envoie de l'objet Save modifié vers le Model
            ClearForm();
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
            ChangeUIElementVisibilityState(confirmButtonList, Visibility.Hidden);
            ClearForm();
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

        /// <summary>
        /// Enable or disable elements inside an UIElement Collection.
        /// </summary>
        /// <param name="_elementCollection">The UIELement Collection you want to enable/disable.</param>
        /// <param name="_choice">Choose if you want to enable (true) the UIElements or not (false).</param>
        private void ChangeUIElementEnableState(List<UIElement> _elementList, bool _choice)
        {
            foreach (UIElement element in _elementList)
            {
                element.IsEnabled = _choice;
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

        #endregion

    }
}
