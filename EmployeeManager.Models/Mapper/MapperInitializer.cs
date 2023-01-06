using AutoMapper;
using EmployeeManager.DataLayer.RepositoryDataModel;
using EmployeeManager.Models;
using EmployeeManager.Models.ExchangeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models.Mapper
{
    public static class MapperInitializer
    {
        public static MapperConfiguration Init()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Departments, Department>()
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.ID))
                    .ReverseMap();

                cfg.CreateMap<Employee, Employeers>()
                    .ForMember(d => d.ID, o => o.MapFrom(s => s.Id))
                    /*.ForMember(d => d.Departments, 
                        o => o.MapFrom(s => string.IsNullOrEmpty(s.DepartmentName) ?
                            new Departments { Name = s.DepartmentName } : null))*/
                    .ForMember(d => d.Departments,
                        o => o.MapFrom(s => new Departments { Name = s.DepartmentName }))
                    .ForMember(d => d.Department, o => o.Ignore())
                    .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                    .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.MiddleName))
                    .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                    .ForMember(d => d.DateBirth, o => o.MapFrom(s => s.DateBirth))
                    .ForMember(d => d.Position, o => o.MapFrom(s => s.Position))
                    .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone))
                    .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));

                cfg.CreateMap<Employeers, Employee>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.ID))
                    .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Departments.Name))
                    .ForSourceMember(s => s.Department, o => o.Ignore())
                    .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                    .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.MiddleName))
                    .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                    .ForMember(d => d.DateBirth, o => o.MapFrom(s => s.DateBirth))
                    .ForMember(d => d.Position, o => o.MapFrom(s => s.Position))
                    .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone))
                    .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));

                cfg.CreateMap<XEmployee, Employee>()
                    .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department))
                    .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Data.FirstName))
                    .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.Data.MiddleName))
                    .ForMember(d => d.LastName, o => o.MapFrom(s => s.Data.LastName))
                    .ForMember(d => d.DateBirth, o => o.MapFrom(s => s.Data.DateBirth))
                    .ForMember(d => d.Position, o => o.MapFrom(s => s.Data.Position))
                    .ForMember(d => d.Phone, o => o.MapFrom(s => s.Data.Phone))
                    .ForMember(d => d.Email, o => o.MapFrom(s => s.Data.Email));

                cfg.CreateMap<Employee, XEmployeeDataElement>();

                cfg.CreateMap<Employee, XEmployee>()
                    .ForMember(d => d.Data, o => o.MapFrom(s => s))
                    .ForMember(d => d.Department, o => o.MapFrom(s => s.DepartmentName));
            });

            return config;
        }
    }
}
