using AutoMapper;
using EmployeeManager.Models.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.UnitTests
{
    [TestClass]
    internal class TestBase
    {
        public static IMapper _mapper { get; set; }

        [ClassInitialize]
        public static void Initialize()
        {
            _mapper = MapperInitializer.Init().CreateMapper();
        }
    }
}
