using HR.BLL.ModelVM.Employee;
using HR.BLL.ModelVM.ResponseResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface IEmployeeService
    {
        Response<bool> AddEmployee(CreateEmployeeVM employeeVm, string? ImageName);
        Response<bool> EditEmployee(EditEmployeeVM employeeVM, string? FileName);
        Response<bool> DeleteEmployee(string id);
        Response<GetEmployeeVM> GetEmployeeById(string id);
        Response<List<GetEmployeeVM>> GetActiveEmployee();
    }
}
