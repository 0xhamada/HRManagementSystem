using AutoMapper;
using HR.BLL.ModelVM.Department;
using HR.BLL.ModelVM.Employee;
using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Employee, GetEmployeeVM>().ReverseMap();
            CreateMap<Employee, CreateEmployeeVM>().ReverseMap();
            CreateMap<EditEmployeeVM, GetEmployeeVM>().ReverseMap();
            CreateMap<Employee, EditEmployeeVM>().ReverseMap();
          //  CreateMap<Employee, RegisterEmployeeVM>().ReverseMap();

            CreateMap<Department, CreateDepartmentVM>().ReverseMap();
            CreateMap<Department, GetDepartmentVM>().ReverseMap();
            CreateMap<Department, EditDepartmentVM>().ReverseMap();
            CreateMap<GetDepartmentVM, EditDepartmentVM>().ReverseMap();
        }
        
    }
}
