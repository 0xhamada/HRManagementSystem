using HR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.Property(lt => lt.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(a => a.LeaveRequests)
                   .WithOne(a => a.leavetype)
                   .HasForeignKey(a => a.LeaveTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
