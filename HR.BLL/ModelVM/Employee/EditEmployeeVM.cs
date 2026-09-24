using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.ModelVM.Employee
{
    public class EditEmployeeVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public int DeptId { get; set; }
        public HR.DAL.Entities.Department? department { get; set; }

        public IFormFile? NewImage { get; set; }
        public string? Image { get; set; }
    }
}
