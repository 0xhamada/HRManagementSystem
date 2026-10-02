using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.ModelVM.AttendanceVM
{
    public class AttendanceListVM
    {
        public int Id { get; set; }
        public string? EmployeeName { get; set; }   
        public DateTime Date { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
