using HR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {

        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.Property(a => a.Date)
                 .IsRequired();

            builder.Property(a => a.CheckInTime)
                   .IsRequired();

            builder.HasOne(a=> a.Employee)
                   .WithMany()
                   .HasForeignKey(a=> a.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
