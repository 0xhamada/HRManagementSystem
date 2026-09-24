using HR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace HR.BLL.ModelVM.Employee
{
    public class GetEmployeeVM
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string? Image { get; set; }
        public HR.DAL.Entities.Department? department { get; set; }
        public int DeptId { get; set; }
    }
}
