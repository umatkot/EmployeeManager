using EmployeeManager.Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManager.Main
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow(int? id)
        {
            InitializeComponent();

            var viewModel = new EmployeeDetailViewModel(id);

            viewModel.RequestClose += (dr) => 
            {
                DialogResult = dr;
                Close(); 
            };

            DataContext = viewModel;
            Owner = App.Current.MainWindow;
        }
    }
}
