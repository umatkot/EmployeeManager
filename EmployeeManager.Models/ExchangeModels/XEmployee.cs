using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeManager.Models.ExchangeModels
{

    public class XEmployee
    {
        [XmlElement("DEPARTMENT")]
        public string Department { get; set; }

        [XmlElement("DATA")]
        public XEmployeeDataElement Data { get; set; }
    }
}
