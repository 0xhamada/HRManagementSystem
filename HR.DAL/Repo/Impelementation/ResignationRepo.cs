using HR.DAL.DataBase;
using HR.DAL.Entities;
using HR.DAL.Enum;
using HR.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Impelementation
{
    public class ResignationRepo : IResignationRepo
    {
        private readonly AppDbContext appDbContext;

        public ResignationRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public bool Add(Resignation resignation)
        {
            appDbContext.Resignations.Add(resignation);
            return appDbContext.SaveChanges() > 0;
        }

        public List<Resignation> GetAll(bool pendingOnly)
        {
            var query = appDbContext.Resignations
                                     .Include(r => r.Employee)
                                     .AsQueryable();
            if(pendingOnly)
            {
                query = query.Where(a=> a.Status == ResignationStatus.Pending);
            }
            return query.OrderBy(r=> r.RequestedDate).ToList();
        }

        public Resignation? GetById(int id)
        {
            return appDbContext.Resignations
                .Include(a=>a.Employee)
                .FirstOrDefault(a=>a.Id == id);
        }

        public Resignation? GetPendingByEmployee(string employeeId)
        {
            return appDbContext.Resignations
                .FirstOrDefault(a => a.EmployeeId == employeeId && a.Status == ResignationStatus.Pending);
        }

        public bool Update(Resignation resignation)
        {
            appDbContext.Resignations.Update(resignation);
            return appDbContext.SaveChanges() > 0;
        }
    }
}
