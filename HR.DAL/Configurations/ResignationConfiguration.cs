using HR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Configurations
{
    public class ResignationConfiguration : IEntityTypeConfiguration<Resignation>
    {
        public void Configure(EntityTypeBuilder<Resignation> builder)
        {
            builder.Property(r => r.Reason)
                  .IsRequired()
                  .HasMaxLength(500);

            builder.Property(r => r.Status)
                   .HasConversion<string>();

            builder.HasOne(a => a.Employee)
                   .WithMany()
                   .HasForeignKey(e => e.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
