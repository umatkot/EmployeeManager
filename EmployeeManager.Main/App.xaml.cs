using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using EmployeeManager.Main.Startup;
using EmployeeManager.Main.ViewModel;
//using System.ComponentModel;
using System.Windows;

namespace EmployeeManager.Main
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static AutofacServiceLocator _locator;
        IContainer _container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _container = Bootstrapper.Bootstrap();

            
            _locator = new AutofacServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => _locator);
            

            var viewModel = _container.Resolve<EmployeeViewModel>();
            var mainWindow = _container.Resolve<MainWindow>();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }
}
