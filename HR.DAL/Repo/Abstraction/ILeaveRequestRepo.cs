using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HR.DAL.Repo.Abstraction
{
    public interface ILeaveRequestRepo
    {
        bool AddRequest(LeaveRequest request);
        bool UpdateRequest(LeaveRequest request);
        LeaveRequest? GetRequestById(int id);
        List<LeaveRequest> GetAll(Expression<Func<LeaveRequest, bool>>? Filter = null);
    }
}
