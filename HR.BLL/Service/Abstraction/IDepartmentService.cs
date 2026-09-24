using HR.BLL.ModelVM.Department;
using HR.BLL.ModelVM.ResponseResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface IDepartmentService
    {
        Response<bool> AddDepartment(CreateDepartmentVM vm);
        Response<bool> EditDepartment(EditDepartmentVM vm);
        Response<bool> DeleteDepartment(int id);

        Response<GetDepartmentVM> GetDepartmentById(int id);
        Response<List<GetDepartmentVM>> GetAllDepartments();
    }
}
