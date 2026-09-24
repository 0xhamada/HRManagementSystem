using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HR.DAL.Repo.Abstraction
{
    public interface IDepartmentRepo
    {
        bool Add(Department department);
        bool Edit(Department newDepartment);
        bool Delete(int id);
        List<Department> GetAll(Expression<Func<Department, bool>>? filter = null);

        Department? GetById(int id);
    }
}
