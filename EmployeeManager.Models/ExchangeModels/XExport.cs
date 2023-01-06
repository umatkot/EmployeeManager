using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeManager.Models.ExchangeModels
{
    [XmlRoot("EXPORT")]
    public class XExport
    {
        [XmlElement("EMPLOYEE")]
        public List<XEmployee> Employeers { get; set; }
    }
}
