using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeManager.Models.ExchangeModels
{
    public class XEmployeeDataElement
    {
        [XmlElement("FIRSTNAME")]
        public string FirstName { get; set; }

        [XmlElement("MIDDLENAME")]
        public string MiddleName { get; set; }

        [XmlElement("LASTNAME")]
        public string LastName { get; set; }

        
        [XmlIgnore]
        public System.DateTime DateBirth { get; set; }

        [XmlElement("DATEBIRTH")]
        public string DateBirthString
        {
            get { return DateBirth.ToString("dd.MM.yyyy"); }
            set { DateBirth = DateTime.Parse(value); }
        }


        [XmlElement("POSITION")]
        public string Position { get; set; }

        [XmlElement("PHONE")]
        public string Phone { get; set; }

        [XmlElement("EMAIL")]
        public string Email { get; set; }
    }
}
