using EmployeeManager.DataLayer.RepositoryDataModel;
using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Library.Repository
{
    public class EmployeeRepository //: IRepository<Employeers>
    {
        private readonly EmployeeDbEntities _db;

        internal EmployeeRepository()
        {
            _db = new EmployeeDbEntities();
        }

        

        private void LogHistory<T>(string tableName, string keyName, T entity, InsertMode insertMode)
        {
            var propInfo = typeof(T).GetProperty(keyName);
            var isImport = insertMode == InsertMode.Import;

            var logHistory = new HistoryLevel
            {
                InsertMode = isImport,
                RecordId = (int)propInfo.GetValue(entity),
                TableName = tableName
            };

            _db.HistoryLevelSet.Add(logHistory);

            _db.SaveChanges();
        }
        private void LogHistory<T>(string tableName, string keyName, List<T> entity, InsertMode insertMode)
        {
            var propInfo = typeof(T).GetProperty(keyName);
            var isImport = insertMode == InsertMode.Import;

            var logHistory = entity.Select(e =>
                new HistoryLevel
                {
                    InsertMode = isImport,
                    RecordId = (int)propInfo.GetValue(e),
                    TableName = tableName
                });

            _db.HistoryLevelSet.AddRange(logHistory);

            _db.SaveChanges();
        }

        internal void DeleteAll()
        {
            _db.Database.ExecuteSqlCommand(
@"
DELETE FROM Employeers;
DELETE FROM Departments;
DELETE FROM HistoryLevelSet;

--DBCC CHECKIDENT ('Employeers', RESEED, 1);
--DBCC CHECKIDENT ('Departments', RESEED, 1);
--DBCC CHECKIDENT ('HistoryLevelSet', RESEED, 1);

");

        }

        internal void Add(Employeers entity)
        {
            var baseEntity = _db.Employeers.Add(entity);

            _db.SaveChanges();

            LogHistory("Employeers", "ID", baseEntity, InsertMode.User);
        }

        internal void AddList(List<Employeers> entities)
        {
            var departments = entities
                .Select(e => e.Departments.Name)
                .Distinct()
                .Select(dn => new Departments { Name = dn });

            _db.Departments.AddRange(departments);

            _db.SaveChanges();

            var departmentsEntities = _db.Departments.AsNoTracking().ToDictionary(d => d.Name);

            foreach (var employee in entities)
            {
                var departmentInEntity = departmentsEntities[employee.Departments.Name];
                employee.Departments = null;
                employee.Department = departmentInEntity.ID;
                LogHistory("Departments", "ID", departmentInEntity, InsertMode.Import);
            }

            var baseEmployeers = _db.Employeers.AddRange(entities);

            _db.SaveChanges();

            LogHistory("Employeers", "ID", baseEmployeers.ToList(), InsertMode.Import);
        }

        internal IQueryable<Employeers> GetAll() => from employee in _db.Employeers.AsNoTracking()
                                                  join department in _db.Departments.AsNoTracking()
                                                    on employee.Department equals department.ID
                                                  select employee;

        internal IQueryable<Departments> GetAllDepartments() => from department in _db.Departments.AsNoTracking()
                                                              select department;

        internal void RemoveById(int id)
        {
            var employee2Remove = _db.Employeers.Find(id);

            if (null == employee2Remove)
                return;

            var historyOfElement = _db.HistoryLevelSet
                .Where(h =>
                    h.RecordId == employee2Remove.ID &&
                    h.TableName == "Employeers")
                .Single();

            using (var _tx = _db.Database.BeginTransaction())
            {
                _db.HistoryLevelSet.Remove(historyOfElement);
                _db.Employeers.Remove(employee2Remove);

                try
                {
                    _db.SaveChanges();
                    _tx.Commit();
                }
                catch (Exception exc)
                {
                    _tx.Rollback();
                }
            }

        }

        internal Employeers GetEmployeeById(int id)
        {
            var employee = _db.Employeers.Find(id);
            
            _db.Entry(employee).State = EntityState.Detached;

            employee.Departments = _db.Departments.Find(employee.Department);

            _db.Entry(employee.Departments).State = EntityState.Detached;

            return employee;
        }

        internal void Save(Employeers employeers)
        {
            employeers.Departments = null;

            var modifiedEmployee = _db.Employeers.Add(employeers);
            
            _db.Entry(modifiedEmployee).State = EntityState.Modified;

            _db.SaveChanges();
        }
    }
}
