using AutoMapper;
using HR.BLL.ModelVM.AccountVM;
using HR.BLL.ModelVM.Department;
using HR.BLL.ModelVM.Employee;
using HR.BLL.ModelVM.LeaveRequestVM;
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
            CreateMap<Employee, RegisterEmployeeVM>().ReverseMap();
            CreateMap<LeaveRequest, CreateLeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequestListVM, LeaveRequest>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveType, EditLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeVM, EditLeaveTypeVM>().ReverseMap();
            CreateMap<Employee, ProfileVM>().ForMember(dest => dest.DepartmentName,
                                                       opt => opt.MapFrom(src => src.Department != null 
                                                                                    ? src.Department.Name 
                                                                                    : null));
            CreateMap<LeaveType, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<Department, CreateDepartmentVM>().ReverseMap();
            CreateMap<Department, GetDepartmentVM>().ReverseMap();
            CreateMap<Department, EditDepartmentVM>().ReverseMap();
            CreateMap<GetDepartmentVM, EditDepartmentVM>().ReverseMap();
        }
        
    }
}
