using EmployeeManager.Library.FileExchange;
using EmployeeManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeManager.UnitTests
{
    [TestClass]
    public class FileExchangeTests
    {
        string _initialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

        [TestMethod]
        public void LoadFileTestShowldRetriveBusinesEntities()
        {
            var fileName = Path.Combine(_initialDirectory, "test1.xml");

            var fileWorker = new XMLFileWorker();

            var result = fileWorker.Load(fileName);
        }

        [TestMethod]
        public void SaveFileTest()
        {
            var fileName = Path.Combine(_initialDirectory, "test2.xml");

            var fileWorker = new XMLFileWorker();

            var employeers = new List<Employee>() { 
                new Employee()
                {
                    DepartmentName = "Тестовый отдел",
                    FirstName = "Имя",
                    MiddleName = "Отчество",
                    LastName = "Фамилия",
                    DateBirth = DateTime.Parse("01.11.2001"),
                    Position = "Тестовая должность",
                    Phone = "+7812 123456",
                    Email = "test@mail.org"
                }
            };

            fileWorker.Save(employeers, fileName);
        }
    }
}
