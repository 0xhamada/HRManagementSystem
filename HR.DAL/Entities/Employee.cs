using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Entities
{
    public class Employee : IdentityUser
    {
        public string Name { get; private set; } = null!;
        public int Age { get; private set; }
        public decimal Salary { get; private set; }
        public string? Image { get; private set; }

        public int DeptId { get; private set; }
        public Department? Department { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Employee() { } // required by EF Core

        public Employee(string name, int age, decimal salary, int deptId, string? image = null)
        {
            Name = name;
            Age = age;
            Salary = salary;
            DeptId = deptId;
            Image = image;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, int age, decimal salary, int deptId, string? image)
        {
            Name = name;
            Age = age;
            Salary = salary;
            DeptId = deptId;
            Image = image;
        }
        public void SetImage(string image)
        {
            Image = image;
        }
        public void UpdateImage(string image)
        {
            Image = image;
        }
        public void ToggleStatus() => IsDeleted = !IsDeleted;
    }
}
