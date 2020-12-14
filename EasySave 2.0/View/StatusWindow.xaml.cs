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
    /// Interaction logic for MainWindow.xaml Status frame
    /// </summary>
    public partial class BaseWindow : Window
    {
        
        #region Methods

        /// <summary>
        /// Called when a property is changed in SaveProgress Object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Progress_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ProgressState")
            {
                ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;
                int _percentage = Convert.ToInt32(Math.Floor(VM.Model.WorkList[selectedItem.Index].Progress.ProgressState));
                ChangeSaveProgressLabel(_percentage);
            }
        }

        #region Update Labels

        /// <summary>
        /// Changes the curent save label content
        /// </summary>
        /// <param name="_currentSaveName">Name of the current save procedure</param>
        private void ChangeCurrentSaveLabel(string _currentSaveName)
        {
            CurrentSaveLabel.Content = _currentSaveName;
        }

        /// <summary>
        /// Changes the curent save status label content
        /// </summary>
        /// <param name="_currentSaveStatus">Status of the current save procedure</param>
        private void ChangeSaveStatusLabel(SaveStatusEnum _currentSaveStatus)
        {
            switch (_currentSaveStatus)
            {
                case SaveStatusEnum.running:
                    SaveStatusLabel.Content = Properties.Langs.Lang.Running;
                    break;
                case SaveStatusEnum.paused:
                    SaveStatusLabel.Content = Properties.Langs.Lang.Paused;
                    break;
                case SaveStatusEnum.encryption:
                    SaveStatusLabel.Content = Properties.Langs.Lang.Encryption;
                    break;
                case SaveStatusEnum.complete:
                    SaveStatusLabel.Content = Properties.Langs.Lang.Complete;
                    break;
            }
        }

        /// <summary>
        /// Changes the save progress label content.
        /// </summary>
        /// <param name="_saveProgress">percentage completion of the save procedure</param>
        private void ChangeSaveProgressLabel(int _saveProgress)
        {
            SaveProgressLabel.Content = _saveProgress + " %";
            if (_saveProgress == 100)
            {
                ChangeSaveStatusLabel(SaveStatusEnum.complete);
                PauseSaveSatus.IsEnabled = false;
                ResumeSaveStatus.IsEnabled = false;
                CancelSaveSatus.Visibility = Visibility.Collapsed;
                CloseSaveSatus.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Closes save status frame after save completion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseSaveStatus_Click(object sender, RoutedEventArgs e)
        {
            ChangeSaveStatusLabel(SaveStatusEnum.complete);
            CloseSaveSatus.Visibility = Visibility.Collapsed;
            CancelSaveSatus.Visibility = Visibility.Visible;
            SaveStatus.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Informs the Model of the pause request, and change UIElements accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseSaveStatus_Click(object sender, RoutedEventArgs e)
        {
            PauseSaveSatus.IsEnabled = false;
            ResumeSaveStatus.IsEnabled = true;
            ChangeSaveStatusLabel(SaveStatusEnum.paused);

            //if solo save
            ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;
            VM.PauseSaveProcedure(selectedItem.Index, true);
            //else all
        }

        /// <summary> 
        /// Informs the Model of the resume request, and change UIElements accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResumeSaveStatus_Click(object sender, RoutedEventArgs e)
        {
            PauseSaveSatus.IsEnabled = true;
            ResumeSaveStatus.IsEnabled = false;
            ChangeSaveStatusLabel(SaveStatusEnum.running);

            //if solo save
            ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;
            VM.PauseSaveProcedure(selectedItem.Index, false);
            //else all

        }

        /// <summary>
        /// Informs the Model of the cancel request, and closes the save status frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelSaveStatus_Click(object sender, RoutedEventArgs e)
        {
            SaveStatus.Visibility = Visibility.Collapsed;

            //if solo save
            ISaveWork selectedItem = (ISaveWork)SaveList.SelectedItem;
            VM.CancelSaveProcedure(selectedItem.Index);
            //else all

        }

        #endregion

        /// <summary>
        /// Possible save procedure status
        /// </summary>
        enum SaveStatusEnum
        {
            running,
            paused,
            complete,
            encryption
        }       

        #endregion
    }
}
