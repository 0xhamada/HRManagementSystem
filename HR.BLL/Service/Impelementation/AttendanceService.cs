using HR.BLL.ModelVM.AttendanceVM;
using HR.BLL.ModelVM.ResponseResult;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Impelementation
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepo _attendanceRepo;

        public AttendanceService(IAttendanceRepo attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }

        public Response<bool> CheckIn(string employeeId)
        {
            try
            {
                var existing = _attendanceRepo.GetTodayRecord(employeeId);
               if ( existing != null)
                {
                    return new Response<bool>(false , " you aleardy checked in", true );
                }
                var checkedIn = _attendanceRepo.Add(employeeId);
                return new Response<bool>(checkedIn, checkedIn? null! : " Failed to Check In", !checkedIn);
            }
            catch (Exception ex) 
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<bool> CheckOut(string employeeId)
        {
            try
            {
                var existing = _attendanceRepo.GetTodayRecord(employeeId);
                if (existing == null)
                {
                    return new Response<bool>(false, " you Have not check in", true);
                }
                if(existing.IsCheckedOut())
                {
                    return new Response<bool>(false, " you Have aleardy checked out", true);
                }
                existing.CheckOut();
                var result = _attendanceRepo.Update(existing);
                return new Response<bool>(result, result ? null! : "Failed to check out", !result);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        public Response<List<AttendanceListVM>> GetAllAttendance()
        {
            try
            {
                var Attendance = _attendanceRepo.GetAll();
                var mapped = Attendance.Select(a => new AttendanceListVM
                {
                    Id = a.Id,
                    EmployeeName = a.Employee?.Name ?? "Unknown",
                    Date = a.Date,
                    CheckInTime = a.CheckInTime,
                    CheckOutTime = a.CheckOutTime,
                    IsCheckedOut = a.IsCheckedOut()
                }).ToList();
                return new Response<List<AttendanceListVM>>(mapped, null!, false);

            }
            catch (Exception ex)
            {
                return new Response<List<AttendanceListVM>>(null!, ex.Message, true);
            }
        }

        public Response<List<AttendanceListVM>> GetMyAttendance(string employeeId)
        {
            try
            {
                var Attendance = _attendanceRepo.GetByEmployee(employeeId);
                var mapped = Attendance.Select(a => new AttendanceListVM
                {
                    Id = a.Id,
                    Date = a.Date,
                    CheckInTime = a.CheckInTime,
                    CheckOutTime = a.CheckOutTime,
                    IsCheckedOut = a.IsCheckedOut()
                }).ToList();
                return new Response<List<AttendanceListVM>>(mapped, null!, false);

            }
            catch (Exception ex)
            {
                return new Response<List<AttendanceListVM>>(null!, ex.Message, true);
            }
        }

        public Response<AttendanceListVM> GetTodayStatus(string employeeId)
        {
            try
            {
                var record = _attendanceRepo.GetTodayRecord(employeeId);
                if (record == null)
                    return new Response<AttendanceListVM>(null!, "Not checked in yet", false);

                var mapped = new AttendanceListVM
                {
                    Id = record.Id,
                    Date = record.Date,
                    CheckInTime = record.CheckInTime,
                    CheckOutTime = record.CheckOutTime,
                    IsCheckedOut = record.IsCheckedOut()
                };

                return new Response<AttendanceListVM>(mapped, null!, false);

            }
            catch (Exception ex)
            {
                return new Response<AttendanceListVM>(null!, ex.Message, true);
            }
        }
    }
}
