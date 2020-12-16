using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave_2._0
{
    /// <summary>
    /// Setting class for Model
    /// </summary>
    public class Setting : INotifyPropertyChanged
    {
        private List<Extension> priorityExtension;
        /// <summary>
        /// Priority extension to save 
        /// </summary>
        public List<Extension> PriorityExtension
        {
            get { return priorityExtension; }
            set
            {
                priorityExtension = value;
                OnPropertyChanged("PriorityExtension");
            }
        }

        public static long maxTransferSize;
        /// <summary>
        /// Max multithreading transfer size
        /// </summary>
        public long MaxTransferSize
        {
            get { return maxTransferSize; }
            set
            {
                maxTransferSize = value;
                OnPropertyChanged("MaxTransferSize");
            }
        }

        public static string softwareString;
        /// <summary>
        /// Business software name string
        /// </summary>
        public string SoftwareString
        {
            get { return softwareString; }
            set
            {
                softwareString = value;
                OnPropertyChanged("SoftwareString");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
