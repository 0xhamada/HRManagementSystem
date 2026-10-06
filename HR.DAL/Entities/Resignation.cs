using HR.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Entities
{
    public class Resignation
    {
        public int Id { get; private set; }
        public string Reason { get; private set; } = null!;
        public DateTime RequestedDate { get; private set; }     // when they submitted
        public DateTime LastWorkingDate { get; private set; }   // intended last day
        public ResignationStatus Status { get; private set; }

        public string EmployeeId { get; private set; } = null!;
        public Employee? Employee { get; private set; }

        private Resignation() { }

        public Resignation(string employeeId, string reason, DateTime lastWorkingDate)
        {
            EmployeeId = employeeId;
            Reason = reason;
            LastWorkingDate = lastWorkingDate;
            RequestedDate = DateTime.UtcNow;
            Status = ResignationStatus.Pending;
        }

        public void Approve() => Status = ResignationStatus.Approved;
        public void Reject() => Status = ResignationStatus.Rejected;
    }
}
 

