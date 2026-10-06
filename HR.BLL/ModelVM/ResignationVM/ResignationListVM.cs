using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.ModelVM.ResignationVM
{
    public class ResignationListVM
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public DateTime RequestedDate { get; set; }
        public DateTime LastWorkingDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
