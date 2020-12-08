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
        public MainWindow()
        {
            VM = new ViewModel();
            DataContext = VM;
            InitializeComponent();
        }
        #endregion

        #region Methods



        #endregion

        /*private void SaveList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SaveList.Items.Add(DataContext.SaveExemple());
        }*/
    }
}
