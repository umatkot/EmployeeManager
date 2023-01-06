using AutoMapper;
using EmployeeManager.Models.Mapper;
using EmployeeManager.Models;
using System.Collections.Generic;
using System.Linq;
using EmployeeManager.Library.FileExchange;
using EmployeeManager.DataLayer.RepositoryDataModel;
using System;

namespace EmployeeManager.Library.Repository
{
    public class DataContextLayer
    {
        //private IEventAggregator _eventAggregator;
        private readonly EmployeeRepository _data;
        private readonly IMapper _mapper;

        public DataContextLayer(/*IEventAggregator eventAggregator*/)
        {
            _data = new EmployeeRepository();
            _mapper = MapperInitializer.Init().CreateMapper(); 
            //_eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Готовый запрос для БД, выполняется в .ToList(), 
        /// поэтому на этом этапе его ещё можно фильтровать с целью выбора только нужных данных из БД
        /// </summary>
        /// <param name="filterDepartment">Filter for query with department id parameter</param>
        /// <returns></returns>
        public List<Employee> GetEmployeers(Department filterDepartment = null)
        {
            var dataList = _data.GetAll();

            //if(!(new Nullable<int> [] {-1, null }).Contains( filterDepartment?.Id))
            if(filterDepartment != null && filterDepartment.Id != -1)
            {
                dataList = dataList.Where(employee => employee.Department == filterDepartment.Id);
            }

            return _mapper.Map<List<Employee>>(dataList.ToList());
        }

        public List<Department> GetDepartments()
        {
            var departments = _data.GetAllDepartments();
            return _mapper.Map<List<Department>>(departments);
        }

        public void BackupDatabase()
        {
            var fileOpenDlg = new FileSupportDialog();

            if (!fileOpenDlg.ShowOpenInstance() || string.IsNullOrEmpty(fileOpenDlg.FilePath))
                return;

            var fileWorker = new XMLFileWorker();

            var employeers = fileWorker.Load(fileOpenDlg.FilePath);

            _data.DeleteAll();

            var db_employeers = _mapper.Map<List<Employeers>>(employeers);

            _data.AddList(db_employeers);

            //_eventAggregator.GetEvent<RefreshListEvent>().Publish(true);

            return;
        }

        public void DumpDatabase() { 
            
            var fileOpenDialog = new FileSupportDialog();

            if (!fileOpenDialog.ShowSaveInstance())
                return;

            var fileWorker = new XMLFileWorker();

            var employeers = GetEmployeers();

            fileWorker.Save(employeers, fileOpenDialog.FilePath);
        }

        public void AddEmployee(Employee employee, Department department)
        {
            var employeers = _mapper.Map<Employeers>(employee);
            var departments = _mapper.Map<Departments>(department);
            employeers.Department = departments.ID;
            employeers.Departments = null;
            _data.Add(employeers);
        }

        public void RemoveEmployee(Employee employee)
        {
            _data.RemoveById(employee.Id);
        }

        public Employee GetEmployee(int? id)
        {
            return id == null ?
                new Employee { DateBirth = DateTime.Now } :
                _mapper.Map<Employee>(_data.GetEmployeeById((int)id));
        }

        public Department GetEmployeeDepartment(int employeeId)
        {
            return _mapper.Map<Department>(_data.GetEmployeeById(employeeId).Departments);
        }

        public void EditEmployee(Employee employee, Department department)
        {
            var employeers = _mapper.Map<Employeers>(employee);
            employeers.Department = department.Id;
            _data.Save(employeers);
        }
    }
}
