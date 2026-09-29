using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.ModelVM.LeaveRequestVM
{
    public class LeaveRequestListVM
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string LeaveTypeName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime RequestedAt { get; set; }
    }
}
