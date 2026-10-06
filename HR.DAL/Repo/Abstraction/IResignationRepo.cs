using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Repo.Abstraction
{
    public interface IResignationRepo
    {
        bool Add(Resignation resignation);
        bool Update(Resignation resignation);
        Resignation? GetById(int id);
        Resignation? GetPendingByEmployee(string employeeId);
        List<Resignation> GetAll(bool pendingOnly);
    }
}
