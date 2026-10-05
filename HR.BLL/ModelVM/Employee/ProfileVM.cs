using System;
using System.Collections.Generic;
using System.Text;
using HR.DAL.Entities;
namespace HR.BLL.ModelVM.Employee
{
    public class ProfileVM
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public decimal Salary { get; set; }

        public int Age { get; set; }
        public string JobTitle { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public string? DepartmentName { get; set; }

        public string? Image { get; set; }
    }
}
