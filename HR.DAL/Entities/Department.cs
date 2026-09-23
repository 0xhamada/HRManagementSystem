using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Entities
{
    public class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Code { get; private set; } = null!;

        // Navigation property: one Department has many Employees
        public List<Employee> Employees { get; private set; } = new List<Employee>();

        private Department() { } // required by EF Core

        public Department(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public void Update(string name, string code)
        {
            Name = name;
            Code = code;
        }
    
    }
}
