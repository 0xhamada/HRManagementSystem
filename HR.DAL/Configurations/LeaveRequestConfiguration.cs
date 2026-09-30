using HR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Configurations
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.Property(lr => lr.Reason)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(lr => lr.Status)
                   .IsRequired()
                   .HasConversion<string>();   // stores enum as readable text

            builder.Property(lr => lr.StartDate)
                   .IsRequired();              // make migration 

            builder.Property(lr => lr.EndDate)
                   .IsRequired();

            builder.Property(lr => lr.RequestedAt)
                   .IsRequired();

            builder.HasOne(a=> a.employee)
                   .WithMany(a=> a.LeaveRequests)
                   .HasForeignKey(a=>a.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
