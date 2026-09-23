using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Impelementation
{
    public class DepartmentRepo : HR.DAL.Repo.Abstraction.IDepartmentRepo
    {
        private readonly HR.DAL.DataBase.AppDbContext db;

        public DepartmentRepo(HR.DAL.DataBase.AppDbContext db)
        {
            this.db = db;
        }

        public bool Add(HR.DAL.Entities.Department department)
        {
            db.Departments.Add(department);
            return db.SaveChanges() > 0;
        }

        public bool Edit(HR.DAL.Entities.Department newDepartment)
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

        public List<HR.DAL.Entities.Department> GetAll(System.Linq.Expressions.Expression<Func<HR.DAL.Entities.Department, bool>>? filter = null)
        {
            return filter is null ? db.Departments.ToList() : db.Departments.Where(filter).ToList();
        }

        public HR.DAL.Entities.Department? GetById(int id)
        {
            return db.Departments.Find(id);
        }
    }
}
