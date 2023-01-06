using System.Collections.Generic;
using System.Linq;
using EmployeeManager.Models;
using EmployeeManager.Library.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using System.Windows.Controls;
using System.Windows;
using EmployeeManager.DataLayer.RepositoryDataModel;
using Prism.Events;
using EmployeeManager.Library.Events;

namespace EmployeeManager.Main.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        DataContextLayer repository;

        public EmployeeViewModel() : base("EmployeeViewModel")
        {
            /*IEventAggregator ea = new EventAggregator();
            ea.GetEvent<RefreshListEvent>().Subscribe( RefreshList );
              */
            repository = new DataContextLayer(/*ea*/);

            ImportDataSourceCommand = new DelegateCommand(ImportDataSource);

            ExportDataSourceCommand = new DelegateCommand(ExportDataSource)
                /*.ObservesCanExecute(() => CanSaveData)*/;

            AddNewEmployeeCommand = new DelegateCommand(AddNewEmployee);

            EditEmployeeCommand = new DelegateCommand(EditEmployee)
                .ObservesProperty(() => Employee)
                .ObservesCanExecute(() => IsEmployeeSelected);

            DeleteEmployeeCommand = new DelegateCommand(DeleteEmployee)
                .ObservesProperty(() => Employee)
                .ObservesCanExecute(() => IsEmployeeSelected); ;

            RefreshList();
        }

        private ObservableCollection<Employee> employeeList;
        public ObservableCollection<Employee> EmployeeList
        {
            get { return employeeList; }
            set { SetProperty(ref employeeList, value); }
        }

        public Employee Employee
        {
            get { return employee; }
            set { SetProperty(ref employee, value); }
        }

        private Employee employee;


        private ObservableCollection<Department> departmentList;
        public ObservableCollection<Department> DepartmentList
        {
            get { return departmentList; }
            set { SetProperty(ref departmentList, value); }
        }

        private Department department;
        public Department Department
        {
            get { return department; }
            set { 
                if(!SetProperty(ref department, value)) return;
                ChangeFilter();
            }
        }

        #region Commands reference...

        public ICommand ImportDataSourceCommand { get; }
        public ICommand ExportDataSourceCommand { get; }

        public ICommand AddNewEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }
        #endregion

        /// <summary>
        /// Обновляет представление
        /// </summary>
        /// <param name="refreshAll">Грузить все данные для контролов</param>
        void RefreshList(bool refreshAll = true) {

            EmployeeList = new ObservableCollection<Employee>(GetEmployeers());

            if (!refreshAll) return;

            DepartmentList = new ObservableCollection<Department>(GetDepartments());

            /*Для исключаещего фильтра**/
            DepartmentList.Insert(0, new Department
            {
                Name = "Все",
                Id = -1
            });
        }

        void ChangeFilter() {
            EmployeeList.Clear();
            EmployeeList.AddRange(repository.GetEmployeers(Department));
        }

        private List<Employee> GetEmployeers() => repository.GetEmployeers();
        private List<Department> GetDepartments() => repository.GetDepartments();

        #region Commands implementation

        void ImportDataSource()
        {
            repository.BackupDatabase();
            RefreshList(true);
        }

        void ExportDataSource()
        {
            if (!CanSaveData) return;

            repository.DumpDatabase();
        }

        void AddNewEmployee()
        {
            var modalDialog = new ModalWindow(null);

            if (modalDialog.ShowDialog().Equals(true))
                ChangeFilter();
        }

        void EditEmployee()
        {
            var modalDialog = new ModalWindow(Employee.Id);

            if(modalDialog.ShowDialog().Equals(true))
                ChangeFilter();
        }

        void DeleteEmployee()
        {
            var userChoiseResult = MessageBox.Show($@"Будет удалено следующее из списка {Employee}");
            if(MessageBoxResult.OK == userChoiseResult)
            {
                repository.RemoveEmployee(Employee);
                ChangeFilter();
            }
        }

        bool IsEmployeeSelected => Employee != null && Employee != default(Employee);

        
        public bool CanSaveData
        {
            get { return EmployeeList != null && EmployeeList.Any(); }
        }
        #endregion
    }
}
