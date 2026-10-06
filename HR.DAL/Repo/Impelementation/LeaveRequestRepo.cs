using HR.DAL.DataBase;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HR.DAL.Repo.Impelementation
{
    public class LeaveRequestRepo : ILeaveRequestRepo
    {
        private readonly AppDbContext appDbContext;
        public LeaveRequestRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public bool AddRequest(LeaveRequest request)
        {
            appDbContext.LeaveRequests.Add(request);
            return appDbContext.SaveChanges() > 0;
            //if (result.Entity.Id > 0)
            //{
            //    appDbContext.SaveChanges();
            //    return true;
            //}
            //return false;
        }

        public List<LeaveRequest> GetAll(Expression<Func<LeaveRequest, bool>>? Filter = null)
        {

            var query = appDbContext.LeaveRequests
                .Include(a=> a.employee)
                .Include(a => a.leavetype)
                .AsQueryable();
            if(Filter != null)
            {
                query = query.Where(Filter);
            }
            query = query.OrderBy(a => a.RequestedAt);
            return query.ToList();
        }

        public LeaveRequest? GetRequestById(int id)
        {
            var request = appDbContext.LeaveRequests
                .Include(a => a.employee)
                .Include(a => a.leavetype)
                .FirstOrDefault(a => a.Id == id);

            return request == null ? null : request;
        }

        public bool UpdateRequest(LeaveRequest request)
        {
            appDbContext.LeaveRequests.Update(request);
            return appDbContext.SaveChanges() > 0;
        }
    }
}
