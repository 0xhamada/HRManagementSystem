using HR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(d => d.Code)
                   .IsRequired()
                   
                   .HasMaxLength(20);

        }
    }
}
