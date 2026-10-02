using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Entities
{
    public class Attendance
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }        
        public DateTime CheckInTime { get; private set; }
        public DateTime? CheckOutTime { get; private set; } 

        public string EmployeeId { get; private set; } = null!;
        public Employee? Employee { get; private set; }

        private Attendance() { } // required by EF Core

        public Attendance(string employeeId)
        {
            EmployeeId = employeeId;
            Date = DateTime.UtcNow.Date;   
            CheckInTime = DateTime.UtcNow;
        }

        public void CheckOut()
        {
            CheckOutTime = DateTime.UtcNow;
        }

        public bool IsCheckedOut() => CheckOutTime.HasValue;
    }
}

