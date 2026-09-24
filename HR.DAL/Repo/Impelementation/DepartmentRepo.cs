using HR.DAL.DataBase;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Impelementation
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext db;

        public DepartmentRepo(AppDbContext db)
        {
            this.db = db;
        }

        public bool Add(Department department)
        {
            db.Departments.Add(department);
            return db.SaveChanges() > 0;
        }

        public bool Edit(Department newDepartment)
        {
            var oldDepartment = db.Departments.Find(newDepartment.Id);
            if (oldDepartment is null) return false;

            oldDepartment.Update(newDepartment.Name, newDepartment.Code);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var department = db.Departments.Find(id);
            if (department is null) return false;

            db.Departments.Remove(department);
            return db.SaveChanges() > 0;
        }

        public List<Department> GetAll(System.Linq.Expressions.Expression<Func<Department, bool>>? filter = null)
        {
            return filter is null ? db.Departments.Include(a=> a.Employees.Where(a=>a.IsDeleted == false)).ToList() : db.Departments.Where(filter).Include(a=>a.Employees.Where(a => a.IsDeleted == false)).ToList();
        }

        public Department? GetById(int id)
        {
            return db.Departments.Find(id);
        }
    }
}
