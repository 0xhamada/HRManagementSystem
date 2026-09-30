using HR.BLL.ModelVM.LeaveRequestVM;
using HR.BLL.ModelVM.ResponseResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface ILeaveTypeService
    {
        Response<List<LeaveTypeVM>> GetAll();
        Response<LeaveTypeVM> GetById(int id);
        Response<bool> Create(CreateLeaveTypeVM vm);
        Response<bool> Edit(EditLeaveTypeVM vm);
        Response<bool> Delete(int id);
    }
}
