using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.ModelVM.Employee
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public IFormFile? Image { get; set; }
        public int DeptId { get; set; }
    }
}
