using HR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(e => e.Salary)
                   .HasColumnType("decimal(18,2)");

            

            builder.Property(e => e.DeptId)
                   .IsRequired();

            builder.HasOne(a => a.Department)
                   .WithMany(e => e.Employees)
                   .HasForeignKey(a => a.DeptId);
                
        }
    }
}
