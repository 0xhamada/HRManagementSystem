using HR.BLL.ModelVM.LeaveRequestVM;
using HR.BLL.ModelVM.ResponseResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface ILeaveRequestService
    {
        Response<bool> CreateRequest(string employeeId, CreateLeaveRequestVM vm);
        Response<List<LeaveRequestListVM>> GetMyRequests(string employeeId);
        Response<List<LeaveRequestListVM>> GetPendingRequests();
        Response<bool> Approve(int requestId);
        Response<bool> Reject(int requestId);
        Response<List<LeaveTypeVM>>  GetAllLeaveTypes();

    }
}
