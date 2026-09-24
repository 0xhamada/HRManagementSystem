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
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext db;
        public EmployeeRepo(AppDbContext db)
        {
            this.db = db;
        }
        public bool Add(Employee employee)
        {
            try
            {
                var result = db.Users.Add(employee);
                db.SaveChanges();
                if(result.Entity.Id != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public bool Edit(Employee newemployee)
        {
            try
            {
                var oldemployee = db.Users.Where(a => a.Id == newemployee.Id).FirstOrDefault();
                if (oldemployee != null)
                {
                    oldemployee.Update(newemployee.Name, newemployee.Age, newemployee.Salary, newemployee.DeptId, newemployee.Image);
                    
                    
                        db.SaveChanges();
                        return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }
        public bool ToggleStatus(string id)
        {
            try
            {
                var Deleted = db.Users.Where(a => a.Id == id).FirstOrDefault();
                if (Deleted != null)
                {
                     Deleted.ToggleStatus();
                     db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }



        public List<Employee> GetAll(Expression<Func<Employee, bool>>? filter = null)
        {
            try
            {
                if (filter != null)
                {
                    var result = db.Users.Where(filter).Include(a => a.Department).ToList();
                    return result;
                }
                else
                {
                    var result = db.Users.Include(a => a.Department).ToList();
                    return result;
                }
            }
            catch
            {
                throw;
            }

        }

        public Employee? GetById(string id)
        {
            try
            {
                var result = db.Users.Where(a => a.Id == id).Include(a=>a.Department).FirstOrDefault();
                if (result != null)
                {
                    return result;
                }
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
