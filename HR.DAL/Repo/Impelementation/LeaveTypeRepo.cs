using HR.DAL.DataBase;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Impelementation
{
    public class LeaveTypeRepo : ILeaveTypeRepo
    {
        private readonly AppDbContext appDbContext;
        public LeaveTypeRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public bool Add(LeaveType type)
        {
            var result = appDbContext.LeaveTypes.Add(type);
            return appDbContext.SaveChanges() > 0;
        }

        public List<LeaveType> GetAll() => appDbContext.LeaveTypes.ToList();
        //{
        //    var result = appDbContext.LeaveTypes.ToList();
        //    return result;
        //}

        public LeaveType? GetById(int id) => appDbContext.LeaveTypes.FirstOrDefault(x => x.Id == id);
        //{
        //    var result = appDbContext.LeaveTypes.FirstOrDefault(x => x.Id == id);
        //    return result == null ? null : result;
        //}
        public bool Delete(int id)
        {
            var entity = appDbContext.LeaveTypes.FirstOrDefault(a => a.Id == id);
            if (entity == null) 
                return false;
            
             appDbContext.LeaveTypes.Remove(entity);
            
            return appDbContext.SaveChanges() > 0;
        }
        public bool Update(LeaveType type)
        {
            var result = appDbContext.LeaveTypes.Update(type);
            return appDbContext.SaveChanges() > 0;
        }
    }
}
