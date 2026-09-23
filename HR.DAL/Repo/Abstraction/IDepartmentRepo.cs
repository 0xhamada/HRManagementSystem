using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Abstraction
{
    public interface IDepartmentRepo
    {
        bool Add(HR.DAL.Entities.Department department);
        bool Edit(HR.DAL.Entities.Department department);
        bool Delete(int id);
        List<HR.DAL.Entities.Department> GetAll(System.Linq.Expressions.Expression<Func<HR.DAL.Entities.Department, bool>>? filter = null);
        HR.DAL.Entities.Department? GetById(int id);
    }
}
