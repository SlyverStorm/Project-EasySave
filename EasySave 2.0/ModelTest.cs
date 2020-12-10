using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave_2._0
{
    class ModelTest : INotifyPropertyChanged
    {
        #region Variables

        private List<SaveWorkTest> _workList;

        public List<SaveWorkTest> WorkList
        {
            get { return _workList; }
            set { _workList = value; }
        }

        #endregion

        public ModelTest()
        {
            WorkList = new List<SaveWorkTest>();
            WorkList.Add(new SaveWorkTest(1, "Test Save", "C:/balek", "D:/balek*2", SaveWorkTest.SaveWorkTestType.complete, new List<SaveWorkTest.SaveWorkTestExtension> { SaveWorkTest.SaveWorkTestExtension.exe, SaveWorkTest.SaveWorkTestExtension.gif }));
            WorkList.Add(new SaveWorkTest(2, "CorenQ", "fdzafzda", "fdzafzda", SaveWorkTest.SaveWorkTestType.differencial, new List<SaveWorkTest.SaveWorkTestExtension> { SaveWorkTest.SaveWorkTestExtension.ALL }));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void CreateCompleteWork(string _name, string _sourcePath, string _destinationPath, List<SaveWorkTest.SaveWorkTestExtension> _extensionList)
        {
            WorkList.Add(new SaveWorkTest(WorkList[WorkList.Count - 1].SaveID + 1, _name, _sourcePath, _destinationPath, SaveWorkTest.SaveWorkTestType.complete, _extensionList));
        }

        public void CreateDifferencialWork(string _name, string _sourcePath, string _destinationPath, List<SaveWorkTest.SaveWorkTestExtension> _extensionList)
        {
            WorkList.Add(new SaveWorkTest(WorkList[WorkList.Count - 1].SaveID + 1, _name, _sourcePath, _destinationPath, SaveWorkTest.SaveWorkTestType.differencial, _extensionList));
        }
    }
}
