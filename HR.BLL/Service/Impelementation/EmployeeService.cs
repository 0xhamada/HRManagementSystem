using AutoMapper;
using HR.BLL.ModelVM.AccountVM;
using HR.BLL.ModelVM.Employee;
using HR.BLL.ModelVM.ResponseResult;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace HR.BLL.Service.Impelementation
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper mapper;
        private readonly IEmployeeRepo repo;
        private readonly UserManager<Employee> userManger;

        public EmployeeService(IMapper mapper, IEmployeeRepo repo, UserManager<Employee> userManger)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.userManger = userManger;
           
        }
        public Response<bool> AddEmployee(CreateEmployeeVM employeeVm, string? ImageName)
        {

            try
            {
                var existing = repo.GetAll(a => a.Name == employeeVm.Name).FirstOrDefault();

                if (existing != null)
                {
                    return new Response<bool>(false, "This name already exists", true);
                }

                var map = mapper.Map<Employee>(employeeVm);
                if (ImageName != null)
                {
                    map.SetImage(ImageName);
                }


                var result = repo.Add(map);
                return new Response<bool>(result, result ? null : "Failed", !result);
            }
            catch (Exception e)
            {
                return new Response<bool>(false, e.Message, false);

            }
        }

        public Response<bool> DeleteEmployee(string id)
        {
            try
            {
                var result = repo.ToggleStatus(id);
             return   new Response<bool>(result, result ? null : "Falied To Delete", !result);

            }
            catch(Exception e)
            {
               return new Response<bool>(false, e.Message, true);
            }
        }

        public Response<bool> EditEmployee(EditEmployeeVM employeeVM, string? FileName)
        {
            try
            {
                var map = mapper.Map<Employee>(employeeVM);

                if (FileName != null)
                    map.UpdateImage(FileName);
                var result = repo.Edit(map);

                return new Response<bool>(result, result ? null : "Failed", !result);
            }
            catch (Exception e)
            {
                return new Response<bool>(false, e.Message, false);

            }
        }

        public Response<List<GetEmployeeVM>> GetActiveEmployee()
        {
            try
            {
                var result = repo.GetAll(a => a.IsDeleted == false);
                var map = mapper.Map<List<GetEmployeeVM>>(result);
                return new Response<List<GetEmployeeVM>>(map, null, false);
            }
            catch (Exception e)
            {
                return new Response<List<GetEmployeeVM>>(null, e.Message, true);

            }
        }

        public Response<GetEmployeeVM> GetEmployeeById(string id)
        {
            try
            {
                var result = repo.GetById(id);
                if (result != null)
                {
                    var map = mapper.Map<GetEmployeeVM>(result);
                    return new Response<GetEmployeeVM>(map, null, false);
                }
                return new Response<GetEmployeeVM>(null, "Failed", true);
            }
            catch (Exception e)
            {
                return new Response<GetEmployeeVM>(null, e.Message, true);

            }
        }
        public async Task<IdentityResult> RegisterEmployee(RegisterEmployeeVM employee)
        {
            // Map  RegisterEmployeeVM  to employee
            var emp = mapper.Map<Employee>(employee);
            var result = await userManger.CreateAsync(emp, employee.Password);
            if (result.Succeeded)
            {
                await userManger.AddToRoleAsync(emp, "Employee");
            }
            return result;
            
        }
    }
}
