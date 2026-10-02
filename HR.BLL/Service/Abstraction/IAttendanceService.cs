using HR.BLL.ModelVM.AttendanceVM;
using HR.BLL.ModelVM.ResponseResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface IAttendanceService
    {
        Response<bool> CheckIn(string employeeId);
        Response<bool> CheckOut(string employeeId);
        Response<List<AttendanceListVM>> GetMyAttendance(string employeeId);
        Response<List<AttendanceListVM>> GetAllAttendance();
        Response<AttendanceListVM> GetTodayStatus(string employeeId);
    }
}
