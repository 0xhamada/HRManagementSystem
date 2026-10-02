using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Abstraction
{
    public interface IAttendanceRepo
    {
        bool Add(string employeeId);
        bool Update(Attendance attendance);
        Attendance? GetTodayRecord(string employeeId);
        List<Attendance> GetByEmployee(string employeeId);
        List<Attendance> GetAll();
    }
}
