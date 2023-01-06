using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Library.FileExchange
{
    internal interface IFileExchange
    {
        List<Employee> Load();
        List<Employee> Load(string filePath);
        void Save(List<Employee> objects, string filePath);
    }
}
