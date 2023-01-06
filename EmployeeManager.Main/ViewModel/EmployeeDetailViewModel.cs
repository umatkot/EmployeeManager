using EmployeeManager.DataLayer.RepositoryDataModel;
using EmployeeManager.Library.Repository;
using EmployeeManager.Models;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmployeeManager.Main.ViewModel
{
    internal class EmployeeDetailViewModel : ViewModelBase
    {
        DataContextLayer repository;
        public EmployeeDetailViewModel(int? id = null) : base("EmployeeDetailViewModel")
        {
            AddEmployeeCommand = new DelegateCommand(OnAddEmployee);
            EditEmployeeCommand = new DelegateCommand(OnEditEmployee);

            repository = new DataContextLayer(/*eventAggregator*/);

            Departments = new ObservableCollection<Department>(GetDepartments());

            /*Вообще-то так нехорошо делать                                                                                                                                           , но это тестовое задание для компании АО "ЦентрИнформ", поэтому здесь я себе это позволю*/
            Employee = repository.GetEmployee(id);
            IsEdit = id != null;
            ChangeViewMode();
            if (IsEdit)
            {
                Department = repository.GetEmployeeDepartment(Employee.Id);
            }
        }

        public bool IsEdit = false;

        private Visibility addVisibility;
        public Visibility AddVisibility
        {
            get { return addVisibility; }
            set { SetProperty(ref addVisibility, value); }
        }

        private Visibility editVisibility;
        public Visibility EditVisibility
        {
            get { return editVisibility; }
            set { SetProperty(ref editVisibility, value); }
        }

        private void ChangeViewMode()
        {
            if (IsEdit) 
            {
                EditVisibility = Visibility.Visible;
                AddVisibility = Visibility.Hidden;
            } else
            {
                EditVisibility = Visibility.Hidden;
                AddVisibility = Visibility.Visible;
            }
        }

        public ObservableCollection<Department> Departments { get; set; }


        private Department department;
        public Department Department
        {
            get { return department; }
            set { SetProperty(ref department, value); }
        }


        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set { SetProperty(ref employee, value); }
        }

        public event Action<bool> RequestClose;

        public ICommand AddEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; set; }

        private DelegateCommand closeDialogWindowCommand;
        public DelegateCommand CloseDialogWindowCommand =>
            closeDialogWindowCommand ?? (closeDialogWindowCommand = new DelegateCommand(ExecuteCloseDialogWindowCommand));

        void ExecuteCloseDialogWindowCommand()
        {
            RequestClose(false);
        }
                
        private List<Department> GetDepartments() => repository.GetDepartments();

        void OnAddEmployee()
        {
            repository.AddEmployee(Employee, Department);
            RequestClose(true);
        }

        void OnEditEmployee()
        {
            repository.EditEmployee(Employee, Department);
            RequestClose(true);
        }

        bool OnAddCanExecute()
        {
            return true;
        }
    }
}
