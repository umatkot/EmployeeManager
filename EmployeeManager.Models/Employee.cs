using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace EmployeeManager.Models
{
    public class Employee : ModelBase
    {
        private string departmentName;
        public string DepartmentName
        {
            get { return departmentName; }
            set { SetProperty(ref departmentName, value); }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set { SetProperty(ref middleName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        private DateTime dateBirth;
        public DateTime DateBirth
        {
            get { return dateBirth; }
            set { SetProperty(ref dateBirth, value); }
        }

        private string position;
        public string Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(string.Concat(LastName, FirstName, MiddleName, DepartmentName, Position)))
                return "новый";

            return $"{LastName} {FirstName} {MiddleName} - {DepartmentName}({Position})";
        }
    }
}
