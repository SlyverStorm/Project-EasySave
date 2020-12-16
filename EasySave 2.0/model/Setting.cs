using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave_2._0
{


    public class Setting : INotifyPropertyChanged
    {

        private List<Extension> priorityExtension;

        public List<Extension> PriorityExtension
        {
            get { return priorityExtension; }
            set
            {
                priorityExtension = value;
                OnPropertyChanged("PriorityExtension");
            }
        }

        private long maxTransferSize;

        public long MaxTransferSize
        {
            get { return maxTransferSize; }
            set
            {
                maxTransferSize = value;
                OnPropertyChanged("MaxTransferSize");
            }
        }

        private string softwareString;

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

        public Setting()
        {
            
        }

    }
}
