using HR.DAL.DataBase;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Impelementation
{
    public class AttendanceRepo : IAttendanceRepo
    {
        private readonly AppDbContext _appDbContext;

        public AttendanceRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool Add(string employeeId)
        {
            var Attendance = new Attendance(employeeId);
            _appDbContext.Attendances.Add(Attendance);
            return _appDbContext.SaveChanges() > 0;
        }

        public List<Attendance> GetAll()
        {
            return _appDbContext.Attendances.Include(a=> a.Employee).OrderByDescending(a=>a.Date).ToList();
        }

        public List<Attendance> GetByEmployee(string employeeId)
        {
           return _appDbContext.Attendances.Where(a=>a.EmployeeId == employeeId)
                                           .OrderByDescending(a=>a.Date)
                                           .ToList();
        }

        public Attendance? GetTodayRecord(string employeeId)
        {
            // var employee = _appDbContext.Attendances.FirstOrDefault(a => a.EmployeeId == employeeId);
            var today = DateTime.UtcNow.Date;
            return _appDbContext.Attendances
                                           .FirstOrDefault(a => a.EmployeeId == employeeId && a.Date == today);
        }

        public bool Update(Attendance attendance)
        {
            _appDbContext.Attendances.Update(attendance);
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
