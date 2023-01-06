using EmployeeManager.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using EmployeeManager.Models.ExchangeModels;
using AutoMapper;
using EmployeeManager.Models.Mapper;
using System.Xml;

namespace EmployeeManager.Library.FileExchange
{
    public class XMLFileWorker : IFileExchange
    {
        private readonly IMapper _mapper;

        public XMLFileWorker() {
            _mapper = MapperInitializer.Init().CreateMapper();
        }

        public List<Employee> Load() => new List<Employee>();

        public List<Employee> Load(string filePath) {

            var xelements = XElement.Load(filePath);

            var serializer = new XmlSerializer(typeof(XExport));

            var elements = serializer.Deserialize(xelements.CreateReader()) as XExport;

            var data = _mapper.Map<List<Employee>>(elements.Employeers);

            return data;
        }

        public void Save(List<Employee> objects, string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(XExport));

            var wrapper = new XExport {
                Employeers = _mapper.Map<List<XEmployee>>(objects)
            };

            XDocument document = new XDocument();
            using (var writer = document.CreateWriter())
            {
                xmlSerializer.Serialize(writer, wrapper);
                writer.Close();
                document.Save(filePath);
            }
        }
    }
}
