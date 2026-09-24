using System;
using System.Collections.Generic;
using System.Text;
using HR.DAL.Entities;
namespace HR.BLL.ModelVM.Department
{
    public class GetDepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<HR.DAL.Entities.Employee> Employees { get;  set; } 

    }
}
