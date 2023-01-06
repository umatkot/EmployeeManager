using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class Department : ModelBase
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var objDepartment = obj as Department;
            if (objDepartment == null) return false;
            return objDepartment.Name == Name && objDepartment.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
