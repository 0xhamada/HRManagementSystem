using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HR.DAL.Repo.Abstraction
{
    public interface IEmployeeRepo
    {
        bool Add(Employee employee);
        bool Edit(Employee employee);
        bool ToggleStatus(string id);
        List<Employee> GetAll(Expression<Func<Employee, bool>>? filter = null);
        Employee? GetById(string id);
    }
}
