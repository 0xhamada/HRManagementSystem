using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Abstraction
{
    public interface ILeaveTypeRepo
    {
        List<LeaveType> GetAll();
        LeaveType? GetById(int id);
        bool Add(LeaveType type);
        bool Update(LeaveType type);
        bool Delete(int id);
    }
}
