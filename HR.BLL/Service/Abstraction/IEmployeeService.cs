using HR.BLL.ModelVM.AccountVM;
using HR.BLL.ModelVM.Employee;
using HR.BLL.ModelVM.ResponseResult;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface IEmployeeService
    {
        Task<Response<bool>> AddEmployee(CreateEmployeeVM employeeVm, string? ImageName);
        Response<bool> EditEmployee(EditEmployeeVM employeeVM, string? FileName);
        Response<bool> DeleteEmployee(string id);
        Response<GetEmployeeVM> GetEmployeeById(string id);
        Response<List<GetEmployeeVM>> GetActiveEmployee();
        Response<ProfileVM> GetMyProfile(string employeeId);
        Task<IdentityResult> RegisterEmployee(RegisterEmployeeVM employee);
        Task<string?> GenerateResetTokenAsync(string email);
    }
}
