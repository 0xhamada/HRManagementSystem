using HR.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace HR.DAL.Entities
{
    public class LeaveRequest
    {
        public int Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; } = null!;
        public DateTime RequestedAt { get; private set; }
        public string EmployeeId { get; private set; } = null!;
        public Employee? employee { get; private set; }
        public int LeaveTypeId { get; private set; }
        public LeaveType? leavetype { get; private set; }

        public LeaveStatus Status { get; private set; }
        private LeaveRequest() { }
        public LeaveRequest(string employeeId, int leaveTypeId, DateTime startDate, DateTime endDate, string reason)
        {
            EmployeeId = employeeId;
            LeaveTypeId = leaveTypeId;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            Status = LeaveStatus.Pending;   // every request starts as Pending — not optional, not caller-controlled
            RequestedAt = DateTime.UtcNow;
        }

        public void Approve()
        {
            Status = LeaveStatus.Approved;
        }

        public void Reject()
        {
            Status = LeaveStatus.Rejected;
        }


    }
}
